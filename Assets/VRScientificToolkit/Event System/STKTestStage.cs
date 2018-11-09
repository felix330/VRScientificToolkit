using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class STKTestStage : MonoBehaviour{
    public STKTestControllerProperty[] properties;
    public List<GameObject> GameobjectsToActivate = new List<GameObject>();
    public List<GameObject> GameobjectsToDeActivate = new List<GameObject>();
    public List<GameObject> GameobjectsToSendMessageTo = new List<GameObject>();
    public List<string> messagesToSend = new List<string>();
    public bool hasTimeLimit;
    public int timeLimit;
    public STKTestController myController;
    public GameObject startButton;
    private static bool started;
    private Hashtable values = new Hashtable();
    private static float time;

    /*~STKTestStage()
    {
        Debug.Log("Destroying Test Stage");
    }*/

    private void Start()
    {
        properties = Array.ConvertAll(STKArrayTools.ClearNullReferences(properties), item => item as STKTestControllerProperty);
    }

    void Update()
    {
        if (started)
        {
            time += Time.deltaTime;

            if (hasTimeLimit && time >= timeLimit)
            {
                ToggleTest(startButton);
            }
        }
    }

    public void AddProperty(string name)
    {
        GameObject newProperty = GameObject.Instantiate(myController.propertyPrefab);
        newProperty.transform.SetParent(transform);
        newProperty.GetComponent<STKTestControllerProperty>().text.text = name;
        properties = Array.ConvertAll(STKArrayTools.AddElement(newProperty.GetComponent<STKTestControllerProperty>(),properties), item => item as STKTestControllerProperty);
        startButton.transform.SetParent(transform.parent);
        startButton.transform.SetParent(transform); //Reset button to last position
    }

    public void ToggleTest(GameObject button)
    {
        if (!started)
        {
            button.GetComponent<Button>().GetComponentInChildren<Text>().text = "Stop Stage";
            foreach (GameObject g in GameobjectsToActivate)
            {
                g.SetActive(true);
            }
            values = new Hashtable();
            foreach (STKTestControllerProperty p in properties)
            {
                Debug.Log(p.text.text);
                Debug.Log(p.GetValue());
                values.Add(p.text.text, p.GetValue());
                p.gameObject.SetActive(false);
            }
            STKJsonParser.TestStart(values);
            started = true;
        }
        else
        {
            foreach (STKTestControllerProperty p in properties)
            {
                p.gameObject.SetActive(true);
                p.Clear();
            }
            button.GetComponent<Button>().GetComponentInChildren<Text>().text = "Start Stage";
            foreach (GameObject g in GameobjectsToActivate)
            {
                g.SetActive(false);
            }
            time = 0;
            STKEventReceiver.SendEvents();
            STKEventReceiver.ClearEvents();
            STKJsonParser.TestEnd();
            started = false;
            myController.StageEnded();
        }

    }

    public static float GetTime()
    {
        return time;
    }

    public static bool GetStarted()
    {
        return started;
    }
}
