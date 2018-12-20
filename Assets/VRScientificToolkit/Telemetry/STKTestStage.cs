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
    public GameObject timeText;
    public GameObject propertyParent;
    public GameObject buttonParent;
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
            timeText.GetComponent<Text>().text = Mathf.Round(time).ToString();
            if (hasTimeLimit && time >= timeLimit)
            {
                ToggleTest(startButton);
            }
        }
    }

    public void AddInputProperty(string name)
    {
        GameObject newProperty = GameObject.Instantiate(myController.inputPropertyPrefab);
        newProperty.transform.SetParent(propertyParent.transform);
        newProperty.GetComponent<STKTestControllerProperty>().text.text = name;
        properties = Array.ConvertAll(STKArrayTools.AddElement(newProperty.GetComponent<STKTestControllerProperty>(),properties), item => item as STKTestControllerProperty);
        startButton.transform.SetParent(transform.parent);
        startButton.transform.SetParent(propertyParent.transform); //Reset button to last position
    }

    public void AddToggleProperty(string name)
    {
        GameObject newProperty = GameObject.Instantiate(myController.togglePropertyPrefab);
        newProperty.transform.SetParent(propertyParent.transform);
        newProperty.GetComponent<STKTestControllerProperty>().text.text = name;
        properties = Array.ConvertAll(STKArrayTools.AddElement(newProperty.GetComponent<STKTestControllerProperty>(), properties), item => item as STKTestControllerProperty);
        startButton.transform.SetParent(transform.parent);
        startButton.transform.SetParent(propertyParent.transform); //Reset button to last position
    }

    public void AddButton(string name)
    {
        GameObject newButton = GameObject.Instantiate(myController.buttonPrefab);
        newButton.transform.SetParent(buttonParent.transform);
        newButton.GetComponent<Button>().GetComponentInChildren<Text>().text = name;
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
            for (int i = 0; i<GameobjectsToSendMessageTo.Count; i++)
            {
                GameobjectsToSendMessageTo[i].SendMessage(messagesToSend[i]);
            }
            foreach (GameObject g in GameobjectsToDeActivate)
            {
                g.SetActive(false);
            }
            values = new Hashtable();
            foreach (STKTestControllerProperty p in properties)
            {
                Debug.Log(p.text.text);
                Debug.Log(p.GetValue());
                values.Add(p.text.text, p.GetValue());
                p.gameObject.SetActive(false);
            }
            if (hasTimeLimit)
            {
                timeText.transform.parent.gameObject.SetActive(true);
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
            Debug.Log("Point 1");
            button.GetComponent<Button>().GetComponentInChildren<Text>().text = "Start Stage";
            foreach (GameObject g in GameobjectsToActivate)
            {
                g.SetActive(false);
            }
            time = 0;
            timeText.transform.parent.gameObject.SetActive(false);
            Debug.Log("Point 2");
            STKEventReceiver.SendEvents();
            Debug.Log("Point 3");
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
