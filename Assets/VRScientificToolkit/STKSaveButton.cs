using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STKSaveButton : MonoBehaviour {

	public void Onclick()
    {
        STKJsonParser.SaveRunning();
    }
}
