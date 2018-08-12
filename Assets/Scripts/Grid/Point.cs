using UnityEngine;

public enum TileContent
{
    PatronSpawn,
    Empty,
    Patron,
    Seat,
    FullSeat,
    Blocking
}

public class Point
{ 
    public Point(Vector2 location)
    {
        this.location = location;
        tileContent = TileContent.Empty;
    }

    public TileContent tileContent;
    public Vector2 location;
}
