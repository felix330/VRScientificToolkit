using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEventValues : MonoBehaviour {

    private STKEventSender sender;
	// Use this for initialization
	void Start () {
        sender = GetComponent<STKEventSender>();
        sender.eventToSend.SetValue("Name", "Franz");
        sender.eventToSend.SetValue("Age", 42);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
