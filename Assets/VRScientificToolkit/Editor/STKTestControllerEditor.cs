using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(STKTestController))]
public class STKTestControllerEditor : Editor
{
    private string[] newNames;

    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();

        STKTestController myTarget = (STKTestController)target;
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());

        if (GUILayout.Button(new GUIContent("Add Stage", "Add a stage to your experiment. Different stages can have different attributes.")))
        {
            myTarget.AddStage();
        }
        
    }
}
