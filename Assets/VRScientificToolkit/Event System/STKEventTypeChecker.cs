using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Defines Variable Types which can be serialized in events
public static class STKEventTypeChecker {
    
    public static System.Type[] allowedTypes = {typeof(int),typeof(float),typeof(string),typeof(bool),typeof(Vector2),typeof(Vector3),typeof(Vector4),typeof(System.String),typeof(UnityEngine.Vector2),typeof(UnityEngine.Vector3),typeof(UnityEngine.Vector4) };

    public static bool IsValid(System.Type typeToTest)
    {
        foreach(System.Type t in allowedTypes)
        {
            if (typeToTest == t)
            {
                return true;
            } 
        }
        return false;
    }
}
