using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class STKTestControllerProperty : MonoBehaviour {

    public Text text;
    public InputField inputField;
    private string value;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        value = inputField.text;
	}

    public string GetValue()
    {
        return value;
    }

    public void Clear()
    {
        value = null;
        inputField.text = null;
    }
    
}
