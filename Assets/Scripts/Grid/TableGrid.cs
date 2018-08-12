using System.Collections.Generic;
using UnityEngine;

public class TableGrid : MonoBehaviour
{
    public int sizeX = 10, sizeY = 10;
    public float gridSize = 1.0f;

    [Tooltip("How many tiles around the edge of the map that tables cannot spawn on")]
    public int NoSpawnZone = 1;

    private List<Vector2> spawnPoints;
    private List<Vector2> seatLocations;
    private List<Vector2> usedSeats;

    public bool drawGrid = true;
    public bool tileContentDebug = false;
    private Point[,] grid;

    private void Awake()
    {
        spawnPoints = new List<Vector2>();
        seatLocations = new List<Vector2>();
        usedSeats = new List<Vector2>();

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
    /// Returns a random spawn location
    /// </summary>
    /// <returns></returns>
    public Vector2 GetSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Count)];
    }

    /// <summary>
    /// Returns the location of a free seat, will return null if no free seats
    /// </summary>
    /// <returns>Free seat on grid, or null if none available</returns>
    public Vector2? GetFreeSeat()
    {
        if(seatLocations.Count > 0)
        {
            int randomRange = Random.Range(0, seatLocations.Count);
            Vector2 location = seatLocations[randomRange];

            //Move seat to Used
            seatLocations.RemoveAt(randomRange);
            usedSeats.Add(location);

            return location;
        }

        return null;
    }

    /// <summary>
    /// Release a seat for another patron to use
    /// </summary>
    /// <param name="location">Location of the freed seat</param>
    public void ReleaseSeat(Vector2 location)
    {
        if(usedSeats.Contains(location))
        {
            usedSeats.Remove(location);
            seatLocations.Add(location);
        }
    }

    /// <summary>
    /// Changed an empty or seat tile to used so  it stops getting used for others patrons
    /// </summary>
    /// <param name="takeTile">Tile to be used by the patron</param>
    /// <param name="releaseTile">releasing the tile the patron was on null if no tile</param>
    public void Taketile(Vector2 takeTile, Vector2? releaseTile = null)
    {
        Point tile;

        if (releaseTile != null)
        {
            //Get the current tile
            tile = grid[(int)releaseTile.Value.x, (int)releaseTile.Value.y];

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
            grid[(int)releaseTile.Value.x, (int)releaseTile.Value.y] = tile;
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

        grid[(int)takeTile.x, (int)takeTile.y] = tile;

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
        if (point.tileContent == TileContent.Seat || point.tileContent <= TileContent.Empty)
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
        if (location.x >= (sizeX- NoSpawnZone) || location.x < NoSpawnZone || location.y >= (sizeY - NoSpawnZone) || location.y < NoSpawnZone)
            return false;

        bool spawnFree = grid[(int)location.x, (int)location.y].tileContent == TileContent.Empty;

        if (spawnFree)
        {
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    spawnFree = (grid[(int)location.x + x, (int)location.y + y].tileContent == TileContent.Empty);

                    if (!spawnFree)
                    {
                        return false;
                    }                       
                }
            }
        }


        return spawnFree;
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

        Vector2 tileLocation;
        foreach (Vector2 otherLocation in otherLocations)
        {
            tileLocation = new Vector2(otherLocation.x + location.x, otherLocation.y + location.y);
            if (!IsTileFreeToSpawn(tileLocation))
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

        //If a tile is a spawn point add it to the list of spawn locations
        if(tileContent == TileContent.PatronSpawn)
        {
            spawnPoints.Add(location);
        }
        else if(tileContent == TileContent.Seat)
        {
            seatLocations.Add(location);
        }

    }

    /// <summary>
    /// Returns the world position of the grid location
    /// </summary>
    /// <param name="coord">The grid location to convert</param>
    /// <param name="middleX">If true will return the middle of x not the start/param>
    /// <param name="middleY">if true will return the middle of y not the start</param>
    /// <returns>Returns to world position of the grid</returns>
    public Vector3 GetWorldPositionOfGrid(Vector2 coord, bool middleX = true, bool middleY = true)
    {
        float gridMiddleX = (middleX ? (gridSize / 2) : 0);
        float gridMiddleY = (middleY ? (gridSize / 2) : 0);
        return new Vector3(gridMiddleX + transform.position.x + (coord.x * gridSize), gridMiddleY + transform.position.y + (coord.y * gridSize));
    }

    public void ToggleGrid()
    {
        drawGrid = !drawGrid;
    }

    public void ToggleTileContent()
    {
        tileContentDebug = !tileContentDebug;
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
        if (drawGrid)
        {
            Vector3 position = transform.position;

            for (int y = 0; y <= sizeY; y++)
            {
                Gizmos.DrawLine(new Vector3(position.x, position.y + (y * gridSize)),
                    new Vector3(position.x + sizeX * gridSize, position.y + (y * gridSize)));
            }

            for (int x = 0; x <= sizeX; x++)
            {
                Gizmos.DrawLine(new Vector3(position.x + (x * gridSize), position.y),
                    new Vector3(position.x + (x * gridSize), position.y + (sizeY * gridSize)));
            }

            if(tileContentDebug)
            {
                float halfGrid = gridSize / 2;
                Point currentPoint;

                for(int x = 0; x < sizeX; x++)
                {
                    for(int y = 0; y < sizeY; y++)
                    {
                        currentPoint = grid[x, y];

                        switch (currentPoint.tileContent)
                        {
                            case TileContent.PatronSpawn:
                                Gizmos.color = new Color(0, 1, 0, 1);
                                break;
                            case TileContent.Empty:
                                Gizmos.color = new Color(1, 1, 1, 1);
                                break;
                            case TileContent.Patron:
                                Gizmos.color = new Color(0, 0, 0, 1);
                                break;
                            case TileContent.Seat:
                                Gizmos.color = new Color(0, 0, 1, 1);
                                break;
                            case TileContent.FullSeat:
                                Gizmos.color = new Color(0, 1, 1, 1);
                                break;
                            case TileContent.Blocking:
                                Gizmos.color = new Color(1, 0, 0, 1);
                                break;
                            default:
                                break;
                        }

                        Gizmos.DrawCube(GetWorldPositionOfGrid(new Vector2(x, y)), new Vector3(gridSize,gridSize,1));
                    }
                }
            }
        }
    }


}
