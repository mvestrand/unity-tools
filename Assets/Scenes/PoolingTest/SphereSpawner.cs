using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MVest.Unity.Pool;

namespace MVest.Unity.Pool.Demo {

public class SphereSpawner : MonoBehaviour {


    [SerializeField] GameObject prefab = null;
    [SerializeField] float spawnTime = 1f;
    [SerializeField] float randomVelocity = 1f;
    float lastTime = 0f;

    // Start is called before the first frame update
    void OnEnable()
    {
        lastTime = Time.timeSinceLevelLoad;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad > lastTime+spawnTime && prefab != null) {
            lastTime = spawnTime + Time.timeSinceLevelLoad;
            GameObject obj;
            if (prefab.TryGetComponent<PooledMonoBehaviour>(out var pooling)) {
                obj = pooling.Get(transform.position, transform.rotation).gameObject;
            } else {
                obj = Instantiate(prefab, transform.position, transform.rotation);
            }
            if (randomVelocity > 0 && obj.TryGetComponent<Rigidbody>(out var rb)) {
                rb.velocity = randomVelocity * Random.insideUnitSphere;
            }
        }
    }
}
}


