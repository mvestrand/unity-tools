using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MVest.Unity.Pool;

namespace MVest.Unity.Pool.Demo
{

    public class DestroyAfterTime : MonoBehaviour
    {
        [SerializeField] float lifetime = 3f;
        [SerializeField] float timeAlive = 0f;

        void OnEnable()
        {
            timeAlive = 0f;
        }

        // Update is called once per frame
        void Update()
        {
            timeAlive += Time.deltaTime;
            if (timeAlive >= lifetime)
            {
                if (gameObject.TryGetComponent<PooledMonoBehaviour>(out var pooling))
                {
                    pooling.Release();
                }
                else
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}