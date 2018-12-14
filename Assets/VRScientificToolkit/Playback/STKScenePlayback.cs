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
        Debug.Log(parsedJson);
        stage = stageToPlay;
        //DeactivateAllComponents();
    }

    public static void DeactivateAllComponents()
    {
        GameObject[] allGameObjects = Object.FindObjectsOfType<GameObject>();

        foreach (GameObject go in allGameObjects)
        {
            MonoBehaviour[] components = go.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour c in components)
            {
                c.enabled = false;
            }
            Rigidbody[] rigidbodies = go.GetComponents<Rigidbody>();
            foreach (Rigidbody r in rigidbodies)
            {
                r.isKinematic = true;
            }
        }
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
                if (events != null)
                {
                    if (g.activeSelf == false)
                    {
                        g.SetActive(true);
                        MonoBehaviour[] components = g.GetComponents<MonoBehaviour>();
                        foreach (MonoBehaviour c in components)
                        {
                            c.enabled = false;
                        }
                        Rigidbody[] rigidbodies = g.GetComponents<Rigidbody>();
                        foreach (Rigidbody r in rigidbodies)
                        {
                            r.isKinematic = true;
                        }
                    }
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
                } else
                {
                    g.SetActive(false);
                }
            }
        }
    }

    public static void SetVariable(JSONNode node, string name, Component c, GameObject g)
    {
        if (node.IsArray)
        {
            switch (node.Count)
            {
                case 2:
                    Vector2 v2 = new Vector2(node[0], node[1]);
                    if (c.GetType().GetField(name) != null)
                    {
                        c.GetType().GetField(name).SetValue(c, v2);
                    }
                    else
                    {
                        c.GetType().GetProperty(name).SetValue(c, v2);
                    }
                    break;
                case 3:
                    Vector3 v3 = new Vector3(node[0],node[1],node[2]);
                    if (c.GetType().GetField(name) != null)
                    {
                        c.GetType().GetField(name).SetValue(c, v3);
                    } else
                    {
                        c.GetType().GetProperty(name).SetValue(c, v3);
                    }
                    break;
                case 4:
                    if (c.GetType().GetField(name) != null)
                    {
                        if (c.GetType().GetField(name).GetValue(c).GetType() == typeof(Vector4))
                        {
                            Vector4 v4 = new Vector4(node[0], node[1], node[2], node[3]);
                            c.GetType().GetField(name).SetValue(c, v4);
                        } else if (c.GetType().GetField(name).GetValue(c).GetType() == typeof(Quaternion))
                        {
                            Quaternion q = new Quaternion(node[0], node[1], node[2], node[3]);
                            c.GetType().GetField(name).SetValue(c, q);
                        }
                    }
                    else
                    {
                        if (c.GetType().GetProperty(name).GetValue(c).GetType() == typeof(Vector4))
                        {
                            Vector4 v4 = new Vector4(node[0], node[1], node[2], node[3]);
                            c.GetType().GetProperty(name).SetValue(c, v4);
                        }
                        else if (c.GetType().GetProperty(name).GetValue(c).GetType() == typeof(Quaternion))
                        {
                            Quaternion q = new Quaternion(node[0], node[1], node[2], node[3]);
                            c.GetType().GetProperty(name).SetValue(c, q);
                        }
                    }
                    break;
            }
        } else if (node.IsBoolean)
        {
            bool b = node;
            if (c.GetType().GetField(name) != null)
            {
                c.GetType().GetField(name).SetValue(c, b);
            } else
            {
                c.GetType().GetProperty(name).SetValue(c, b);
            }
        } else if (node.IsNumber)
        {
            float f = node;
            if (c.GetType().GetField(name) != null)
            {
                if (c.GetType().GetField(name).GetValue(c).GetType() == typeof(int))
                {
                    int i = (int)f;
                    c.GetType().GetField(name).SetValue(c, i);
                } else
                {
                    c.GetType().GetField(name).SetValue(c, f);
                }
            }
            else
            {
                if (c.GetType().GetProperty(name).GetValue(c).GetType() == typeof(int))
                {
                    int i = (int)f;
                    c.GetType().GetProperty(name).SetValue(c, i);
                }
                else
                {
                    c.GetType().GetProperty(name).SetValue(c, f);
                }
            }
        } else if (node.IsString)
        {
            string s = node;
            if (c.GetType().GetField(name) != null)
            {
                c.GetType().GetField(name).SetValue(c, s);
            }
            else
            {
                c.GetType().GetProperty(name).SetValue(c, s);
            }
        }
    }

    public static void StopPlayback()
    {
        playbackMode = false;
    }
}
