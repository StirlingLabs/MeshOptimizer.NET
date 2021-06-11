using System;
using JetBrains.Annotations;

namespace MeshOptimizer {

  [PublicAPI]
  [AttributeUsage(AttributeTargets.All)]
  public class NativeTypeNameAttribute : Attribute {

    public string Name;

    public NativeTypeNameAttribute(string name) => Name = name;

  }

}