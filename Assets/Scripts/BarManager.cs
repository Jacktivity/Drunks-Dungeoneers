﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarManager : MonoBehaviour {

    public DrinkTemplate[] drinks;
    public DrinkHolder[] drinkTemplates;
    public GameObject maid;


	// Use this for initialization
	void Start ()
    {
        this.maid = Instantiate(maid);
        SetDrinks();
    }

	// Update is called once per frame
	void Update ()
    {
    }

    public DrinkTemplate SelectDrink()
    {
        int pos = Random.Range(0, drinks.Length);
        return drinks[pos];
    }

    /*
     * Setters
     * */
    public void SetDrinks()
    {
        foreach (DrinkHolder obj in drinkTemplates)
        {
            var drink = SelectDrink();
            if (obj.GetComponent<SpriteRenderer>().sprite == null)
            {
                if(!CheckDrinks(drink))
                {
                    drink = SelectDrink();
                    CheckDrinks(drink);
                }
                obj.SetDrink(drink);
            }
        }
    }

    public void SetMaidDrink(DrinkTemplate drink)
    {
        maid.GetComponent<Maid>().SetDrink(drink);
        StartCoroutine(DelayDrinkSpawn());
    }

    private IEnumerator DelayDrinkSpawn()
    {
        yield return new WaitForSeconds(5);
        SetDrinks();
    }
    public bool CheckDrinks(DrinkTemplate drink)
    {
        foreach(DrinkHolder obj in drinkTemplates)
        {
            if (drink == obj.drinkTemplate)
            {
                return false;
            }    
        }
        return true;
    }
}
