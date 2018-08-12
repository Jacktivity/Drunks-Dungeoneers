using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBlocker : MonoBehaviour {

    public Vector2 gridLocation;

    TableGrid grid;

    private void Start()
    {
        grid = GetComponentInParent<TableGrid>();
        grid.AddTileToLocation(gridLocation, TileContent.Blocking);
    }

    private void OnDrawGizmos()
    {
        Vector3 position = transform.position;

        if (grid == null)
            grid = GetComponentInParent<TableGrid>();

        Gizmos.color = new Color(1, 0, 0, 1);
        Gizmos.DrawCube(new Vector3((position.x), (position.y)), new Vector3(grid.gridSize,grid.gridSize,1)); 
    }
}
