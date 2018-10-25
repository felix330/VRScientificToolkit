using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(EventParameter))]
public class STKEventEditor : PropertyDrawer {

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        string[] choices = new string[STKEventTypeChecker.allowedTypes.Length];

        for (int i = 0; i < STKEventTypeChecker.allowedTypes.Length; i++)
        {
            choices[i] = STKEventTypeChecker.allowedTypes[i].ToString();
        }

        EditorGUI.indentLevel++;
        EditorGUI.PropertyField(new Rect(position.x, position.y, position.width, 17), property.FindPropertyRelative("name"));
        property.FindPropertyRelative("typeIndex").intValue = EditorGUI.Popup(new Rect(position.x, position.y + 20f, position.width, 17), property.FindPropertyRelative("typeIndex").intValue, choices);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 50.0f;
    }

}
