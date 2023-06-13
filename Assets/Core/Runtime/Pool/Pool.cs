using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MVest.Unity.Pooling {
    public static class Pool {

        public static GameObject Instantiate(GameObject original) {
            if (original.TryGetComponent<PooledMonoBehaviour>(out var pooling)) {
                return pooling.Get().gameObject;
            } else {
                return Instantiate(original);
            }
        }

        public static GameObject Instantiate(GameObject original, Transform parent) {
            if (original.TryGetComponent<PooledMonoBehaviour>(out var pooling)) {
                return pooling.Get(parent).gameObject;
            } else {
                return Instantiate(original, parent);
            }            
        }

        public static GameObject Instantiate(GameObject original, Vector3 position, Quaternion rotation) {
            if (original.TryGetComponent<PooledMonoBehaviour>(out var pooling)) {
                return pooling.Get(position, rotation).gameObject;
            } else {
                return Instantiate(original, position, rotation);
            }            
        }

        public static GameObject Instantiate(GameObject original, Vector3 position, Quaternion rotation, Transform parent) {
            if (original.TryGetComponent<PooledMonoBehaviour>(out var pooling)) {
                return pooling.Get(position, rotation, parent).gameObject;
            } else {
                return Instantiate(original, position, rotation, parent);
            }            
        }

        public static void Release(GameObject gameObject) {
            if (gameObject.TryGetComponent<PooledMonoBehaviour>(out var pooling)) {
                pooling.Release();
            } else {
                GameObject.Destroy(gameObject);
            }

        }

    }
}