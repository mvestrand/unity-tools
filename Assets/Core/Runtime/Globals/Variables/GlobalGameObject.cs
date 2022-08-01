using System;
using UnityEngine;


namespace MVest.Unity.Globals {

	/// <summary>
  /// A global variable of type UnityEngine.GameObject
  /// </summary>
	[CreateAssetMenu(menuName = "Global/Game Object", order = 6)]
	public class GlobalGameObject : GlobalVariable<UnityEngine.GameObject> { }

	/// <summary>
  /// A global variable reference of type UnityEngine.GameObject
  /// </summary>
	[Serializable]
	public class GlobalGameObjectRef : GlobalRef<GlobalGameObject, UnityEngine.GameObject> { }

}



