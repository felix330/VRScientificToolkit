using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Collects all Events to later send them to STKJSONParser
public static class STKEventReceiver {
    
    public static Hashtable savedEvents = new Hashtable();
    private static STKSettings settings = Resources.Load<STKSettings>("STKSettings");

    public static void ReceiveEvent(STKEvent e)
    {
        if (savedEvents.ContainsKey(e.eventName))
        {
            List<STKEvent> eventsList = (List<STKEvent>)savedEvents[e.eventName];
            eventsList.Add(e);
            savedEvents[e.eventName] = eventsList;
            if (settings.useSlidingWindow && eventsList.Count > settings.EventMaximum) //Reduces Data volume when too many Events were received
            {
                eventsList.RemoveAt(0); //Removes first Element (Sliding window)
            } else if (settings.useDataReduction && eventsList.Count > settings.EventMaximum)
            {
                eventsList = ReduceListData(eventsList);
            } else if (settings.createFileWhenFull && eventsList.Count > settings.EventMaximum)
            {
                STKJsonParser.SaveRunning(); //Saves all current Events and starts again with 0 Events
            }
        } else
        {
            savedEvents[e.eventName] = new List<STKEvent>();
            List<STKEvent> eventsList = (List<STKEvent>)savedEvents[e.eventName];
            eventsList.Add(e);
            savedEvents[e.eventName] = eventsList;
        }
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

    public static Hashtable GetEvents()
    {
        return savedEvents;
    }

    public static void ClearEvents()
    {
        savedEvents.Clear();
        savedEvents = new Hashtable();
    }
}
