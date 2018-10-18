using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(STKTrackObject))]
public class STKTrackEditor : Editor {
    

    void OnEnable()
    {

    }

    public override void OnInspectorGUI()
    {
        STKTrackObject currentTarget = (STKTrackObject)target;
        currentTarget.trackedObject = (GameObject)EditorGUILayout.ObjectField("Object to track: ",currentTarget.trackedObject,typeof(GameObject),true);

        foreach (Component c in currentTarget.trackedObject.GetComponents(typeof(Component)))
        {
            if (c.GetType() != typeof(STKTrackObject))
            {
                EditorStyles.label.fontStyle = FontStyle.Bold;
                EditorGUILayout.Toggle(c.GetType().ToString(), false);
                EditorStyles.label.fontStyle = FontStyle.Normal;
                EditorGUI.indentLevel++;
                foreach (var varToCheck in c.GetType().GetProperties())
                {
                    if (STKEventTypeChecker.IsValid(varToCheck.PropertyType))
                    {
                        EditorGUILayout.Toggle(varToCheck.Name, false);
                    }
                }
                EditorGUI.indentLevel--;
            }
        }
    }

}
