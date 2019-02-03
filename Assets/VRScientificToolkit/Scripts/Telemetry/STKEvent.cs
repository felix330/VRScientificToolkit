using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class EventParameter
{
    public string name;
    public System.Type systemType;
    public int typeIndex; //Index in the allowedTypes Array of the STKEventTypeChecker
    public bool hideFromInspector;

    public void SetTypeFromIndex()
    {
        systemType = STKEventTypeChecker.allowedTypes[typeIndex];
    }
}

[CreateAssetMenu(menuName = "VR Scientific Toolkit/STKEvent")]
public class STKEvent : ScriptableObject
{
    [SerializeField]
    public List<EventParameter> parameters = new List<EventParameter>();
    public string eventName;
    public Hashtable objects = new Hashtable();
    public float time;

    public void AddParameter(string name, int typeIndex) //Defines a new Parameter
    {
        EventParameter newParameter = new EventParameter();
        newParameter.name = name;
        newParameter.hideFromInspector = true;
        newParameter.typeIndex = typeIndex;
        parameters.Add(newParameter);
    }

    public void SetValue(string key, object value) //Sets a parameter to a certain value
    {
        //Test if Key exists and Value is the correct Datatype
        foreach (EventParameter p in parameters)
        {
            if (key == p.name)
            {
                if (p.systemType == null)
                {
                    p.SetTypeFromIndex();
                }

                if (value.GetType() == p.systemType)
                {
                    objects.Add(key, value);
                }
            }
        }
        
    }

}
