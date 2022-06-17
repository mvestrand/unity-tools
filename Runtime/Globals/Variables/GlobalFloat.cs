using System;
using UnityEngine;


namespace MVest.Unity.Global {


	[CreateAssetMenu(menuName = "Global/Float", order = 3)]
	public class GlobalFloat : GlobalVariable<float> { }

	[Serializable]
	public class GlobalFloatRef : GlobalRef<GlobalFloat, float> { }

}
