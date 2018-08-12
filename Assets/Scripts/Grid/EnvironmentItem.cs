using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TableObject", menuName = "Object/Table")]
public class TableObject : ScriptableObject {

    public ChairObject chair;
    //Used if the item covers more than one square
    public List<Vector2> addedLocations;
    public List<Vector2> seatLocations;
    public Sprite sprite;
}

[CreateAssetMenu(fileName = "ChairObject", menuName = "Object/Char")]
public class ChairObject : ScriptableObject
{
    public List<Vector2> addedLocations;
    public Sprite sprite;
}