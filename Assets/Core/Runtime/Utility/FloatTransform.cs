using UnityEngine;

namespace MVest.Unity {

[System.Serializable]
public struct FloatTransform {
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;


    public FloatTransform(Vector3 position, Quaternion rotation, Vector3 scale) {
        this.position = position;
        this.rotation = rotation;
        this.scale = scale;
    }

    public FloatTransform(Transform transform, bool local = true) {
        if (local) {
            position = transform.localPosition;
            rotation = transform.localRotation;
            scale = transform.localScale;
        } else {
            position = transform.position;
            rotation = transform.rotation;
            scale = transform.lossyScale;
        }
    }

    public void SetFromLocalTransform(Transform transform) {
        position = transform.position;
        rotation = transform.rotation;
        scale = transform.localScale;
    }

    public void SetLocalTransform(Transform transform) {
        transform.localPosition = position;
        transform.localRotation = rotation;
        transform.localScale = scale;
    }

    public Vector3 TransformPoint(Vector3 point) {
        return rotation * Vector3.Scale(point, scale) + position;
    }

    public Vector3 InverseTransformPoint(Vector3 point) {
        return InverseScale(Quaternion.Inverse(rotation) * (point - position), scale);
    }

    public Vector3 TransformDirection(Vector3 direction) {
        return rotation * direction;
    }

    public Vector3 InverseTransformDirection(Vector3 direction) {
        return Quaternion.Inverse(rotation) * direction;
    }

    public Vector3 TransformVector(Vector3 vector) {
        return rotation * Vector3.Scale(vector, scale);
    }

    public Vector3 InverseTransformVector(Vector3 vector) {
        return InverseScale(Quaternion.Inverse(rotation) * vector, scale);
    }




    private static Vector3 InverseScale(Vector3 point, Vector3 scale) {
        return new Vector3(point.x / scale.x, point.y / scale.y, point.z / scale.z);
    }
}

}

