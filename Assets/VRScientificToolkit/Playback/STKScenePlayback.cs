using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

//Sceneplayback recreates Scene states from JSON data
public static class STKScenePlayback {

    public static bool playbackMode;
    private static List<GameObject> trackedObjects;
    private static JSONNode parsedJson;
    private static int stage;

	public static void StartPlayback(string json, int stageToPlay)
    {
        playbackMode = true;
        trackedObjects = GameObject.Find("STKTrackedObjects").GetComponent<STKTrackedObjects>().trackedObjects;
        parsedJson = JSON.Parse(json);
        stage = stageToPlay;
    }

    public static void GoToPoint(float t)
    {
        foreach (GameObject g in trackedObjects)
        {
            if (g.GetComponent<STKEventSender>() != null && g.GetComponent<STKEventSender>().eventBase != null)
            {
                STKEvent eventBase = g.GetComponent<STKEventSender>().eventBase;
                foreach (EventParameter param in eventBase.parameters)
                {
                    Debug.Log(parsedJson[("Stage"+stage.ToString())][eventBase.eventName][param.name]);
                }
            }
        }
    }

    public static void StopPlayback()
    {
        playbackMode = false;
    }
}
