using System;

using UnityEngine;
using UnityEditor;


#if ODIN_INSPECTOR

using Sirenix.OdinInspector.Editor;

#endif


namespace MVest.Unity {

[CustomPropertyDrawer(typeof(NotNullAttribute))]
public class NotNullDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        bool isNull = property.objectReferenceValue == null;
        if (isNull)
        {
            Color prevColor = GUI.color;
            GUI.color = Color.red;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.color = prevColor;
        }
        else
        {
            EditorGUI.PropertyField(position, property, label, true);
        }
    }
}



#if ODIN_INSPECTOR

public class NotNullOdinDrawer : OdinAttributeDrawer<NotNullAttribute>
{

    protected override void DrawPropertyLayout(GUIContent label)
    {
        bool isNull = (this.Property.ValueEntry.WeakSmartValue == null);
        if (isNull)
        {
            Color prevColor = GUI.color;
            GUI.color = Color.red;
            this.CallNextDrawer(label);
            GUI.color = prevColor;
        }
        else
        {
            this.CallNextDrawer(label);
        }

    }
}

#endif



}