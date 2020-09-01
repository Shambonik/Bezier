using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BezierGUI))]
public class BezierInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        BezierGUI bezier = (BezierGUI)target;
        if(GUILayout.Button("Add Node"))
        {
            bezier.createNode();
        }
        if(GUILayout.Button("Create circle"))
        {
            bezier.createCircle();
        }
        if (GUILayout.Button("Create straight line"))
        {
            bezier.createLine();
        }
    }
}
