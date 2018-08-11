using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaidsDrink : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        GetComponent<SpriteRenderer>().sprite = null;

    }
	
	public void setDrink(DrinkTemplate newDrink)
    {
        GetComponent<SpriteRenderer>().sprite = newDrink.sprite;
    }
}
