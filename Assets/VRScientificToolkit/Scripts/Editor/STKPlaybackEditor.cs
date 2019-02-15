using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
namespace STK
{
    ///<summary>Creates controls for the scene playback function.</summary>
    public class STKPlaybackEditor : EditorWindow
    {

        [MenuItem("Window/VR Scientific Toolkit/JSON Playback")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(STKPlaybackEditor));
        }

        int currentStage;
        int lastStage;
        float currentTime;
        float lastTime;
        string filePath;
        bool started;
        bool playing = false;
        float lastSystemTime;

        private void OnInspectorUpdate()
        {
            float t = Time.realtimeSinceStartup;
            if (playing)
            {
                currentTime += t - lastSystemTime;
                STKScenePlayback.GoToPoint(currentTime);
                Repaint();
            }
            lastSystemTime = t;
        }

        private void OnGUI()
        {
            if (!EditorApplication.isPlaying)
            {
                EditorGUILayout.LabelField("Please enter play mode to use the Playback tool.");
                currentStage = 0;
                lastStage = -1;
                currentTime = 0;
                lastTime = -1;
                filePath = null;
                started = false;
                playing = false;
            }
            else
            {
                EditorGUILayout.LabelField("Playback");
                EditorGUILayout.Space();
                if (GUILayout.Button("Select JSON File"))
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                    filePath = EditorUtility.OpenFilePanel("Select JSON File", filePath, "json");
                }

                if (!started)
                {
                    STKScenePlayback.DeactivateAllComponents();
                    started = true;
                }

                if (filePath != null && filePath != "")
                {
                    currentStage = EditorGUILayout.IntField("Stage (Start at 0):", currentStage);
                    currentTime = EditorGUILayout.FloatField("Time to Restore:", currentTime);

                    if (currentStage != lastStage)
                    {
                        StreamReader reader = new StreamReader(filePath);
                        string s = reader.ReadToEnd();
                        STKScenePlayback.StartPlayback(s, currentStage);
                        STKScenePlayback.GoToPoint(currentTime);
                    }
                    else if (currentTime != lastTime)
                    {
                        STKScenePlayback.GoToPoint(currentTime);
                    }

                    if (!playing)
                    {
                        if (GUILayout.Button("Play"))
                        {
                            playing = true;
                        }
                    }
                    else
                    {
                        if (GUILayout.Button("Pause"))
                        {
                            playing = false;
                        }
                    }


                    lastStage = currentStage;
                    lastTime = currentTime;
                }
            }
        }
    }
}
