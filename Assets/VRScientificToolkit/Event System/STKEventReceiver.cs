using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class STKEventReceiver {

    public static List<STKEvent> savedEvents = new List<STKEvent>();

	public static void ReceiveEvent(STKEvent e)
    {
        savedEvents.Add(e);
    }
}
