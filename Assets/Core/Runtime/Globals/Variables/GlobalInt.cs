using System;
using UnityEngine;


namespace MVest.Unity.Globals {

	/// <summary>
  /// A global variable of type System.Int32
  /// </summary>
	[CreateAssetMenu(menuName = "Global/Int", order = 2)]
	public class GlobalInt : GlobalVariable<int> { }

	/// <summary>
  /// A global variable reference of type System.Int32
  /// </summary>
	[Serializable]
	public class GlobalIntRef : GlobalRef<GlobalInt, int> { }



}
