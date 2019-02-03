using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTest : MonoBehaviour {

    public GameObject bttn;
	// Use this for initialization
	void Start () {
        GetComponent<STKTestStage>().ToggleTest(bttn);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
