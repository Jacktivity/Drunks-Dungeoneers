using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinderTest : MonoBehaviour {

    public PathFinder pathFinder;
    public TableGrid grid;

    public Vector2 startLocation;
    public Vector2 endLocation;

    public float stepDelay = 1.0f;

    [SerializeField]
    public List<Vector2> path;
    int index = 0;

    public float delay;

    private void Start()
    {
        delay = stepDelay;        
    }

    public void GetPath()
    {
        path = pathFinder.GetPath(startLocation, endLocation);
        index = 0;
    }

    private void Update()
    {
        if (delay < 0.0f)
        {
            if (index < path.Count)
            {
                Vector3 newPosition = grid.GetWorldPositionOfGrid(path[index]);
                transform.position = newPosition;
                index++;
            }
            else
                index = 0;

            delay = stepDelay;
        }
        else
            delay -= Time.deltaTime;
    }

}
