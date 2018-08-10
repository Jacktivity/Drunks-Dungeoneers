using UnityEngine;

public enum TileContent
{
    IsEmpty,
    Seat,
    Blocking
}

class Point
{ 
    public TileContent tileContent;
    public Vector2 location;
}
