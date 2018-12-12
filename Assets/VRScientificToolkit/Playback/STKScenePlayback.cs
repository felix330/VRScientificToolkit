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
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        playbackMode = true;
        trackedObjects = GameObject.Find("STKTrackedObjects").GetComponent<STKTrackedObjects>().trackedObjects;
        parsedJson = JSON.Parse(json);
        stage = stageToPlay;
    }

    public static void GoToPoint(float t)
    {
        foreach (GameObject g in trackedObjects) // Goes through events with Eventsenders and finds the respective events in the JSON file
        {
            if (g.GetComponent<STKEventSender>() != null && g.GetComponent<STKEventSender>().eventBase != null)
            {
                STKEvent eventBase = g.GetComponent<STKEventSender>().eventBase;
                JSONNode currentEvent = parsedJson;
                JSONNode events = parsedJson[("Stage" + stage.ToString())][eventBase.eventName];
                for (int i = 0; i < events.Count; i++)
                {
                    if (events[i]["time"] >= t) //Finds event closest in time to point that will be restored
                    {
                        currentEvent = events[i];
                        i = events.Count;
                    }
                }
                foreach (EventParameter param in eventBase.parameters)
                {
                    Component component = g.GetComponent<STKEventSender>().GetComponentFromParameter(param.name);
                    string name = g.GetComponent<STKEventSender>().GetVariableNameFromEventVariable(param.name);
                    SetVariable(currentEvent[param.name], name, component, g);
                }
            }
        }
    }

    public static void SetVariable(JSONNode node, string name, Component c, GameObject g)
    {
        if (node.IsArray)
        {
            Debug.Log("Array Detected " + node.Count);
            switch (node.Count)
            {
                case 2:
                    break;
                case 3:
                    Vector3 v = new Vector3(node[0],node[1],node[2]);
                    if (c.GetType().GetField(name) != null)
                    {
                        c.GetType().GetField(name).SetValue(c, v);
                    } else
                    {
                        c.GetType().GetProperty(name).SetValue(c, v);
                    }
                    break;
                case 4:
                    break;

            }
        }
    }

    public static void StopPlayback()
    {
        playbackMode = false;
    }
}
