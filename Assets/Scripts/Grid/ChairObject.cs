using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChairObject", menuName = "Object/Chair")]
public class ChairObject : ScriptableObject
{
    [SerializeField]
    public List<Vector2> addedLocations;
    [SerializeField]
    public Sprite sprite;
    //If true will spawn in the middle of a til
    //Falst will spawn on the bottom left edge
    [SerializeField]
    public bool middleGridx = true;
    [SerializeField]
    public bool middleGridY = true;
}