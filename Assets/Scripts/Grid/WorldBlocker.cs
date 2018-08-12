using UnityEngine;

public class WorldBlocker : MonoBehaviour {

    public Vector2 gridLocation;
    public TileContent tileContent;

    TableGrid grid;

    private void Start()
    {
        grid = GetComponentInParent<TableGrid>();
        grid.AddTileToLocation(gridLocation, tileContent);
    }

    private void OnDrawGizmos()
    {
        Vector3 position = transform.position;

        if (grid == null)
            grid = GetComponentInParent<TableGrid>();

        if (tileContent == TileContent.Blocking)
        {
            Gizmos.color = new Color(1, 0, 0, 1);
        }
        else if (tileContent == TileContent.PatronSpawn)
        {
            Gizmos.color = new Color(0, 1, 0, 1);
        }

        Gizmos.DrawCube(position, new Vector3(grid.gridSize,grid.gridSize,1)); 
    }
}
