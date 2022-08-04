using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVest.Unity {
    [System.Serializable]//[InlineProperty]
    public struct IObject<TInterface> where TInterface : class {
        [SerializeField]
        // [RequireInterface(typeof(TInterface))]
        private Object _value;

        public IObject(TInterface value) {
            _value = value as Object;
        }

        public static implicit operator TInterface(IObject<TInterface> iObj) => iObj._value as TInterface;
		public static implicit operator IObject<TInterface>(TInterface value) => new IObject<TInterface>(value);

    }

}