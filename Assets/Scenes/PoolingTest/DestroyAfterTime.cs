using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MVest.Unity.Pooling;

namespace MVest.Unity.Pooling.Demo
{

    public class DestroyAfterTime : MonoBehaviour, IRestartable
    {
        [SerializeField] float lifetime = 3f;
        [SerializeField] float timeAlive = 0f;

        public void Restart(IRestartable original) {
            DestroyAfterTime o = original as DestroyAfterTime;
            lifetime = o.lifetime;
            timeAlive = o.timeAlive;
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