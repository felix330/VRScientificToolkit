using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class STKTrackSteamVRActions : MonoBehaviour {

	// Use this for initialization
	void Start () {
		foreach (SteamVR_Action a in SteamVR_Input.actions)
        {
            Debug.Log(a.GetShortName());
        }
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
