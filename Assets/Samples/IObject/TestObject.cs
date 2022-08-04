using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour
{
	// [SerializeField]
	// [MVest.Unity.RequireInterface(typeof(ITestInterface))]
	// private Object test;


		[SerializeField]
		private MVest.Unity.IObject<ITestInterface> testIObject;


		// Start is called before the first frame update
		void Start()
		{
			var tmp = testIObject;
			Debug.Log(testIObject.i?.TestMsg());

			testIObject.i = new TestManagedObject();
			Debug.Log(testIObject.i?.TestMsg()); // Should be null
			testIObject = tmp;

			Debug.Log(testIObject.i?.TestMsg());

		}

		// Update is called once per frame
		void Update()
		{
				
		}
}


public class TestManagedObject : ITestInterface {
	public string TestMsg() {
		return "Hello from managed object";
	}
}