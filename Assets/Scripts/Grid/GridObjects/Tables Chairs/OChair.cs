using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OChair : MonoBehaviour {

    public ChairObject chairObject;
    public Vector2 location;

    private TableGrid grid;
    

	// Use this for initialization
	void Start () {

        GetComponent<SpriteRenderer>().sprite = chairObject.sprite;
        grid = GetComponentInParent<TableGrid>();

        grid.AddTileToLocation(location, TileContent.Seat);

        foreach(Vector2 extendedChair in chairObject.addedLocations)
        {
            grid.AddTileToLocation(new Vector2(location.x + extendedChair.x, location.y + extendedChair.y), TileContent.Seat);
        }
    }
}
