using UnityEngine;

public enum TileContent
{
    Seat,
    FullSeat,
    Blocking
}

public class Point
{ 
    public Point(Vector2 location)
    {
        this.location = location;
    }

    public TileContent tileContent;
    public Vector2 location;
}
