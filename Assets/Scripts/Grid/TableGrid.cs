using UnityEngine;

public class TableGrid : MonoBehaviour {

    public int sizeX = 10, sizeY = 10;
    public float gridSize = 1.0f;

    private Point[,] grid;

    private void Awake()
    {
        grid = new Point[sizeX, sizeY];

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                grid[x, y] = new Point(new Vector2(x, y));
            }
        }
    }

    
    /// <summary>
    /// Returns a clone of the grid so that it cannot be updated outside of this class
    /// </summary>
    /// <returns>Clone of the grid</returns>
    public Point[,] GetGrid()
    {
        return (Point[,])grid.Clone();
    }

    public bool IsTileFree(Vector2 location)
    {
        if (location.x >= sizeX || location.x < 0 || location.y >= sizeY || location.y < 0)
            return false;

        Point point = grid[(int)location.x, (int)location.y];

        //If null then the point is empty
        if (point.tileContent == TileContent.Seat)
        {
            return true;
        }

        return false;
    }


    public void AddTileToLocation(Vector2 location, TileContent tileContent)
    {
        grid[(int)location.x, (int)location.y].tileContent = tileContent;
    }

    /// <summary>
    /// Returns the world position of the grid
    /// </summary>
    /// <param name="coord">the grid coord we want to get the position of</param>
    /// <returns>The 3d representaion of the grid position</returns>
    public Vector3 GetWorldPositionOfGrid(Vector2 coord)
    {
        return new Vector3((gridSize / 2) + transform.position.x + (coord.x * gridSize), (gridSize / 2) + transform.position.y + (coord.y * gridSize));
    }

    /// <summary>
    /// makes sure the  Minimum size is a 1X1 grid
    /// </summary>
    public void OnValidate()
    {
        if (sizeX <= 0)
            sizeX = 1;

        if (sizeY <= 0)
            sizeY = 1;

        if(gridSize <= 0.0f)        
            gridSize = 0.01f;
        
    }

    private void OnDrawGizmos()
    {
        Vector3 position = transform.position;

        for(float y = 0; y <= sizeY; y++)
        {
            Gizmos.DrawLine(new Vector3(position.x, position.y + (y * gridSize)),
                new Vector3(position.x + sizeX * gridSize, position.y + (y * gridSize)));
        }

        for(float x = 0; x <= sizeX; x++)
        {
            Gizmos.DrawLine(new Vector3(position.x + (x * gridSize), position.y),
                new Vector3(position.x + (x * gridSize), position.y + (sizeY * gridSize)));
        }
    }


}
