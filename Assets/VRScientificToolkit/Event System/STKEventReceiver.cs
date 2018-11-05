using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class STKEventReceiver {
    
    public static Hashtable savedEvents = new Hashtable();

    public static void ReceiveEvent(STKEvent e)
    {
        if (savedEvents.ContainsKey(e.eventName))
        {
            List<STKEvent> eventsList = (List<STKEvent>)savedEvents[e.eventName];
            eventsList.Add(e);
            savedEvents[e.eventName] = eventsList;
        } else
        {
            savedEvents[e.eventName] = new List<STKEvent>();
            List<STKEvent> eventsList = (List<STKEvent>)savedEvents[e.eventName];
            eventsList.Add(e);
            savedEvents[e.eventName] = eventsList;
        }
    }

    public static void SendEvents()
    {
        STKJsonParser.ReceiveEvents(savedEvents);
    }
}
