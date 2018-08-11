using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkHolder : MonoBehaviour
{

    public DrinkTemplate drinkTemplate;

    public void OnMouseOver()
    {

        Debug.Log("hover");
        if (Input.GetMouseButtonDown(0))
        {
            this.GetComponent<SpriteRenderer>().sprite = null;
            this.GetComponentInParent<BarManager>().SetMaidDrink(this.drinkTemplate);
        }
    }
    public void SetDrink(DrinkTemplate newDrink)
    {
        drinkTemplate = newDrink;
        GetComponent<SpriteRenderer>().sprite = drinkTemplate.sprite;
    }

}
