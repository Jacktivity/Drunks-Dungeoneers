using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkHolder : MonoBehaviour
{

    // Variables
    private BarManager barManger;
    public DrinkTemplate drinkTemplate;

    // Called on level start
    private void Start()
    {
        barManger = GetComponentInParent<BarManager>();
    }

    // Used for main clicking on drinks to pickup from the bar
    public void OnMouseOver()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            if (Vector2.Distance(barManger.maid.transform.position, this.transform.position) < 0.5)
            {
                this.GetComponent<SpriteRenderer>().sprite = null;
                barManger.SetMaidDrink(this.drinkTemplate);
            }
        }
    }

    // Updates the sprite and data stored at a given bar location
    public void SetDrink(DrinkTemplate newDrink)
    {
        drinkTemplate = newDrink;
        GetComponent<SpriteRenderer>().sprite = drinkTemplate.sprite;
    }

}
