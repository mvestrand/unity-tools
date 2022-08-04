using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "tmp/Scriptable Object with Interface")]
public class TestScriptableObject :ScriptableObject, ITestInterface {
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public string TestMsg() {
        return "Hello from Scriptable";
    }
}
