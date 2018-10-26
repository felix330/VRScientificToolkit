using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class STKEventSender : MonoBehaviour {

    public STKEvent eventBase;
    [HideInInspector]
    public STKEvent eventToSend;
    public PropertyInfo[] trackedProperties;
    public FieldInfo[] trackedFields;
    
	// Use this for initialization
	void Awake () {
        eventToSend = Instantiate(eventBase);
	}

    private void Start()
    {
        Debug.Log(trackedProperties);
        foreach (PropertyInfo p in trackedProperties)
        {
            Debug.Log(p.Name);
        }
    }

    private void Update()
    {
        
    }

    [ContextMenu("Deploy")]
    void Deploy()
    {
        STKEventReceiver.ReceiveEvent(eventToSend);
    }

    public void SetTrackedVar(PropertyInfo[] p, FieldInfo[] f)
    {
        trackedProperties = p;
        trackedFields = f;
        Debug.Log(gameObject);
    }
}
