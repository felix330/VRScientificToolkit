using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class STKTestController : MonoBehaviour {

    public GameObject verticalGroup; //Parent object for spawned selections
    public GameObject propertyPrefab;
    public GameObject startButton;

    public GameObject[] startActivateObjects;

    [SerializeField]
    private List<STKTestControllerProperty> properties = new List<STKTestControllerProperty>();
    private Hashtable values = new Hashtable();
    private static bool started;
    private static float time;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (started)
        {
            time += Time.deltaTime;
        }
	}

    public void AddProperty(string name)
    {
        Debug.Log("add property");
        GameObject newProperty = GameObject.Instantiate(propertyPrefab);
        newProperty.transform.parent = verticalGroup.transform;
        newProperty.GetComponent<STKTestControllerProperty>().text.text = name;
        properties.Add(newProperty.GetComponent<STKTestControllerProperty>());
        startButton.transform.parent = transform;
        startButton.transform.parent = verticalGroup.transform; //Reset button to last position
    }

    public void ToggleTest(GameObject button)
    {
        if (!started)
        {
            button.GetComponent<Button>().GetComponentInChildren<Text>().text = "Stop Test";
            foreach (GameObject g in startActivateObjects)
            {
                g.SetActive(true);
            }

            foreach (STKTestControllerProperty p in properties)
            {
                Debug.Log(p.text.text);
                Debug.Log(p.GetValue());
                values.Add(p.text.text, p.GetValue());
                Debug.Log("Add Value");
            }
            STKJsonParser.TestStart(values);
            started = true;
        } else
        {
            button.GetComponent<Button>().GetComponentInChildren<Text>().text = "Start Test";
            foreach (GameObject g in startActivateObjects)
            {
                g.SetActive(false);
            }
            time = 0;
            started = false;
            STKJsonParser.TestEnd();
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
