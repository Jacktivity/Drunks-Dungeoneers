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

        if(e.isKey)
        {
            if (e.character == 'a' || e.character == 's')
            {
                if (mousePos.x <= (tableGrid.transform.position.x + (tableGrid.gridSize * tableGrid.sizeX)) &&
                    mousePos.y <= (tableGrid.transform.position.y + (tableGrid.gridSize * tableGrid.sizeY)) &&
                    mousePos.x >= (tableGrid.transform.position.x) &&
                    mousePos.y >= (tableGrid.transform.position.y))
                {

                    GameObject obj;
                    UnityEngine.Object prefab = PrefabUtility.GetCorrespondingObjectFromSource(Selection.activeObject);

                    if (prefab)
                    {
                        bool found = false;


                        //(0,0) == (3,1)
                        WorldBlocker[] currentBlocking = tableGrid.GetComponentsInChildren<WorldBlocker>();
                        Vector2 gridLocation = new Vector2(Mathf.Floor((mousePos.x - tableGrid.transform.position.x) / tableGrid.gridSize), Mathf.Floor((mousePos.y - tableGrid.transform.position.y) / tableGrid.gridSize));

                        if (currentBlocking.Length > 0)
                            foreach (WorldBlocker block in currentBlocking)
                            {
                                if (block.gridLocation == gridLocation)
                                {
                                    found = true;
                                    break;
                                }
                            }

                        if (!found)
                        {
                            obj = (GameObject)PrefabUtility.InstantiatePrefab(prefab);

                            Vector3 aligned = new Vector3(tableGrid.transform.position.x + (gridLocation.x * tableGrid.gridSize) + (tableGrid.gridSize /2), tableGrid.transform.position.y + (gridLocation.y * tableGrid.gridSize) + (tableGrid.gridSize / 2));

                            obj.transform.position = aligned;
                            obj.transform.SetParent(tableGrid.transform, true);
                            WorldBlocker worldBlock = obj.GetComponent<WorldBlocker>();
                            worldBlock.gridLocation = gridLocation;

                            if (e.character == 'a')
                            {
                                worldBlock.tileContent = TileContent.Blocking;
                            }
                            else if (e.character == 's')
                            {
                                worldBlock.tileContent = TileContent.PatronSpawn;
                            }
                        }
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

        if(GUILayout.Button("Toggle Grid"))
        {
            tableGrid.ToggleGrid();
        }

        if (tableGrid.drawGrid)
        {
            
            if (GUILayout.Button("Toggle Tile Content"))
            {
                tableGrid.ToggleTileContent();
            }
        }

        SceneView.RepaintAll();

        if(GUI.changed)
        {
            tableGrid.OnValidate();
        }
    }
}

