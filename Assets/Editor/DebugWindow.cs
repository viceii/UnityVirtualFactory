using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DebugWindow : EditorWindow
{
     [MenuItem("XFrame/Debug")]
     private static void AddWindow1()
     {
         DebugWindow myWindow = (DebugWindow)EditorWindow.GetWindow(typeof(DebugWindow), false, "Debug", true);//´´½¨´°¿Ú
         myWindow.Show();
     }

    private float minTimeScale;
    private float maxTimeScale;
    private float timeScale;
    private void OnGUI()
    {
        timeScale = EditorGUILayout.Slider("TimeScale", timeScale, 0.1f, 5f);
        Time.timeScale = timeScale;
    }
}
