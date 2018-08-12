using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour {

    public TableGrid grid;
    private readonly float straitLength = 1;

    float Heuristic(Vector2 from, Vector2 goal)
    {
        float dx = Mathf.Abs(from.x - goal.x);
        float dy = Mathf.Abs(from.y - goal.y);
        return straitLength * (dx + dy);
    }

    /// <summary>
    /// Gets the grid locations with the lowest score to the end node
    /// </summary>
    /// <param name="openSet">Available position that have not been used</param>
    /// <param name="fScore">The fscores for all of the grid locations</param>
    /// <returns></returns>
    Vector2 GetLowestFScore(List<Vector2> openSet, Dictionary<Vector2, float> fScore)
    {
        Vector2 current = openSet[0];
        float score = fScore[current];

        foreach(Vector2 vector in openSet)
        {
            float newScore = fScore[vector];

            if (score > newScore)
            {
                current = vector;
                score = newScore;
            }
        }

        return current;
    }

    /// <summary>
    /// Creates the path then flips it so it is the right way round
    /// </summary>
    /// <param name="cameFrom">The list of vectors and where they came from</param>
    /// <param name="current">The current and final position</param>
    /// <returns></returns>
    List<Vector2> ConstructPath(Dictionary<Vector2, Vector2> cameFrom, Vector2 current)
    {
        List<Vector2> invertedPath = new List<Vector2>();
        invertedPath.Add(current);

        while(cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            invertedPath.Add(current);
        }

        List<Vector2> path = new List<Vector2>(invertedPath.Count);

        //Flips the path so that it is the right way round
        for(int i = invertedPath.Count - 1; i >=0; i--)
        {
            path.Add(invertedPath[i]);
        }

        return path;
    }

    /// <summary>
    /// Gets all the valid neightbors for the current node
    /// </summary>
    /// <param name="current"></param>
    /// <returns></returns>
    List<Vector2> getNeighbors(Vector2 current)
    {
        List<Vector2> neighbors = new List<Vector2>();

        Vector2 neighbor = current;
        neighbor.x++;

        if(grid.IsTileFree(neighbor))
            neighbors.Add(neighbor);

        neighbor = current;
        neighbor.x--;

        if (grid.IsTileFree(neighbor))
            neighbors.Add(neighbor);

        neighbor = current;
        neighbor.y++;

        if (grid.IsTileFree(neighbor))
            neighbors.Add(neighbor);

        neighbor = current;
        neighbor.y--;

        if (grid.IsTileFree(neighbor))
            neighbors.Add(neighbor);

        return neighbors;
    }

    float DistanceBetween(Vector2 current, Vector2 neighbor)
    {
        //If both are differnet then the neighbor is diaganal
        //Used for diaganal movement
        /*
        if(current.x != neighbor.x && current.y != neighbor.y)
        {
            return diaganalLength;
        }
        */

        return straitLength;
    }

    /// <summary>
    /// Gets a free seat and gets a random spawn point
    /// then gets a path between them
    /// </summary>
    /// <returns>A Path between the spawn and the seat</returns>
    public List<Vector2> GetPathFromSpawnToSeat()
    {
        Vector2 spawnLocation = grid.GetSpawnPoint();
        Vector2? seatLocation = grid.GetFreeSeat();

        if(seatLocation != null)
        {
            return GetPath(spawnLocation, seatLocation.Value);
        }

        return null;
    }

    /// <summary>
    /// Returns a path to and from the 
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns>Returns the generated path or will return null if no path is available</returns>
    public List<Vector2> GetPath(Vector2 from, Vector2 to)
    {
        //Nodes already calculated
        List<Vector2> closedSet = new List<Vector2>();

        //Descovered nodes that have not been calculated yet
        List<Vector2> openSet = new List<Vector2>();
        openSet.Add(from);

        //which position can efficiently be reached from
        //will contain the most efficent step
        //Key came from value
        Dictionary<Vector2, Vector2> cameFrom = new Dictionary<Vector2, Vector2>();

        //The cost of getting to this node from the starting node
        Dictionary<Vector2, float> gScore = new Dictionary<Vector2, float>();
        gScore.Add(from, 0);

        //get the total cost of getting from the start node to the goal
        Dictionary<Vector2, float> fScore = new Dictionary<Vector2, float>();

        //For the first node this is complely heuristic
        fScore.Add(from, Heuristic(from, to));

        while(openSet.Count != 0)
        {
            //get the grid space with the lowest score to the end grid
            Vector2 current = GetLowestFScore(openSet, fScore);

            if(current == to)
            {
                return ConstructPath(cameFrom, current);
            }

            //Place this grid location into the closed set
            openSet.Remove(current);
            closedSet.Add(current);

            foreach(Vector2 neighbor in getNeighbors(current))
            {
                //Skip if we have already evaluated this neighbor
                if(!closedSet.Contains(neighbor))
                {
                    //Gets the distance from the start node to this neighbor
                    float tentative_gScore = gScore[current] + DistanceBetween(current, neighbor);

                    //Discover a new position
                    if(!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                    else if(tentative_gScore >= gScore[neighbor])
                    {
                        //This is not a better path
                        continue;
                    }

                    //This is the best path so we record it
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentative_gScore;
                    fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, to);
                }
            }

        }

        return null;

    }    
}