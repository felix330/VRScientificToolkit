using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script that stores all Gameobjects with a tracker to use in the playback module
public class STKTrackedObjects : MonoBehaviour {

    public List<GameObject> trackedObjects;

    private void Start()
    {
        foreach (GameObject g in trackedObjects)
        {
            if (g.GetComponent<STKEventSender>() == null)
            {
                //trackedObjects.Remove(g);
                Debug.Log("Removing");
            }
        }
    }
}
