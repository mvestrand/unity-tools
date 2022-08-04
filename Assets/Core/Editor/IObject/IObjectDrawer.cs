using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace MVest.Unity {

	[CustomPropertyDrawer(typeof(IObject<>))]
	public class IObjectDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            var interfaceType = fieldInfo.FieldType.GenericTypeArguments[0];

            EditorGUI.BeginProperty(position, label, property);

			// Create prefix label with interface name appended
            GUIContent newLabel = new GUIContent(label);
            newLabel.text += " (" + interfaceType.ToString() + ")";

            var valProp = property.FindPropertyRelative("_value");

            Component comp;

			// Draw object field and update the reference if an object implementing the interface is given
            var newObj = EditorGUI.ObjectField(position, newLabel, valProp.objectReferenceValue, typeof(Object), true);
            if (newObj == null) {
                valProp.objectReferenceValue = newObj;

            } else if (interfaceType.IsAssignableFrom(newObj.GetType())) {
                valProp.objectReferenceValue = newObj;

            } else if (newObj is GameObject g && (comp = g.GetComponent(interfaceType)) != null) {
                valProp.objectReferenceValue = comp;
            }

            EditorGUI.EndProperty();        }

    }


}