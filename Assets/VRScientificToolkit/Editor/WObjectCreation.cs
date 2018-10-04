using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WObjectCreation : EditorWindow {

    public bool addAnalytics = true;

	[MenuItem("Window/VR Scientific Toolkit/Object Creation")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(WObjectCreation));
    }

    private void OnGUI()
    {
        EditorGUILayout.Space();
        GUILayout.Label("Player Entity", EditorStyles.boldLabel);
        addAnalytics = GUILayout.Toggle(addAnalytics, "Add Analytics");
        GUILayout.Button("Create Player Entity");
    }

    [MenuItem("Window/VR Scientific Toolkit/Create Test Event")]
    public static void CreateEvent()
    {
        STKEvent newEvent = new STKEvent
        {
            name = "TestName"
        };

        ProjectWindowUtil.CreateAsset(newEvent,"Assets/VRScientificToolkit/Events/"+newEvent.name);
    }
}
