using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MVest.Unity {

[CustomPropertyDrawer(typeof(RequireInterfaceAttribute))]
public class RequireInterfaceAttributeDrawer : PropertyDrawer {

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        
        if (property.propertyType == SerializedPropertyType.ObjectReference) { // Verify the attribute is on an object reference
            var requiredAttribute = this.attribute as RequireInterfaceAttribute;
            
            EditorGUI.BeginProperty(position, label, property);

            GUIContent newLabel = new GUIContent(label);
            newLabel.text += " (" + requiredAttribute.requiredType.ToString()+")";

            Object reference = property.objectReferenceValue;
            Component comp;

            var obj = EditorGUI.ObjectField(position, newLabel, property.objectReferenceValue, typeof(Object), true);
            if (obj != null) {
                if (requiredAttribute.requiredType.IsAssignableFrom(obj.GetType())) {
                    reference = obj;
                } else if (obj is GameObject g && (comp = g.GetComponent(requiredAttribute.requiredType)) != null) {
                    reference = comp;
                }
            } else {
                reference = obj;
            }

            property.objectReferenceValue = reference;

            EditorGUI.EndProperty();
        } else {
            EditorGUI.HelpBox(new Rect(position.xMin, position.yMin, position.xMax, 30f), $"{property.name} is not a Unity.Object reference. RequireInterface can only be used on Unity.Object reference types", MessageType.Error);
            EditorGUI.PropertyField(new Rect(position.xMin, position.yMin + 30f, position.xMax, position.yMax), property, label, true);
        }
    }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            if (property.propertyType != SerializedPropertyType.ObjectReference) {
                return EditorGUI.GetPropertyHeight(property, label) + 30f;  
            }
            return EditorGUI.GetPropertyHeight(property, label);
        }

    }

}

