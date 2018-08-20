using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TableObject", menuName = "Object/Table")]
public class TableObject : ScriptableObject
{
    public ChairObject chair;
    //Used if the item covers more than one square
    public List<Vector2> addedLocations;
    public List<Vector2> seatLocations;
    public Sprite sprite;
    //If true will spawn in the middle of a til
    //Falst will spawn on the bottom left edge
    public bool middleGridx = true;
    public bool middleGridY = true;
}
