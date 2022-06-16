using System;
using UnityEngine;


namespace MVest.Unity.Global {

	[CreateAssetMenu(menuName = "Global/Bool", order = 5)]
	public class GlobalBool : GlobalVariable<bool> { }

	[Serializable]
	public class GlobalBoolRef : GlobalRef<GlobalBool, bool> { }

}
