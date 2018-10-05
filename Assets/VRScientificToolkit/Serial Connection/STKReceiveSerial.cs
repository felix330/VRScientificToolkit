using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class STKReceiveSerial : MonoBehaviour {

    public string port;
    public int baudRate;

    private SerialPort stream;
    private string currentValue;
	// Use this for initialization
	void Start () {
        stream = new SerialPort(port, baudRate);
        stream.Open();
	}
	
	// Update is called once per frame
	void Update () {
        currentValue = stream.ReadLine();
        Debug.Log(currentValue);
	}
}
