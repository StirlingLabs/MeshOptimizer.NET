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
using JetBrains.Annotations;

namespace MeshOptimizer {

  public struct Allocator : IDisposable {

    [NativeTypeName("void *[24]")]
    private BlocksBuffer blocks;

    [NativeTypeName("size_t")]
    private ulong count;

    public unsafe void Dispose() {
      for (var i = count; i > 0; --i)
        Native.Deallocate(blocks[i - 1]);
    }

    public unsafe T* Allocate<T>(ulong size)
      where T : unmanaged {
      var sizeofT = (ulong) sizeof(T);
      var result = (T*) Native.Allocate(
        size > ulong.MaxValue / sizeofT
          ? ulong.MaxValue
          : size * sizeofT
      );
      blocks[count++] = result;
      return result;
    }

    private unsafe struct BlocksBuffer {

#pragma warning disable 169
      // @formatter:off
      private void*
        e0, e1, e2, e3, e4, e5, e6, e7,
        e8, e9, e10, e11, e12, e13, e14, e15,
        e16, e17, e18, e19, e20, e21, e22, e23;
      // @formatter:on
#pragma warning restore 169

      public ref void* this[int index] {
        get {
          fixed (void** pThis = &e0) {
            return ref pThis[index];
          }
        }
      }

      public ref void* this[long index] {
        get {
          fixed (void** pThis = &e0) {
            return ref pThis[index];
          }
        }
      }

      public ref void* this[uint index] {
        get {
          fixed (void** pThis = &e0) {
            return ref pThis[index];
          }
        }
      }

      public ref void* this[ulong index] {
        get {
          fixed (void** pThis = &e0) {
            return ref pThis[index];
          }
        }
      }

    }

  }

}