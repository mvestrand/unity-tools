using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVest.Unity {


    public static class UnityObjectExtensions {

        /// <summary>
        /// Get the full name of a unity object. Can give info on destroyed objects (with errors).
        /// </summary>
        public static string FullName(this UnityEngine.Object obj) {
            if (obj is GameObject) {
                return ((GameObject)obj).HierarchyName();
            } else if (obj is Component) {
                return ((Component)obj).gameObject.HierarchyName() + ":" + obj.GetType().Name;
            } else if (obj == null) {
                return "null";
            } else {
                return obj.name;
            }
        }

        /// <summary>
        /// Get the object's fullname and instance ID
        /// </summary>
        public static string DebugName(this UnityEngine.Object obj) {
            if (obj != null) {
                return obj.FullName() + "[" + obj.GetInstanceID() + "]";
            } else {
                return "null";
            }
        }

    }

}
