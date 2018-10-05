using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class STKEventReceiver {

    public static STKEvent[][] savedEvents;

	public static void ReceiveEvent(STKEvent e)
    {
        Debug.Log("Received Event " + e.eventName);

    }
}
