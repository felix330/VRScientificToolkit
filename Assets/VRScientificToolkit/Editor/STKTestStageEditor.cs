using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(STKTestStage))]
public class STKTestStageEditor : Editor {

    public string newName;

    public override void OnInspectorGUI()
    {
        if (!EditorApplication.isPlaying)
        {
            STKTestStage myTarget = (STKTestStage)target;
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());

            EditorGUILayout.LabelField("Add a new Property:");
            newName = EditorGUILayout.TextField("Name of new Property: ", newName);

            if (newName != null && newName != "")
            {
                if (GUILayout.Button("Add Property"))
                {
                    myTarget.AddProperty(newName);
                }
            }
            else
            {
                GUILayout.Label("Please choose a name before adding a new property.");
            }
            EditorGUILayout.Space();

            base.OnInspectorGUI();
        }
    }

}
