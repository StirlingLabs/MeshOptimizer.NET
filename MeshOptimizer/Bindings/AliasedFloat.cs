using System.Runtime.InteropServices;

namespace MeshOptimizer {

  [StructLayout(LayoutKind.Explicit)]
  public struct AliasedFloat {

    [FieldOffset(0)]
    public float f;

    [FieldOffset(0)]
    [NativeTypeName("unsigned int")]
    public uint ui;

  }

}