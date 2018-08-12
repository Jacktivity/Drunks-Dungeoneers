using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarManager : MonoBehaviour {

    // Variables
    public DrinkTemplate[] drinks; // Contains all the drinks in the game
    public DrinkHolder[] drinkTemplates; // The three bar drink positions
    public GameObject maid; // Pointer to the maid object, representing the player


	// Use this for initialization
	void Start ()
    {
        this.maid = Instantiate(maid);
        SetDrinks();
    }

    // Picks a random new drink from the list of drink ScriptableObjects
    public DrinkTemplate SelectDrink()
    {
        int pos = Random.Range(0, drinks.Length);
        return drinks[pos];
    }

    // Called every drink spawn interval (Determined by DelayDrinkSpawn) to populate the bar with new drinks
    public void SetDrinks()
    {
        // For the 3 drinkholder positions
        foreach (DrinkHolder obj in drinkTemplates)
        {
            // Select a new random drink
            var drink = SelectDrink();
            // If the current bar position is empty, then
            if (obj.GetComponent<SpriteRenderer>().sprite == null)
            {
                // If the drink to be placed is elsewhere on the bar, then
                if(!CheckDrinks(drink))
                {
                    this.SetDrinks();
                    break;
                }
                // Else place the drink on the table
                obj.SetDrink(drink);
            }
        }
    }

    public void SetMaidDrink(DrinkTemplate drink)
    {
        maid.GetComponent<Maid>().SetDrink(drink);
        StartCoroutine(DelayDrinkSpawn());
    }

    // Delays the spawning of a new drink by a  given time
    private IEnumerator DelayDrinkSpawn()
    {
        yield return new WaitForSeconds(1);
        SetDrinks();
    }

    public bool CheckDrinks(DrinkTemplate drink)
    {
        // For the 3 drinkholder positions
        foreach(DrinkHolder obj in drinkTemplates)
        {
            // If the drink to be placed exists in this position, return false
            if (drink.Equals(obj.drinkTemplate))
            {
                return false;
            }    
        }
        // Else return true
        return true;
    }
}
