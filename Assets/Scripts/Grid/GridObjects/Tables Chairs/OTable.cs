using System.Collections.Generic;
using UnityEngine;

public class OTable : MonoBehaviour {

    public Vector2 location;
    public List<GameObject> chairs;
    public TableObject tableType;
    public GameObject chairPrefab;

    private TableGrid grid;    

    private void Start()
    {
        grid = GetComponentInParent<TableGrid>();

        GetComponent<SpriteRenderer>().sprite = tableType.sprite;

        //Add all the chairs for the table
        foreach(Vector2 chair in tableType.seatLocations)
        {
            GameObject prefab = (GameObject)Instantiate(chairPrefab, transform);
            prefab.transform.position = new Vector3(transform.position.x + (grid.gridSize * chair.x), transform.position.y + (grid.gridSize * chair.y));

            OChair oChair = prefab.GetComponent<OChair>();
            oChair.location = new Vector2(location.x + chair.x, location.y + chair.y);
            oChair.chairObject = tableType.chair;
        }

        //Block the tiles the table is on
        foreach(Vector2 tableLocations in tableType.addedLocations)
        {
            grid.AddTileToLocation(new Vector2(location.x + tableLocations.x, location.y + tableLocations.y), TileContent.Blocking);
        }
    }
}
