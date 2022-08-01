using System;
using UnityEngine;


namespace MVest.Unity.Globals {

	/// <summary>
  /// A global variable of type System.Single
  /// </summary>
	[CreateAssetMenu(menuName = "Global/Float", order = 3)]
	public class GlobalFloat : GlobalVariable<float> { }

	/// <summary>
  /// A global variable reference of type System.Single
  /// </summary>
	[Serializable]
	public class GlobalFloatRef : GlobalRef<GlobalFloat, float> { }

}
