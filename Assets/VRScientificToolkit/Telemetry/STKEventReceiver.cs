using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class STKEventReceiver {
    
    public static Hashtable savedEvents = new Hashtable();
    private static STKSettings settings = Resources.Load<STKSettings>("STKSettings");

    public static void ReceiveEvent(STKEvent e)
    {
        if (savedEvents.ContainsKey(e.eventName))
        {
            List<STKEvent> eventsList = (List<STKEvent>)savedEvents[e.eventName];
            if (settings.useSlidingWindow && eventsList.Count > settings.EventMaximum) //Sliding window
            {
                eventsList.RemoveAt(0);
            } else if (settings.useDataReduction && eventsList.Count > settings.EventMaximum)
            {
                eventsList = ReduceListData(eventsList);
            }
                Debug.Log(eventsList.Count);
            eventsList.Add(e);
            savedEvents[e.eventName] = eventsList;
        } else
        {
            savedEvents[e.eventName] = new List<STKEvent>();
            List<STKEvent> eventsList = (List<STKEvent>)savedEvents[e.eventName];
            eventsList.Add(e);
            savedEvents[e.eventName] = eventsList;
        }
        Debug.Log("Receive");
    }

    public static List<STKEvent> ReduceListData(List<STKEvent> l) //Removes every second element from a list and return the reduced list
    {
        int oldLength = l.Count;
        for (int i = 0; i < oldLength; i++)
        {
            if (i >= l.Count)
            {
                i = oldLength;
            } else
            {
                l.RemoveAt(i);
            }
        }
        return l;
    }

    public static void SendEvents()
    {
        STKJsonParser.ReceiveEvents(savedEvents);
    }

    public static void ClearEvents()
    {
        savedEvents = new Hashtable();
    }
}
