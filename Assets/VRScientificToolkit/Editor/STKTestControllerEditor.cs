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

        if (GUILayout.Button("Add Stage"))
        {
            myTarget.AddStage();
        }
        /*
        //Create Stages
        myTarget.numberOfStages = EditorGUILayout.IntField("Number of Stages",myTarget.numberOfStages);

        if (myTarget.testStages == null)
        {
            myTarget.testStages = new STKTestStage[myTarget.numberOfStages];
            for(int i = 0; i < myTarget.testStages.Length; i++)
            {
                myTarget.testStages[i] = new STKTestStage();
                myTarget.testStages[i].myController = myTarget;
            }
        }

        if (newNames == null)
        {
            newNames = new string[myTarget.numberOfStages];
        }

        if (myTarget.testStages.Length != myTarget.numberOfStages)
        {
            if (myTarget.testStages.Length < myTarget.numberOfStages)
            {
                STKTestStage[] oldStages = myTarget.testStages;
                myTarget.testStages = new STKTestStage[myTarget.numberOfStages];
                string[] oldNames = newNames;
                newNames = new string[myTarget.numberOfStages];
                for (int i = 0; i < myTarget.testStages.Length; i++)
                {
                    if (i < oldStages.Length)
                    {
                        myTarget.testStages[i] = oldStages[i];
                        newNames[i] = oldNames[i];
                    }
                    else
                    {
                        myTarget.testStages[i] = new STKTestStage();
                        myTarget.testStages[i].myController = myTarget;
                    }
                }
            } else
            {
                string[] oldNames = newNames;
                newNames = new string[myTarget.numberOfStages];
                STKTestStage[] oldStages = myTarget.testStages;
                myTarget.testStages = new STKTestStage[myTarget.numberOfStages];
                for (int i = 0; i < myTarget.numberOfStages; i++)
                {
                    myTarget.testStages[i] = oldStages[i];
                    newNames[i] = oldNames[i];
                }
            }
        }

        for (int i = 0; i < myTarget.numberOfStages; i++)
        {
            EditorGUILayout.LabelField("Stage" + (i+1) + ":");
            myTarget.testStages[i].hasTimeLimit = EditorGUILayout.Toggle("Has Time Limit: ", myTarget.testStages[i].hasTimeLimit);
            EditorGUILayout.LabelField("Add a new Property:");
            newNames[i] = EditorGUILayout.TextField("Name of new Property: ", newNames[i]);

            if (newNames[i] != null && newNames[i] != "")
            {
                if (GUILayout.Button("Add Property"))
                {
                    myTarget.testStages[i].AddProperty(newNames[i]);
                }
            }
            else
            {
                GUILayout.Label("Please choose a name before adding a new property.");
            }
            EditorGUILayout.Space();
        }
        */
    }
}
