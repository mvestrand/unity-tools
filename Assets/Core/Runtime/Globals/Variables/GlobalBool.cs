using System;
using UnityEngine;


namespace MVest.Unity.Globals {

	/// <summary>
  /// A global variable of type System.Boolean
  /// </summary>
	[CreateAssetMenu(menuName = "Global/Bool", order = 5)]
	public class GlobalBool : GlobalVariable<bool> { }

	/// <summary>
  /// A global variable reference of type System.Boolean
  /// </summary>
	[Serializable]
	public class GlobalBoolRef : GlobalRef<GlobalBool, bool> { }

}
