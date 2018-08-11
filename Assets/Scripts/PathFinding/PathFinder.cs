using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour {

    public TableGrid grid;

    float straitLength = 1, diaganalLength = 1.4f;

    float Heuristic(Vector2 from, Vector2 goal)
    {
        float dx = Mathf.Abs(from.x - goal.x);
        float dy = Mathf.Abs(from.y - goal.y);
        return straitLength * (dx + dy) + (diaganalLength - 2 * straitLength) + Mathf.Min(dx, dy);
    }


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

        neighbor = current;
        neighbor.x++;
        neighbor.y++;

        if (grid.IsTileFree(neighbor))
            neighbors.Add(neighbor);

        neighbor = current;
        neighbor.x--;
        neighbor.y++;

        if (grid.IsTileFree(neighbor))
            neighbors.Add(neighbor);

        neighbor = current;
        neighbor.x--;
        neighbor.y--;

        if (grid.IsTileFree(neighbor))
            neighbors.Add(neighbor);

        neighbor = current;
        neighbor.x++;
        neighbor.y--;

        if (grid.IsTileFree(neighbor))
            neighbors.Add(neighbor);

        return neighbors;
    }

    float DistanceBetween(Vector2 current, Vector2 neighbor)
    {
        //If both are differnet then the neighbor is diaganal
        if(current.x != neighbor.x && current.y != neighbor.y)
        {
            return diaganalLength;
        }

        return straitLength;
    }

    /// <summary>
    /// Returns a path to and from the 
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
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
            Vector2 current = GetLowestFScore(openSet, fScore);

            if(current == to)
            {
                return ConstructPath(cameFrom, current);
            }

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