using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(STKTestController))]
public class STKTestControllerEditor : Editor
{
    private string newName;

    public override void OnInspectorGUI()
    {
        STKTestController myTarget = (STKTestController)target;
        newName = EditorGUILayout.TextField("Name of new Property: ",newName);

        if (newName != null && newName != "")
        {
            if (GUILayout.Button("Add Property"))
            {
                myTarget.AddProperty(newName);
            }
        } else
        {
            GUILayout.Label("Please choose a name before adding a new property.");
        }
    }
}
