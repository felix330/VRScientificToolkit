using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(STKTestStage))]
public class STKTestStageEditor : Editor {

    public string propertyName;
    public string buttonName;

    public override void OnInspectorGUI()
    {
        if (!EditorApplication.isPlaying)
        {
            STKTestStage myTarget = (STKTestStage)target;
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());

            EditorGUILayout.LabelField("Add a new Property:");
            propertyName = EditorGUILayout.TextField("Name of new Property: ", propertyName);

            if (propertyName != null && propertyName != "")
            {
                if (GUILayout.Button("Add Property"))
                {
                    myTarget.AddProperty(propertyName);
                }
            }
            else
            {
                GUILayout.Label("Please choose a name before adding a new property.");
            }
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Add a new Button:");
            buttonName = EditorGUILayout.TextField("Name of new Button: ", buttonName);

            if (buttonName != null && buttonName != "")
            {
                if (GUILayout.Button("Add Button"))
                {
                    myTarget.AddButton(buttonName);
                }
            }
            else
            {
                GUILayout.Label("Please choose a name before adding a new button.");
            }
            EditorGUILayout.Space();

            base.OnInspectorGUI();
        }
    }

}
