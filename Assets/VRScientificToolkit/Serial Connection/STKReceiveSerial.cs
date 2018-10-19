using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using Unity.Jobs;

public class STKReceiveSerial : MonoBehaviour {

    public string port;
    public int baudRate;

    private SerialPort stream;
    private string currentValue;
    
    // Use this for initialization
    void Start () {
        stream = new SerialPort(port, baudRate);
        stream.DataReceived += new SerialDataReceivedEventHandler(OnDataReceived);
        
        stream.Open();
    }
	
	// Update is called once per frame
	void Update () {
        if (stream.BytesToRead != 0)
        {
            Debug.Log(stream.ReadLine());
        }
        /*Debug.Log(stream.BytesToRead);
        try
        {
            //Debug.Log(stream.ReadLine());
        }
        catch (System.Exception e)
        {

        }*/
    }

    private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        Debug.Log("Got some data");
    }

    /*public struct ReadSerial : IJob
    {
        public SerialPort stream;
        public float result;

        public void Execute()
        {
            stream = new SerialPort("COM4", 9600);
            stream.Open();
            Debug.Log(stream.ReadLine());
            ReadSerial reader = new ReadSerial();
            reader.stream = stream;
            JobHandle handle = reader.Schedule();
            handle.Complete();
        }
    }*/
}

