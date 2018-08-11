using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathFinderTest))]
public class PathFinderTestEditor : Editor {

    PathFinderTest finderTest;

    private void OnEnable()
    {
        finderTest = (PathFinderTest)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Get Path"))
        {
            finderTest.GetPath();
        }

    }

}
