using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBehaviour : MonoBehaviour, ITestInterface {
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public string TestMsg() {
      return "Hello";
    }
}


public interface ITestInterface {
  string TestMsg();
}