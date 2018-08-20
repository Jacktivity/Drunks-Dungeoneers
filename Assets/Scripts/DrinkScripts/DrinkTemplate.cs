using UnityEngine;

[CreateAssetMenu(fileName = "Drink", menuName = "Items/Drink")]
public class DrinkTemplate : ScriptableObject
{
    public string objectName = "New Drink";
    public Sprite sprite;
    public Sprite uiSprite;
}