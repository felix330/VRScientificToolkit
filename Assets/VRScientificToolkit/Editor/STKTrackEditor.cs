using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

[CustomEditor(typeof(STKTrackObject))]
public class STKTrackEditor : Editor {

    public GameObject lastTrackedObject;
    STKTrackObject currentTarget;

    void OnEnable()
    {
        currentTarget = (STKTrackObject)target;
    }

    public override void OnInspectorGUI()
    {
        currentTarget.trackedObject = (GameObject)EditorGUILayout.ObjectField("Object to track: ",currentTarget.trackedObject,typeof(GameObject),true);
        

        if (currentTarget.trackedObject != null)
        {
            if (currentTarget.trackedObject != lastTrackedObject)
            {
                currentTarget.trackedComponents = new bool[currentTarget.trackedObject.GetComponents(typeof(Component)).Length];
                currentTarget.trackedVariables = new bool[currentTarget.trackedObject.GetComponents(typeof(Component)).Length][];
            }
            //Cycle through components of the tracked object
            for (int i = 0; i < currentTarget.trackedObject.GetComponents(typeof(Component)).Length; i++)
            {
                Component c = currentTarget.trackedObject.GetComponents(typeof(Component))[i];
                if (c.GetType() != typeof(STKTrackObject))
                {
                    EditorStyles.label.fontStyle = FontStyle.Bold;
                    currentTarget.trackedComponents[i] = EditorGUILayout.Toggle(c.GetType().ToString(), currentTarget.trackedComponents[i]);
                    EditorStyles.label.fontStyle = FontStyle.Normal;
                    if (currentTarget.trackedObject != lastTrackedObject)
                    {
                        currentTarget.trackedVariables[i] = new bool[c.GetType().GetProperties().Length + c.GetType().GetFields().Length];
                        Debug.Log("Setting array");
                    }
                    //Cycle through variables
                    if (currentTarget.trackedComponents[i] == true)
                    {
                        EditorGUI.indentLevel++;
                        for (int j = 0; j < c.GetType().GetProperties().Length; j++)
                        {
                            var varToCheck = c.GetType().GetProperties()[j];
                            if (STKEventTypeChecker.IsValid(varToCheck.PropertyType))
                            {
                                currentTarget.trackedVariables[i][j] = EditorGUILayout.Toggle(varToCheck.Name, currentTarget.trackedVariables[i][j]);
                            }
                        }

                        for (int j = c.GetType().GetProperties().Length; j < c.GetType().GetFields().Length + c.GetType().GetProperties().Length; j++)
                        {
                            var varToCheck = c.GetType().GetFields()[j];
                            if (STKEventTypeChecker.IsValid(varToCheck.FieldType))
                            {
                                currentTarget.trackedVariables[i][j] = EditorGUILayout.Toggle(varToCheck.Name, currentTarget.trackedVariables[i][j]);
                            }
                        }
                        EditorGUI.indentLevel--;
                    }

                }
            }
        }
        

        lastTrackedObject = currentTarget.trackedObject;
    }

}
