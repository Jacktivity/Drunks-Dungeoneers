using UnityEngine;

public class OTable : MonoBehaviour {

    public Vector2 location;
    public TableObject tableType;
    public GameObject chairPrefab;

    private TableGrid grid;    

    private void Start()
    {
        grid = GetComponentInParent<TableGrid>();

        transform.position = grid.GetWorldPositionOfGrid(location, tableType.middleGridx, tableType.middleGridY);

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
    }
}
