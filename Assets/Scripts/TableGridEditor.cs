using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TableGrid))]
class TableGridEditor : Editor
{

    TableGrid tableGrid;

    private void OnEnable()
    {
        tableGrid = (TableGrid)target;
    }

    public override void OnInspectorGUI()
    { 

        GUILayout.BeginHorizontal();
        GUILayout.Label("Grid X");
        tableGrid.sizeX = EditorGUILayout.IntField(tableGrid.sizeX, GUILayout.Width(50));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Grid Y");
        tableGrid.sizeY = EditorGUILayout.IntField(tableGrid.sizeY, GUILayout.Width(50));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Grid Width");
        tableGrid.width = EditorGUILayout.FloatField(tableGrid.width, GUILayout.Width(50));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Grid Height");
        tableGrid.height = EditorGUILayout.FloatField(tableGrid.height, GUILayout.Width(50));
        GUILayout.EndHorizontal();

        SceneView.RepaintAll();

        if(GUI.changed)
        {
            tableGrid.OnValidate();
        }
    }
}

