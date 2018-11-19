using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
//{\"Tests\": \n[
//]}

public static class STKJsonParser {

    private static string startString;
    private static string eventString;
    private static string endString;
    private static string[] stageString;
    private static int currentStage = 0;

	
    public static void TestStart(Hashtable p)
    {
        if (stageString == null)
        {
            stageString = new string[STKTestController.numberOfStages];
        }
        int i = 0;
        startString = "{\n";
        foreach(string s in p.Keys)
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
        eventString = "";
        foreach (string s in events.Keys)
        {
            eventString += ",\n";
            Debug.Log(s);
            List<STKEvent> eventList = (List<STKEvent>)events[s];
            eventString += "\"" + eventList[0].eventName + "\":\n[\n";
            int eventListIndex = 0;
            foreach (STKEvent e in eventList)
            {
                eventString += "{\n\"uniqueID\": " + e.uniqueID + ",\n";
                eventString += "\"time\": " + e.time + ",\n";
                int objectsIndex = 0;
                foreach (string o in e.objects.Keys)
                {
                    eventString += "\"" + o + "\": " + FormatObject(e.objects[o]) + ""; //TODO: Unterschiedung nach Datentyp
                    if (objectsIndex < e.objects.Keys.Count-1)
                    {
                        eventString += ",\n";
                    }
                    objectsIndex++;
                }
                eventString += "\n}";
                if (eventListIndex < eventList.Count-1)
                {
                    eventString += ",\n";
                }
                eventListIndex++;
            }
            eventString += "]\n";
        }
    }

    private static string FormatObject(System.Object o)
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
        return "";
    }

    public static void TestEnd()
    {
        endString = "}\n";
        stageString[currentStage] = startString + eventString + endString;
        currentStage++;
        if (currentStage >= stageString.Length)
        {
            Debug.Log("Creating final");
            CreateFile();
        }
    }

    public static string CreateFile()
    {
        string fullString = "{\"Tests\": \n[";
        for (int i = 0; i < stageString.Length; i++)
        {
            fullString += stageString[i];
            if (i < stageString.Length-1)
            {
                fullString += ",\n";
            }
        }
        fullString += "]}";
        string path = (Application.persistentDataPath + "\\" + System.DateTime.Now.Month + "-" + System.DateTime.Now.Day + "_" + System.DateTime.Now.Hour + "-" + System.DateTime.Now.Minute + "-" + System.DateTime.Now.Second + ".txt");
        using (StreamWriter sw = File.AppendText(path))
        {
            sw.Write(fullString);
        }
        return fullString;
    }
}
