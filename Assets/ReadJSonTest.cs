using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SimpleJSON;

public class ReadJSonTest : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    [ContextMenu("Start")]
    void Test() {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        StreamReader reader = new StreamReader("C:\\JSON\\12-12_17-31-14.json");
        string s = reader.ReadToEnd();
        Debug.Log(s);
        STKScenePlayback.StartPlayback(s, 0);
        STKScenePlayback.GoToPoint(0);
        /*System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        StreamReader reader = new StreamReader("C:\\JSON\\12-8_13-59-37.json");
        string s = reader.ReadToEnd();
        JSONNode n = JSON.Parse(s);
        Debug.Log(n["Stage0"]["RandomMoveCube5780"][1]["time"].AsFloat);*/
    }
}
