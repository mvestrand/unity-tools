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
        Debug.Log(testIObject.GetType().ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
