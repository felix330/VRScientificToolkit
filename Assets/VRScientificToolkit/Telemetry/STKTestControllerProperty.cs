using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class STKTestControllerProperty : MonoBehaviour {

    public Text text;
    public InputField inputField;
    public Toggle toggle;
    private string value;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (inputField != null)
        {
            value = inputField.text;
        } else if (toggle != null)
        {
            value = toggle.isOn.ToString();
        }
	}

    public string GetValue()
    {
        return value;
    }

    public void Clear()
    {
        value = null;
        if (inputField != null)
        {
            inputField.text = null;
        }
    }
    
}
