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
        SceneView.onSceneGUIDelegate = GridUpdate;
    }

    //Draw blocking zones on the grid
    void GridUpdate(SceneView sceneView)
    {
        Event e = Event.current;

        Ray ray = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
        Vector3 mousePos = ray.origin;

        if(e.isKey && e.character == 'a')
        {
            if (mousePos.x < (tableGrid.transform.position.x + (tableGrid.gridSize * tableGrid.sizeX)) &&
                mousePos.y < (tableGrid.transform.position.y + (tableGrid.gridSize * tableGrid.sizeY)))
            {

                GameObject obj;
                UnityEngine.Object prefab = PrefabUtility.GetCorrespondingObjectFromSource(Selection.activeObject);

                if (prefab)
                {
                    bool found = false;

                    WorldBlocker[] currentBlocking = tableGrid.GetComponentsInChildren<WorldBlocker>();
                    Vector3 aligned = new Vector3(Mathf.Floor(mousePos.x / tableGrid.gridSize) + (tableGrid.gridSize / 2), Mathf.Floor(mousePos.y / tableGrid.gridSize) + (tableGrid.gridSize / 2));
                    if(currentBlocking.Length > 0)
                        foreach (WorldBlocker block in currentBlocking)
                        {
                            if(block.transform.position == aligned)
                            {
                                found = true;
                                break;
                            }
                        }

                    if (!found)
                    {
                        obj = (GameObject)PrefabUtility.InstantiatePrefab(prefab);

                        obj.transform.SetParent(tableGrid.transform);
                        obj.transform.position = aligned;
                        obj.GetComponent<WorldBlocker>().gridLocation = new Vector2(Mathf.Floor(mousePos.x / tableGrid.gridSize) - tableGrid.transform.position.x, Mathf.Floor(mousePos.y / tableGrid.gridSize) - tableGrid.transform.position.y);
                    }
                }
            }
        }        
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Grid X");
        tableGrid.sizeX = EditorGUILayout.IntField(tableGrid.sizeX, GUILayout.Width(50));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Grid Y");
        tableGrid.sizeY = EditorGUILayout.IntField(tableGrid.sizeY, GUILayout.Width(50));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Grid Size");
        tableGrid.gridSize = EditorGUILayout.FloatField(tableGrid.gridSize, GUILayout.Width(50));
        GUILayout.EndHorizontal();

        SceneView.RepaintAll();

        if(GUI.changed)
        {
            tableGrid.OnValidate();
        }
    }
}

