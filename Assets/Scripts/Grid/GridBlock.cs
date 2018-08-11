using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBlock : MonoBehaviour {

    public TableGrid grid;

    public Vector2 gridLocation;

    private void Start()
    {
        transform.position = grid.GetWorldPositionOfGrid(gridLocation);
        grid.AddTileToLocation(gridLocation, TileContent.Blocking);
    }

}
