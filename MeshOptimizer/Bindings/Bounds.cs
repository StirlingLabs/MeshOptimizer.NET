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

namespace MeshOptimizer {

  public unsafe struct Bounds {

    [NativeTypeName("float [3]")]
    public fixed float center[3];

    public float radius;

    [NativeTypeName("float [3]")]
    public fixed float cone_apex[3];

    [NativeTypeName("float [3]")]
    public fixed float cone_axis[3];

    public float cone_cutoff;

    [NativeTypeName("signed char [3]")]
    public fixed sbyte cone_axis_s8[3];

    [NativeTypeName("signed char")]
    public sbyte cone_cutoff_s8;

  }

}