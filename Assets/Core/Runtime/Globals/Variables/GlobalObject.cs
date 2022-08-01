using System;
using UnityEngine;


namespace MVest.Unity.Global {

	/// <summary>
  /// A global variable of type UnityEngine.Object
  /// </summary>
	[CreateAssetMenu(menuName = "Global/Object", order = 1)]
	public class GlobalObject : GlobalVariable<UnityEngine.Object> { }

	/// <summary>
  /// A global variable reference of type UnityEngine.Object
  /// </summary>
	[Serializable]
	public class GlobalObjectRef : GlobalRef<GlobalObject, UnityEngine.Object> { }

}
