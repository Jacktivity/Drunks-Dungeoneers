using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkHolder : MonoBehaviour
{
    BarManager barManger;

    public DrinkTemplate drinkTemplate;
    private void Start()
    {
        barManger = GetComponentInParent<BarManager>();
    }
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
    public void SetDrink(DrinkTemplate newDrink)
    {
        drinkTemplate = newDrink;
        GetComponent<SpriteRenderer>().sprite = drinkTemplate.sprite;
    }

}
