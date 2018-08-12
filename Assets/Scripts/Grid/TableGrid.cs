using System.Collections.Generic;
using UnityEngine;

public class TableGrid : MonoBehaviour
{

    public int sizeX = 10, sizeY = 10;
    public float gridSize = 1.0f;

    public List<Vector2> worldBlock;

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
    /// Changed an empty tile or seat to used stopping others from using it    /// 
    /// </summary>
    /// <param name="takeTile">Tile to be used by the patron</param>
    /// <param name="releaseTile">releasing the tile the patron was on null if no tile</param>
    public void Taketile(Vector2 takeTile, Vector2? releaseTile = null)
    {
        Point tile;

        if (releaseTile != null)
        {
            //Get the current tile
            tile = grid[(int)releaseTile.Value.x, (int)releaseTile.Value.x];

            //If the tile was a seat set it back as a seat 
            if (tile.tileContent == TileContent.FullSeat)
            {
                tile.tileContent = TileContent.Seat;
            }
            //else set the tile to empty
            else if (tile.tileContent == TileContent.Patron)
            {
                tile.tileContent = TileContent.Empty;
            }

            //update the tile
            grid[(int)releaseTile.Value.x, (int)releaseTile.Value.x] = tile;
        }

        //Update the tile that a patron is going to move to
        tile = grid[(int)takeTile.x, (int)takeTile.y];

        if (tile.tileContent == TileContent.Empty)
        {
            tile.tileContent = TileContent.Patron;
        }
        else if (tile.tileContent == TileContent.Seat)
        {
            tile.tileContent = TileContent.FullSeat;
        }

    }

    /// <summary>
    /// Checks if a tile is free for a patron to stand on
    /// </summary>
    /// <param name="location"></param>
    /// <returns></returns>
    public bool IsTileFree(Vector2 location)
    {
        if (location.x >= sizeX || location.x < 0 || location.y >= sizeY || location.y < 0)
            return false;

        Point point = grid[(int)location.x, (int)location.y];

        //If null then the point is empty
        if (point.tileContent == TileContent.Seat || point.tileContent == TileContent.Empty)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Returns true if the spawn is empty else it will return false
    /// </summary>
    /// <param name="location">Location we are checking</param>
    /// <returns>True if location is empty</returns>
    public bool IsTileFreeToSpawn(Vector2 location)
    {
        if (location.x >= sizeX || location.x < 0 || location.y >= sizeY || location.y < 0)
            return false;

        return (grid[(int)location.x, (int)location.y].tileContent == TileContent.Empty);
    }

    /// <summary>
    /// Checks if multiple spaces are free
    /// </summary>
    /// <param name="location">First spawn location</param>
    /// <param name="otherLocations">Other spawn locations needed</param>
    /// <returns></returns>
    public bool CheckIfcanSpawn(Vector2 location, List<Vector2> otherLocations)
    {
        if (!IsTileFreeToSpawn(location))
        {
            return false;
        }

        foreach (Vector2 loc in otherLocations)
        {
            if (!IsTileFreeToSpawn(loc))
            {
                return false;
            }

        }
        return true;
    }

    /// <summary>
    /// Updates the tile to what content is inside it
    /// </summary>
    /// <param name="location">Grid location of the tile</param>
    /// <param name="tileContent">The content on the tile</param>
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

        if (gridSize <= 0.0f)
            gridSize = 0.01f;

    }

    private void OnDrawGizmos()
    {
        Vector3 position = transform.position;

        for (float y = 0; y <= sizeY; y++)
        {
            Gizmos.DrawLine(new Vector3(position.x, position.y + (y * gridSize)),
                new Vector3(position.x + sizeX * gridSize, position.y + (y * gridSize)));
        }

        for (float x = 0; x <= sizeX; x++)
        {
            Gizmos.DrawLine(new Vector3(position.x + (x * gridSize), position.y),
                new Vector3(position.x + (x * gridSize), position.y + (sizeY * gridSize)));
        }
    }


}
