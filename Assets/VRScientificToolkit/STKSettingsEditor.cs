using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(STKSettings))]
public class STKSettingsEditor : Editor {

    public override void OnInspectorGUI()
    {
        STKSettings myTarget = (STKSettings)target;
        base.OnInspectorGUI();

        if (!myTarget.useDataReduction)
        {
            myTarget.useSlidingWindow = EditorGUILayout.Toggle(new GUIContent("Use Sliding Window", "When the maximum event number is reached, an event will be removed from the beginning for each new event added."), myTarget.useSlidingWindow);
        }
        if (!myTarget.useSlidingWindow)
        {
            myTarget.useDataReduction = EditorGUILayout.Toggle(new GUIContent("Use Data Reduction", "When the maximum event number is reached, every second currently stored event will be removed, reducing the precision of earlier data without removing it entirely."), myTarget.useDataReduction);
        }
    }
}
