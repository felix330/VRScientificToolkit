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
    }

}
