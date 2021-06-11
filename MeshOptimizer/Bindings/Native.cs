/* MIT License
 * 
 * Copyright (c) 2016-2021 Arseny Kapoulkine
 * 
 * Permission is hereby granted, free of charge, to any person obtaining
 * a copy of this software and associated documentation files
 * (the "Software"), to deal in the Software without restriction,
 * including without limitation the rights to use, copy, modify, merge,
 * publish, distribute, sublicense, and/or sell copies of the Software,
 * and to permit persons to whom the Software is furnished to do so,
 * subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS
 * BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN
 * ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
 * CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE 
 * SOFTWARE.
 */

using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace MeshOptimizer {

  [PublicAPI]
  public static unsafe class Native {

    static Native()
      => NativeLibrary.SetDllImportResolver(typeof(Native).Assembly, (name, assembly, path) => {
        if (NativeLibrary.TryLoad(name, out var lib))
          return lib;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
          return NativeLibrary.Load("runtimes/win-x64/native/" + name);

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
          return NativeLibrary.Load("runtimes/osx-x64/native/" + name);

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
          return NativeLibrary.Load("runtimes/linux-x64/native/" + name);

        throw new PlatformNotSupportedException(RuntimeInformation.OSDescription + " does not appear to be supported.");
      });

    [NativeTypeName("#define MESHOPTIMIZER_VERSION 160")]
    public const int MESHOPTIMIZER_VERSION = 160;

    private const string LibName = "meshoptimizer";

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_generateVertexRemap", ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern UIntPtr GenerateVertexRemap([NativeTypeName("unsigned int *")] uint* destination, [NativeTypeName("const unsigned int *")]
      uint* indices, [NativeTypeName("size_t")] UIntPtr index_count, [NativeTypeName("const void *")] void* vertices, [NativeTypeName("size_t")] UIntPtr vertex_count, [NativeTypeName("size_t")] UIntPtr vertex_size);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_generateVertexRemapMulti", ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern UIntPtr GenerateVertexRemapMulti([NativeTypeName("unsigned int *")] uint* destination, [NativeTypeName("const unsigned int *")]
      uint* indices, [NativeTypeName("size_t")] UIntPtr index_count, [NativeTypeName("size_t")] UIntPtr vertex_count, [NativeTypeName("const struct meshopt_Stream *")]
      Stream* streams, [NativeTypeName("size_t")] UIntPtr stream_count);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_remapVertexBuffer", ExactSpelling = true)]
    public static extern void RemapVertexBuffer([NativeTypeName("void *")] void* destination, [NativeTypeName("const void *")] void* vertices, [NativeTypeName("size_t")] UIntPtr vertex_count, [NativeTypeName("size_t")] UIntPtr vertex_size,
      [NativeTypeName("const unsigned int *")]
      uint* remap);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_remapIndexBuffer", ExactSpelling = true)]
    public static extern void RemapIndexBuffer([NativeTypeName("unsigned int *")] uint* destination, [NativeTypeName("const unsigned int *")]
      uint* indices, [NativeTypeName("size_t")] UIntPtr index_count, [NativeTypeName("const unsigned int *")]
      uint* remap);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_generateShadowIndexBuffer", ExactSpelling = true)]
    public static extern void GenerateShadowIndexBuffer([NativeTypeName("unsigned int *")] uint* destination, [NativeTypeName("const unsigned int *")]
      uint* indices, [NativeTypeName("size_t")] UIntPtr index_count, [NativeTypeName("const void *")] void* vertices, [NativeTypeName("size_t")] UIntPtr vertex_count, [NativeTypeName("size_t")] UIntPtr vertex_size,
      [NativeTypeName("size_t")] UIntPtr vertex_stride);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_generateShadowIndexBufferMulti", ExactSpelling = true)]
    public static extern void GenerateShadowIndexBufferMulti([NativeTypeName("unsigned int *")] uint* destination, [NativeTypeName("const unsigned int *")]
      uint* indices, [NativeTypeName("size_t")] UIntPtr index_count, [NativeTypeName("size_t")] UIntPtr vertex_count, [NativeTypeName("const struct meshopt_Stream *")]
      Stream* streams, [NativeTypeName("size_t")] UIntPtr stream_count);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_generateAdjacencyIndexBuffer", ExactSpelling = true)]
    public static extern void GenerateAdjacencyIndexBuffer([NativeTypeName("unsigned int *")] uint* destination, [NativeTypeName("const unsigned int *")]
      uint* indices, [NativeTypeName("size_t")] UIntPtr index_count, [NativeTypeName("const float *")] float* vertex_positions, [NativeTypeName("size_t")] UIntPtr vertex_count, [NativeTypeName("size_t")] UIntPtr vertex_positions_stride);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_generateTessellationIndexBuffer", ExactSpelling = true)]
    public static extern void GenerateTessellationIndexBuffer([NativeTypeName("unsigned int *")] uint* destination, [NativeTypeName("const unsigned int *")]
      uint* indices, [NativeTypeName("size_t")] UIntPtr index_count, [NativeTypeName("const float *")] float* vertex_positions, [NativeTypeName("size_t")] UIntPtr vertex_count, [NativeTypeName("size_t")] UIntPtr vertex_positions_stride);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_optimizeVertexCache", ExactSpelling = true)]
    public static extern void OptimizeVertexCache([NativeTypeName("unsigned int *")] uint* destination, [NativeTypeName("const unsigned int *")]
      uint* indices, [NativeTypeName("size_t")] UIntPtr index_count, [NativeTypeName("size_t")] UIntPtr vertex_count);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_optimizeVertexCacheStrip", ExactSpelling = true)]
    public static extern void OptimizeVertexCacheStrip([NativeTypeName("unsigned int *")] uint* destination, [NativeTypeName("const unsigned int *")]
      uint* indices, [NativeTypeName("size_t")] UIntPtr index_count, [NativeTypeName("size_t")] UIntPtr vertex_count);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_optimizeVertexCacheFifo", ExactSpelling = true)]
    public static extern void OptimizeVertexCacheFifo([NativeTypeName("unsigned int *")] uint* destination, [NativeTypeName("const unsigned int *")]
      uint* indices, [NativeTypeName("size_t")] UIntPtr index_count, [NativeTypeName("size_t")] UIntPtr vertex_count, [NativeTypeName("unsigned int")] uint cache_size);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_optimizeOverdraw", ExactSpelling = true)]
    public static extern void OptimizeOverdraw([NativeTypeName("unsigned int *")] uint* destination, [NativeTypeName("const unsigned int *")]
      uint* indices, [NativeTypeName("size_t")] UIntPtr index_count, [NativeTypeName("const float *")] float* vertex_positions, [NativeTypeName("size_t")] UIntPtr vertex_count, [NativeTypeName("size_t")] UIntPtr vertex_positions_stride,
      float threshold);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_optimizeVertexFetch", ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern UIntPtr OptimizeVertexFetch([NativeTypeName("void *")] void* destination, [NativeTypeName("unsigned int *")] uint* indices, [NativeTypeName("size_t")] UIntPtr index_count,
      [NativeTypeName("const void *")] void* vertices, [NativeTypeName("size_t")] UIntPtr vertex_count, [NativeTypeName("size_t")] UIntPtr vertex_size);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_optimizeVertexFetchRemap", ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern UIntPtr OptimizeVertexFetchRemap([NativeTypeName("unsigned int *")] uint* destination, [NativeTypeName("const unsigned int *")]
      uint* indices, [NativeTypeName("size_t")] UIntPtr index_count, [NativeTypeName("size_t")] UIntPtr vertex_count);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_encodeIndexBuffer", ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern UIntPtr EncodeIndexBuffer([NativeTypeName("unsigned char *")] byte* buffer, [NativeTypeName("size_t")] UIntPtr buffer_size, [NativeTypeName("const unsigned int *")]
      uint* indices, [NativeTypeName("size_t")] UIntPtr index_count);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_encodeIndexBufferBound", ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern UIntPtr EncodeIndexBufferBound([NativeTypeName("size_t")] UIntPtr index_count, [NativeTypeName("size_t")] UIntPtr vertex_count);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_encodeIndexVersion", ExactSpelling = true)]
    public static extern void EncodeIndexVersion(int version);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_decodeIndexBuffer", ExactSpelling = true)]
    public static extern int DecodeIndexBuffer([NativeTypeName("void *")] void* destination, [NativeTypeName("size_t")] UIntPtr index_count, [NativeTypeName("size_t")] UIntPtr index_size, [NativeTypeName("const unsigned char *")]
      byte* buffer, [NativeTypeName("size_t")] UIntPtr buffer_size);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_encodeIndexSequence", ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern UIntPtr EncodeIndexSequence([NativeTypeName("unsigned char *")] byte* buffer, [NativeTypeName("size_t")] UIntPtr buffer_size, [NativeTypeName("const unsigned int *")]
      uint* indices, [NativeTypeName("size_t")] UIntPtr index_count);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_encodeIndexSequenceBound", ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern UIntPtr EncodeIndexSequenceBound([NativeTypeName("size_t")] UIntPtr index_count, [NativeTypeName("size_t")] UIntPtr vertex_count);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_decodeIndexSequence", ExactSpelling = true)]
    public static extern int DecodeIndexSequence([NativeTypeName("void *")] void* destination, [NativeTypeName("size_t")] UIntPtr index_count, [NativeTypeName("size_t")] UIntPtr index_size, [NativeTypeName("const unsigned char *")]
      byte* buffer, [NativeTypeName("size_t")] UIntPtr buffer_size);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_encodeVertexBuffer", ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern UIntPtr EncodeVertexBuffer([NativeTypeName("unsigned char *")] byte* buffer, [NativeTypeName("size_t")] UIntPtr buffer_size, [NativeTypeName("const void *")] void* vertices,
      [NativeTypeName("size_t")] UIntPtr vertex_count, [NativeTypeName("size_t")] UIntPtr vertex_size);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_encodeVertexBufferBound", ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern UIntPtr EncodeVertexBufferBound([NativeTypeName("size_t")] UIntPtr vertex_count, [NativeTypeName("size_t")] UIntPtr vertex_size);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_encodeVertexVersion", ExactSpelling = true)]
    public static extern void EncodeVertexVersion(int version);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_decodeVertexBuffer", ExactSpelling = true)]
    public static extern int DecodeVertexBuffer([NativeTypeName("void *")] void* destination, [NativeTypeName("size_t")] UIntPtr vertex_count, [NativeTypeName("size_t")] UIntPtr vertex_size, [NativeTypeName("const unsigned char *")]
      byte* buffer, [NativeTypeName("size_t")] UIntPtr buffer_size);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_decodeFilterOct", ExactSpelling = true)]
    public static extern void DecodeFilterOct([NativeTypeName("void *")] void* buffer, [NativeTypeName("size_t")] UIntPtr vertex_count, [NativeTypeName("size_t")] UIntPtr vertex_size);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_decodeFilterQuat", ExactSpelling = true)]
    public static extern void DecodeFilterQuat([NativeTypeName("void *")] void* buffer, [NativeTypeName("size_t")] UIntPtr vertex_count, [NativeTypeName("size_t")] UIntPtr vertex_size);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_decodeFilterExp", ExactSpelling = true)]
    public static extern void DecodeFilterExp([NativeTypeName("void *")] void* buffer, [NativeTypeName("size_t")] UIntPtr vertex_count, [NativeTypeName("size_t")] UIntPtr vertex_size);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_simplify", ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern UIntPtr Simplify([NativeTypeName("unsigned int *")] uint* destination, [NativeTypeName("const unsigned int *")]
      uint* indices, [NativeTypeName("size_t")] UIntPtr index_count, [NativeTypeName("const float *")] float* vertex_positions, [NativeTypeName("size_t")] UIntPtr vertex_count, [NativeTypeName("size_t")] UIntPtr vertex_positions_stride,
      [NativeTypeName("size_t")] UIntPtr target_index_count, float target_error, [NativeTypeName("float *")] float* result_error);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_simplifySloppy", ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern UIntPtr SimplifySloppy([NativeTypeName("unsigned int *")] uint* destination, [NativeTypeName("const unsigned int *")]
      uint* indices, [NativeTypeName("size_t")] UIntPtr index_count, [NativeTypeName("const float *")] float* vertex_positions, [NativeTypeName("size_t")] UIntPtr vertex_count, [NativeTypeName("size_t")] UIntPtr vertex_positions_stride,
      [NativeTypeName("size_t")] UIntPtr target_index_count, float target_error, [NativeTypeName("float *")] float* result_error);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_simplifyPoints", ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern UIntPtr SimplifyPoints([NativeTypeName("unsigned int *")] uint* destination, [NativeTypeName("const float *")] float* vertex_positions, [NativeTypeName("size_t")] UIntPtr vertex_count,
      [NativeTypeName("size_t")] UIntPtr vertex_positions_stride, [NativeTypeName("size_t")] UIntPtr target_vertex_count);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_simplifyScale", ExactSpelling = true)]
    public static extern float SimplifyScale([NativeTypeName("const float *")] float* vertex_positions, [NativeTypeName("size_t")] UIntPtr vertex_count, [NativeTypeName("size_t")] UIntPtr vertex_positions_stride);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_stripify", ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern UIntPtr Stripify([NativeTypeName("unsigned int *")] uint* destination, [NativeTypeName("const unsigned int *")]
      uint* indices, [NativeTypeName("size_t")] UIntPtr index_count, [NativeTypeName("size_t")] UIntPtr vertex_count, [NativeTypeName("unsigned int")] uint restart_index);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_stripifyBound", ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern UIntPtr StripifyBound([NativeTypeName("size_t")] UIntPtr index_count);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_unstripify", ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern UIntPtr Unstripify([NativeTypeName("unsigned int *")] uint* destination, [NativeTypeName("const unsigned int *")]
      uint* indices, [NativeTypeName("size_t")] UIntPtr index_count, [NativeTypeName("unsigned int")] uint restart_index);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_unstripifyBound", ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern UIntPtr UnstripifyBound([NativeTypeName("size_t")] UIntPtr index_count);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_analyzeVertexCache", ExactSpelling = true)]
    [return: NativeTypeName("struct meshopt_VertexCacheStatistics")]
    public static extern VertexCacheStatistics AnalyzeVertexCache([NativeTypeName("const unsigned int *")]
      uint* indices, [NativeTypeName("size_t")] UIntPtr index_count, [NativeTypeName("size_t")] UIntPtr vertex_count, [NativeTypeName("unsigned int")] uint cache_size, [NativeTypeName("unsigned int")] uint warp_size,
      [NativeTypeName("unsigned int")] uint primgroup_size);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_analyzeOverdraw", ExactSpelling = true)]
    [return: NativeTypeName("struct meshopt_OverdrawStatistics")]
    public static extern OverdrawStatistics AnalyzeOverdraw([NativeTypeName("const unsigned int *")]
      uint* indices, [NativeTypeName("size_t")] UIntPtr index_count, [NativeTypeName("const float *")] float* vertex_positions, [NativeTypeName("size_t")] UIntPtr vertex_count, [NativeTypeName("size_t")] UIntPtr vertex_positions_stride);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_analyzeVertexFetch", ExactSpelling = true)]
    [return: NativeTypeName("struct meshopt_VertexFetchStatistics")]
    public static extern VertexFetchStatistics AnalyzeVertexFetch([NativeTypeName("const unsigned int *")]
      uint* indices, [NativeTypeName("size_t")] UIntPtr index_count, [NativeTypeName("size_t")] UIntPtr vertex_count, [NativeTypeName("size_t")] UIntPtr vertex_size);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_buildMeshlets", ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern UIntPtr BuildMeshlets([NativeTypeName("struct meshopt_Meshlet *")]
      Meshlet* meshlets, [NativeTypeName("unsigned int *")] uint* meshlet_vertices, [NativeTypeName("unsigned char *")] byte* meshlet_triangles, [NativeTypeName("const unsigned int *")]
      uint* indices, [NativeTypeName("size_t")] UIntPtr index_count, [NativeTypeName("const float *")] float* vertex_positions, [NativeTypeName("size_t")] UIntPtr vertex_count, [NativeTypeName("size_t")] UIntPtr vertex_positions_stride,
      [NativeTypeName("size_t")] UIntPtr max_vertices, [NativeTypeName("size_t")] UIntPtr max_triangles, float cone_weight);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_buildMeshletsScan", ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern UIntPtr BuildMeshletsScan([NativeTypeName("struct meshopt_Meshlet *")]
      Meshlet* meshlets, [NativeTypeName("unsigned int *")] uint* meshlet_vertices, [NativeTypeName("unsigned char *")] byte* meshlet_triangles, [NativeTypeName("const unsigned int *")]
      uint* indices, [NativeTypeName("size_t")] UIntPtr index_count, [NativeTypeName("size_t")] UIntPtr vertex_count, [NativeTypeName("size_t")] UIntPtr max_vertices, [NativeTypeName("size_t")] UIntPtr max_triangles);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_buildMeshletsBound", ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern UIntPtr BuildMeshletsBound([NativeTypeName("size_t")] UIntPtr index_count, [NativeTypeName("size_t")] UIntPtr max_vertices, [NativeTypeName("size_t")] UIntPtr max_triangles);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_computeClusterBounds", ExactSpelling = true)]
    [return: NativeTypeName("struct meshopt_Bounds")]
    public static extern Bounds ComputeClusterBounds([NativeTypeName("const unsigned int *")]
      uint* indices, [NativeTypeName("size_t")] UIntPtr index_count, [NativeTypeName("const float *")] float* vertex_positions, [NativeTypeName("size_t")] UIntPtr vertex_count, [NativeTypeName("size_t")] UIntPtr vertex_positions_stride);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_computeMeshletBounds", ExactSpelling = true)]
    [return: NativeTypeName("struct meshopt_Bounds")]
    public static extern Bounds ComputeMeshletBounds([NativeTypeName("const unsigned int *")]
      uint* meshlet_vertices, [NativeTypeName("const unsigned char *")]
      byte* meshlet_triangles, [NativeTypeName("size_t")] UIntPtr triangle_count, [NativeTypeName("const float *")] float* vertex_positions, [NativeTypeName("size_t")] UIntPtr vertex_count,
      [NativeTypeName("size_t")] UIntPtr vertex_positions_stride);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_spatialSortRemap", ExactSpelling = true)]
    public static extern void SpatialSortRemap([NativeTypeName("unsigned int *")] uint* destination, [NativeTypeName("const float *")] float* vertex_positions, [NativeTypeName("size_t")] UIntPtr vertex_count,
      [NativeTypeName("size_t")] UIntPtr vertex_positions_stride);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_spatialSortTriangles", ExactSpelling = true)]
    public static extern void SpatialSortTriangles([NativeTypeName("unsigned int *")] uint* destination, [NativeTypeName("const unsigned int *")]
      uint* indices, [NativeTypeName("size_t")] UIntPtr index_count, [NativeTypeName("const float *")] float* vertex_positions, [NativeTypeName("size_t")] UIntPtr vertex_count, [NativeTypeName("size_t")] UIntPtr vertex_positions_stride);

    [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
    [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "meshopt_setAllocator", ExactSpelling = true)]
    public static extern void SetAllocator([NativeTypeName("void *(*)(size_t)")] IntPtr allocate, [NativeTypeName("void (*)(void *)")] IntPtr deallocate);

    public static int quantizeUnorm(float v, int N) {
      float scale = (1 << N) - 1;

      v = (v >= 0) ? v : 0;
      v = (v <= 1) ? v : 1;
      return (int) (v * scale + 0.5f);
    }

    public static int quantizeSnorm(float v, int N) {
      var scale = (float) ((1 << (N - 1)) - 1);
      var round = (v >= 0 ? 0.5f : -0.5f);

      v = (v >= -1) ? v : -1;
      v = (v <= +1) ? v : +1;
      return (int) (v * scale + round);
    }

    [return: NativeTypeName("unsigned short")]
    public static ushort quantizeHalf(float v) {
      var u = new AliasedFloat {f = v};
      var ui = u.ui;
      var s = (ui >> 16) & 0x8000u;
      var em = ui & 0x7fffffffu;
      var h = (em - (112u << 23) + (1u << 12)) >> 13;

      h = (em < (113u << 23)) ? 0 : h;
      h = (em >= (143u << 23)) ? 0x7c00u : h;
      h = (em > (255u << 23)) ? 0x7e00u : h;
      return (ushort) (s | h);
    }

    public static float quantizeFloat(float v, int N) {
      var u = new AliasedFloat {f = v};
      var ui = u.ui;
      var mask = (1u << (23 - N)) - 1;
      var round = (1u << (23 - N)) >> 1;
      var e = ui & 0x7f800000u;
      var rui = (ui + round) & ~mask;

      ui = e == 0x7f800000u ? ui : rui;
      ui = e == 0 ? 0 : ui;
      u.ui = ui;
      return u.f;
    }

    [NativeTypeName("void *(*)(size_t)")]
    public static void* allocate(ulong l) => (void*) Marshal.AllocHGlobal((IntPtr) l);

    [NativeTypeName("void (*)(void *)")]
    public static void deallocate(void* p) => Marshal.FreeHGlobal((IntPtr) p);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void* UnmanagedAllocatorDelegate(ulong l);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void UnmanagedDeallocatorDelegate(void* p);

  }

}