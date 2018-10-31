using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STKTestController : MonoBehaviour {

    public GameObject verticalGroup; //Parent object for spawned selections
    public GameObject propertyPrefab;
    public GameObject startButton;

    public int numberOfProperties;
    public bool addProperty;

    private Hashtable properties = new Hashtable();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddProperty(string name)
    {
        Debug.Log("add property");
        GameObject newProperty = GameObject.Instantiate(propertyPrefab);
        newProperty.transform.parent = verticalGroup.transform;
        newProperty.GetComponent<STKTestControllerProperty>().text.text = name;
        properties.Add(name, newProperty);
        startButton.transform.parent = transform;
        startButton.transform.parent = verticalGroup.transform; //Reset button to last position
    }
}
