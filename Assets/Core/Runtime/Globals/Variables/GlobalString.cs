using System;
using UnityEngine;


namespace MVest.Unity.Globals {

	/// <summary>
  /// A global variable of type System.String
  /// </summary>
	[CreateAssetMenu(menuName = "Global/String", order = 4)]
	public class GlobalString : GlobalVariable<string> { }

	/// <summary>
  /// A global variable reference of type UnityEngine.Object
  /// </summary>
	[Serializable]
	public class GlobalStringRef : GlobalRef<GlobalString, string> { }

}
