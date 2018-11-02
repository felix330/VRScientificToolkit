using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class STKJsonParser {

    private static string startString;
    private static string[] eventStrings;
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
    }

    public static void ReceiveEvents(Hashtable events)
    {

    }

    public static void TestEnd()
    {

    }
}
