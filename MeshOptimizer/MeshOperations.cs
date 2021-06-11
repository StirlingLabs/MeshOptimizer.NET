using System;
using System.Collections.Generic;

namespace MeshOptimizer {

  /// <summary>
  /// A collection of common meshoptimizer operations
  /// </summary>
  public static class MeshOperations {

    /// <summary>
    /// Executes the "standard" optimizations that are suggested in the meshoptimizer README
    /// See (https://github.com/zeux/meshoptimizer#pipeline)
    /// </summary>
    /// <param name="Vertices">The vertices of the mesh</param>
    /// <param name="Indices">The indices of the mesh</param>
    /// <param name="VertexSize">The size of T in bytes</param>
    /// <typeparam name="T">The vertex type</typeparam>
    /// <returns>A tuple with the new vertices and indices</returns>
    public static Tuple<T[], uint[]> Optimize<T>(T[] Vertices, uint[] Indices, uint VertexSize)
      where T : unmanaged {
      var results = Reindex(Vertices, Indices, VertexSize);
      var vertices = results.Item1;
      var indices = results.Item2;

      OptimizeCache(indices, vertices.Length);
      OptimizeOverdraw(indices, vertices, VertexSize, 1.05f);
      OptimizeVertexFetch(indices, vertices, VertexSize);
      return Tuple.Create(vertices, indices);
    }

    /// <summary>
    /// Reindex the given mesh. See (https://github.com/zeux/meshoptimizer#indexing)
    /// </summary>
    /// <param name="Vertices">The vertices of the mesh</param>
    /// <param name="Indices">The indices of the mesh</param>
    /// <param name="VertexSize">The size of T in bytes</param>
    /// <typeparam name="T">The vertex type</typeparam>
    /// <returns>A tuple with the new vertices and indices</returns>
    public static unsafe Tuple<T[], uint[]> Reindex<T>(T[] Vertices, uint[] Indices, uint VertexSize)
      where T : unmanaged {
      var remap = new uint[Vertices.Length];
      var indexCount = (Indices?.Length ?? Vertices.Length);
      var targetIndices = new uint[indexCount];
      fixed (void* pVertices = Vertices)
      fixed (uint* pRemap = remap)
      fixed (uint* pInputIndices = Indices)
      fixed (uint* pTargetIndices = targetIndices) {
        var totalVertices = Native.GenerateVertexRemap(
          pRemap,
          pInputIndices,
          (UIntPtr) indexCount,
          pVertices,
          (UIntPtr) Vertices.Length,
          (UIntPtr) VertexSize
        );

        Native.RemapIndexBuffer(pTargetIndices, pInputIndices, (UIntPtr) indexCount, pRemap);

        var targetVertices = new T[totalVertices.ToUInt64()];

        fixed (void* pTargetVertices = targetVertices)
          Native.RemapVertexBuffer(pTargetVertices, pVertices, (UIntPtr) Vertices.Length, (UIntPtr) VertexSize, pRemap);

        return Tuple.Create(targetVertices, targetIndices);
      }
    }

    /// <summary>
    /// Optimizes the mesh for the GPU cache. See (https://github.com/zeux/meshoptimizer#vertex-cache-optimization)
    /// </summary>
    /// <param name="Indices">The indices of the mesh</param>
    /// <param name="VertexCount">Total amount of vertices the mesh has</param>
    public static unsafe void OptimizeCache(uint[] Indices, int VertexCount) {
      fixed (uint* pIndices = Indices)
        Native.OptimizeVertexCache(pIndices, pIndices, (UIntPtr) Indices.Length, (UIntPtr) VertexCount);
    }

    /// <summary>
    /// Optimizes the mesh to reduce overdraw. See (https://github.com/zeux/meshoptimizer#vertex-cache-optimization)
    /// </summary>
    /// <param name="Indices">The indices of the mesh</param>
    /// <param name="Vertices">The vertices of the mesh</param>
    /// <param name="Stride">Space (in bytes) between each vertex</param>
    /// <param name="Threshold">The optimization threshold</param>
    /// <typeparam name="T"></typeparam>
    public static unsafe void OptimizeOverdraw<T>(uint[] Indices, T[] Vertices, uint Stride, float Threshold)
      where T : unmanaged {
      fixed (void* pVertices = Vertices)
      fixed (uint* pIndices = Indices)
        Native.OptimizeOverdraw(pIndices, pIndices, (UIntPtr) Indices.Length, (float*) pVertices, (UIntPtr) Vertices.Length, (UIntPtr) Stride, Threshold);
    }

    /// <summary>
    /// Optimizes vertex fetching. See (https://github.com/zeux/meshoptimizer#vertex-cache-optimization)
    /// </summary>
    /// <param name="Indices">The indices of the mesh</param>
    /// <param name="Vertices">The vertices of the mesh</param>
    /// <param name="VertexSize">The size of T in bytes</param>
    /// <typeparam name="T">The vertex type</typeparam>
    public static unsafe void OptimizeVertexFetch<T>(uint[] Indices, T[] Vertices, uint VertexSize)
      where T : unmanaged {
      fixed (void* pVertices = Vertices)
      fixed (uint* pIndices = Indices)
        Native.OptimizeVertexFetch(pVertices, pIndices, (UIntPtr) Indices.Length, pVertices, (UIntPtr) Vertices.Length, (UIntPtr) VertexSize);
    }

  }

}