using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STKLookDirection : MonoBehaviour {

    public STKEvent lookEvent;
    private GameObject lookingAt;
    public bool sendStartEvent;
    public bool sendStopEvent;

    private RaycastHit hit;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Physics.SphereCast(transform.position, 0.2f, transform.forward, out hit, 100);

        if (hit.transform != null && lookingAt != hit.transform.gameObject)
        {
            lookingAt = hit.transform.gameObject;
        } else if (hit.transform == null && lookingAt != null)
        {
            lookingAt = null;
        }
	}

    public GameObject getLookingAt()
    {
        return lookingAt;
    }
}
