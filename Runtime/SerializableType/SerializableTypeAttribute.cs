using System;

namespace MVest.Unity {



  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface)]
  public class SerializableTypeAttribute : Attribute { }
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface)]
  public class SerializableTypeAlwaysAssemblyQualifiedAttribute : Attribute { }

}
