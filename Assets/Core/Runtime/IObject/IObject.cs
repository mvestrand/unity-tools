using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVest.Unity {

    /// <summary>
    /// A reference to a unity object that implements the given interface.
    /// </summary>
    /// <typeparam name="TInterface">The required interface type.</typeparam>
    [System.Serializable]
    public struct IObject<TInterface> where TInterface : class {
        [SerializeField]
        private Object _value;

        public IObject(TInterface value) {
            _value = value as Object;
        }

        public TInterface i {
            get { return _value as TInterface; }
            set { _value = value as Object; }
        }

        // public static implicit operator TInterface(IObject<TInterface> iObj) => iObj._value as TInterface;
		// public static implicit operator IObject<TInterface>(TInterface value) => new IObject<TInterface>(value);

    }

}