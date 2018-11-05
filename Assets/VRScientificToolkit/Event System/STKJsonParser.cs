using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class STKJsonParser {

    private static string startString;
    private static string eventString;
    private static string endString;
	
    public static void TestStart(Hashtable p)
    {
        int i = 0;
        startString = "{\"Tests\": \n[{\n";
        foreach(string s in p.Keys)
        {
            startString += "\"" + s + "\": " + p[s].ToString();
            if ( i<p.Keys.Count-1)
            {
                startString += ", \n";
            }
            i++;
        }
        Debug.Log(startString);
    }

    public static void ReceiveEvents(Hashtable events)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        eventString = ",\n";
        foreach (string s in events.Keys)
        {
            Debug.Log(s);
            List<STKEvent> eventList = (List<STKEvent>)events[s];
            Debug.Log(eventList);
            eventString += eventList[0].eventName + ":\n[\n";
            foreach (STKEvent e in eventList)
            {
                eventString += "{\n\"uniqueID\": " + e.uniqueID + ",\n";
                eventString += "\"time\": " + e.time + ",\n";
                foreach (string o in e.objects.Keys)
                {
                    eventString += "\"" + o + "\": " + FormatObject(e.objects[o]) + ",\n"; //TODO: Unterschiedung nach Datentyp
                }
            }
        }
        Debug.Log(eventString);
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
            return o.ToString();
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

    }
}
