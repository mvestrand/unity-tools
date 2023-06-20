using System.Collections.Generic;

using UnityEngine;



#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#else
using MVest.Unity.OdinInspector;
#endif


namespace MVest.Unity.Pooling {
	public interface IRestartable {
        void Restart(IRestartable original);
    }

	public sealed class PooledMonoBehaviour : MonoBehaviour {

		[FoldoutGroup("Pooling Settings")]
		[Tooltip("The hierarchy path to store pooled objects in (the Pool Manager is the root)")]
		[SerializeField] private string _path;
		[FoldoutGroup("Pooling Settings")]
		[Tooltip("Minimum size of the pool including active objects")]
		[SerializeField] private int _minPoolSize = 0;
		[FoldoutGroup("Pooling Settings")]
		[Tooltip("Maximum size of the pool including active objects")]
		[SerializeField] private int _maxPoolSize = 10000;
		[ShowInInspector] internal GameObjectPool _pool;
		private PooledMonoBehaviour _prototype;
		public PooledMonoBehaviour Prototype { get { return (_prototype != null ? _prototype : this); } }
		internal PooledMonoBehaviour nextPooledObject;
		internal PooledMonoBehaviour prevPooledObject;


		public GameObjectPool Pool {
			get {
				if (_pool == null) {
					GetPool();
				}
				return _pool;
			}
		}

		private GameObjectPool GetPool() {
			if (_pool == null) {
				if (GameObjectPoolManager.Instance == null) {
					return null; // We are not using pooling
				}
				_pool = GameObjectPoolManager.Instance.GetPool(this);
			}
			return _pool;
		}

		internal void _SetPrototype(PooledMonoBehaviour prototype) {
			_prototype = prototype;
		}

		public int MinPoolSize { get { return _minPoolSize; } }
		public int MaxPoolSize { get { return _maxPoolSize; } }

		public int PrefabID {
			get { return (_pool != null ? _pool.Prototype.gameObject.GetInstanceID() : this.gameObject.GetInstanceID()); }
		}

		public string PrefabName {
			get { return (_pool != null ? _pool.Prototype.gameObject.name : this.gameObject.name); }
		}

		public string PrefabPath {
			get { return (_pool != null ? _pool.Prototype._path : this._path); }
		}

		public PooledMonoBehaviour Get() {
			return (Pool != null ? Pool.Get() : Instantiate<PooledMonoBehaviour>(this));
		}


		public PooledMonoBehaviour Get(Transform parent) {
			return (Pool != null ? Pool.Get(parent) : Instantiate<PooledMonoBehaviour>(this, parent));
		}

		public PooledMonoBehaviour Get(Vector3 position, Quaternion rotation) {
			return (Pool != null ? Pool.Get(position, rotation) : Instantiate<PooledMonoBehaviour>(this, position, rotation));
		}

		public PooledMonoBehaviour Get(Vector3 position, Quaternion rotation, Transform parent) {
			return (Pool != null ? Pool.Get(position, rotation, parent) : Instantiate<PooledMonoBehaviour>(this, position, rotation, parent));
		}


		// public T Get<T>() where T : PooledMonoBehaviour {
		// 	return (T)Get();
		// }

		// public T Get<T>(Transform parent) where T : PooledMonoBehaviour {
		// 	return (T)Get(parent);
		// }

		// public T Get<T>(Vector3 position, Quaternion rotation) where T : PooledMonoBehaviour {
		// 	return (T)Get(position, rotation);
		// }

		// public T Get<T>(Vector3 position, Quaternion rotation, Transform parent) where T : PooledMonoBehaviour {
		// 	return (T)Get(position, rotation, parent);
		// }

		public void Release() {
			if (_pool != null) {
				if (GameObjectPoolManager.Instance.CollectionChecks)
					_pool.CheckIntoPool(this, StackTraceUtility.ExtractStackTrace());
				_pool.Release(this);
			} else {
				Destroy(this.gameObject);
			}
		}


		public void RequestPreallocate(int number) {
			GetPool();
			this._pool?.RequestPreallocate(number);
		}

		public void CancelPreallocate(int number) {
			this._pool?.CancelPreallocate(number);
		}

		private void OnDestroy() {
			if (_pool != null)
				_pool.RemoveFromPool(this);

			// Guarantee that these are ACTUALLY set to null to prevent cyclic references
			nextPooledObject = null;
			prevPooledObject = null;
		}

		public void Restart() {
            ResetComponents(this.gameObject, _prototype.gameObject);
        }



    	static List<IRestartable> instanceRestartables = new List<IRestartable>();
    	static List<IRestartable> originalRestartables = new List<IRestartable>();

		public static void ResetComponents(GameObject instance, GameObject original) {
            instanceRestartables.Clear();
            originalRestartables.Clear();
            instance.GetComponentsInChildren<IRestartable>(false, instanceRestartables);
            original.GetComponentsInChildren<IRestartable>(false, originalRestartables);
            if (instanceRestartables.Count != originalRestartables.Count) {
                Debug.LogError($"Cannot reset instance object {instance.ToString()} to original {original.ToString()} because of structural differences.");
                return;
            }
            for (int i = 0; i < instanceRestartables.Count; i++) {
                instanceRestartables[i].Restart(originalRestartables[i]);
            }
            //Debug.Log("Resetting components");
        }








	}

}
