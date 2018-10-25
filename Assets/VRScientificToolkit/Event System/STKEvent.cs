using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;

public enum EventAllowedType { Integer, String, Float, Boolean}

[System.Serializable]
public class EventParameter
{
    public string name;
    public EventAllowedType type;
    public System.Type systemType;
    public int typeIndex;

    public void SetTypeFromIndex()
    {
        //p.systemType = System.Type.GetType(s);
        Debug.Log("Set Property");
        systemType = STKEventTypeChecker.allowedTypes[typeIndex];
    }
}

[CreateAssetMenu(menuName = "VR Scientific Toolkit/STKEvent")]
public class STKEvent : ScriptableObject
{
    public EventParameter[] parameters;
    public string eventName;
    private Hashtable objects = new Hashtable();
    private int uniqueID;
    private float time;

    public void OnEnable()
    {
        Debug.Log("Onenable");
    }

    public void SetValue(string key, object value)
    {
        //Test if Key exists and Value is the correct Datatype
        foreach (EventParameter p in parameters)
        {
            if (p.systemType == null)
            {
                p.SetTypeFromIndex();
            }
            if (key == p.name)
            {
                switch(p.type)
                {
                    case EventAllowedType.Integer:
                        if (value.GetType() == typeof(int))
                        {
                            objects.Add(key, value);
                        } else
                        {
                            Debug.LogWarning("Value " + key + " is type " + value.GetType() + " which is not allowed.");
                        }
                        break;
                    case EventAllowedType.String:
                        if (value.GetType() == typeof(string))
                        {
                            objects.Add(key, value);
                        }
                        else
                        {
                            Debug.LogWarning("Value " + key + " is type " + value.GetType() + " which is not allowed.");
                        }
                        break;
                    case EventAllowedType.Float:
                        if (value.GetType() == typeof(float))
                        {
                            objects.Add(key, value);
                        }
                        else
                        {
                            Debug.LogWarning("Value " + key + " is type " + value.GetType() + " which is not allowed.");
                        }
                        break;
                    case EventAllowedType.Boolean:
                        if (value.GetType() == typeof(bool))
                        {
                            objects.Add(key, value);
                        }
                        else
                        {
                            Debug.LogWarning("Value " + key + " is type " + value.GetType() + " which is not allowed.");
                        }
                        break;
                }
            }
        }
        
    }

}
