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
	
	// Update is called once per frame
	void Update () {
		
	}

    [ContextMenu("Deploy")]
    void Deploy()
    {
        STKEventReceiver.ReceiveEvent(eventToSend);
    }
}
