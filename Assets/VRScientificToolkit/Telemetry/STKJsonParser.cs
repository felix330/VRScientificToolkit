using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class STKJsonParser {

    private static string startString;
    private static string eventString;
    private static string endString;
    private static string[] stageString;
    private static int currentStage = 0;

    private static STKSettings settings = Resources.Load<STKSettings>("STKSettings");


    public static void TestStart(Hashtable p)
    {
        if (stageString == null)
        {
            stageString = new string[STKTestController.numberOfStages];
        }
        int i = 0;
        eventString = "";
        startString = "{\n";
        startString += "\"TimeStarted\": \"" + System.DateTime.Now.Hour + ":" + System.DateTime.Now.Minute + ":" + System.DateTime.Now.Second + "\", \n";
        startString += "\"DateStarted\": \"" + System.DateTime.Now.Year + "." + System.DateTime.Now.Month + "." + System.DateTime.Now.Day + "\"";
        if (p.Count > 0) startString += "\n,";
        foreach (string s in p.Keys)
        {
            startString += "\"" + s + "\": " + FormatObject(p[s]);
            if ( i<p.Keys.Count-1)
            {
                startString += ", \n";
            }
            i++;
        }
    }

    public static void ReceiveEvents(Hashtable events)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        System.Text.StringBuilder sb = new System.Text.StringBuilder("");
        foreach (string s in events.Keys)
        {
            sb.Append(",\n");
            List<STKEvent> eventList = (List<STKEvent>)events[s];
            sb.Append("\"").Append(eventList[0].eventName).Append("\":\n[\n");
            int eventListIndex = 0;
            foreach (STKEvent e in eventList)
            {
                sb.Append("{\n\"uniqueID\": ").Append(e.uniqueID).Append(",\n");
                sb.Append("\"time\": ").Append(e.time).Append(",\n");
                int objectsIndex = 0;
                foreach (string o in e.objects.Keys)
                {
                    sb.Append("\"").Append(o).Append("\": ").Append(FormatObject(e.objects[o])).Append(""); //TODO: Unterschiedung nach Datentyp
                    if (objectsIndex < e.objects.Keys.Count-1)
                    {
                        sb.Append(",\n");
                    }
                    objectsIndex++;
                }
                sb.Append("\n}");
                if (eventListIndex < eventList.Count-1)
                {
                    sb.Append(",\n");
                }
                eventListIndex++;
            }
            sb.Append("]\n");
        }
        eventString = sb.ToString();
    }

    private static string FormatObject(System.Object o)
    {
        if (o != null)
        {

            if (o.GetType() == typeof(string))
            {
                string returnString = "\"" + o + "\"";
                return returnString;
            }
            else if (o.GetType() == typeof(int) || o.GetType() == typeof(float) || o.GetType() == typeof(double))
            {
                return o.ToString();
            }
            else if (o.GetType() == typeof(bool))
            {
                string returnString = "\"" + o.ToString() + "\"";
                return returnString;
            }
            else if (o.GetType() == typeof(Vector2))
            {
                Vector2 v = (Vector2)o;
                string returnString = "[" + v.x + "," + v.y + "]";
                return returnString;
            }
            else if (o.GetType() == typeof(Vector3))
            {
                Vector3 v = (Vector3)o;
                string returnString = "[" + v.x + "," + v.y + "," + v.z + "]";
                return returnString;
            }
            else if (o.GetType() == typeof(Vector4))
            {
                Vector4 v = (Vector4)o;
                string returnString = "[" + v.w + "," + v.x + "," + v.y + "," + v.z + "]";
                return returnString;
            }
            else if (o.GetType() == typeof(Quaternion))
            {
                Quaternion v = (Quaternion)o;
                string returnString = "[" + v.w + "," + v.x + "," + v.y + "," + v.z + "]";
                return returnString;
            }
        }
        Debug.LogWarning("Formatting Object unsuccessful. Returning empty.");
        return "";
    }

    public static void TestEnd()
    {
        endString = "}\n";
        stageString[currentStage] = "\"Stage" + currentStage.ToString() + "\":" + startString + eventString + endString;
        currentStage++;
        if (currentStage >= stageString.Length)
        {
            Debug.Log("Creating final");
            CreateFile();
        }
    }

    public static string CreateFile()
    {
        string fullString = "{\n";
        for (int i = 0; i < stageString.Length; i++)
        {
            fullString += stageString[i];
            if (i < stageString.Length-1)
            {
                fullString += ",\n";
            }
        }
        fullString += "}";
        string path = (settings.jsonPath + "\\" + System.DateTime.Now.Month + "-" + System.DateTime.Now.Day + "_" + System.DateTime.Now.Hour + "-" + System.DateTime.Now.Minute + "-" + System.DateTime.Now.Second + ".json");
        using (StreamWriter sw = File.AppendText(path))
        {
            sw.Write(fullString);
        }
        return fullString;
    }
}
