using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class STKReceiveSerial : MonoBehaviour {

    SerialPort stream = new SerialPort("COM4", 9600);
    private string currentValue;
	// Use this for initialization
	void Start () {
        stream.Open();
	}
	
	// Update is called once per frame
	void Update () {
        currentValue = stream.ReadLine();
        Debug.Log(currentValue);
	}
}
