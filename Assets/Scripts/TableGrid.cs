using UnityEngine;

public class TableGrid : MonoBehaviour {

    public int sizeX = 10, sizeY = 10;
    public float width = 1.0f, height = 1.0f;

    private void Start()
    {
        
    }

    private void Update()
    {
        
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

        if(width <= 0.0f)        
            width = 0.01f;

        if (height <= 0.0f)
            height = 0.01f;
        
    }

    private void OnDrawGizmos()
    {
        Vector3 position = transform.position;

        for(float y = 0; y <= sizeY; y++)
        {
            Gizmos.DrawLine(new Vector3(position.x, position.y + (y * height)),
                new Vector3(position.x + sizeX * width, position.y + (y *height)));
        }

        for(float x = 0; x <= sizeX; x++)
        {
            Gizmos.DrawLine(new Vector3(position.x + (x * width), position.y),
                new Vector3(position.x + (x * width), position.y + (sizeY * height)));
        }
    }


}
