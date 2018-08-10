using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Drink", menuName = "Items/Drink", order = 1)]
public class DrinkTemplate : ScriptableObject
{
    public string objectName = "New Drink";
    public Sprite sprite;
}