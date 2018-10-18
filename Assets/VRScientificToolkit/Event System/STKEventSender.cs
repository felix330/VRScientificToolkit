using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STKEventSender : MonoBehaviour {

    public STKEvent eventBase;
    [HideInInspector]
    public STKEvent eventToSend;
	// Use this for initialization
	void Awake () {
        eventToSend = Instantiate(eventBase);
	}

    [ContextMenu("Deploy")]
    void Deploy()
    {
        STKEventReceiver.ReceiveEvent(eventToSend);
    }
}
