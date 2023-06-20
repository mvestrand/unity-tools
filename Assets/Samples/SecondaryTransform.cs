using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using MVest.Unity;

public class SecondaryTransform : MonoBehaviour {


    public Vector3 a;
    public Vector3 b;
    public FloatTransform tf;
    
    void OnDrawGizmos() {
        tf.SetFromLocalTransform(transform);

        Handles.color = Color.blue;
        Handles.DrawLine(Vector3.zero, transform.TransformPoint(a), 8);
        Handles.color = Color.red;
        Handles.DrawLine(Vector3.zero, tf.TransformPoint(a), 4);

        Handles.color = Color.green;
        Handles.DrawLine(Vector3.zero, transform.InverseTransformPoint(b), 8);
        Handles.color = Color.yellow;
        Handles.DrawLine(Vector3.zero, tf.InverseTransformPoint(b), 4);

    }


}
