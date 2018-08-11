using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maid : MonoBehaviour {

    // Variables

    public DrinkTemplate drink;
    public GameObject drinkHeld;

    private int health;
    private Vector2 hSpeed;
    private Vector2 vSpeed;
    private Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        vSpeed = new Vector2(0,0);
        hSpeed = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
       

        if (Input.GetKey("w"))
        {
            vSpeed = new Vector2(0, 2);
            Debug.Log("Forward key pressed.");
        }
        if (Input.GetKey("a"))
        {
            hSpeed = new Vector2(-2, 0);
        }
        if (Input.GetKey("s"))
        {
            vSpeed = new Vector2(0, -2);

        }
        if (Input.GetKey("d"))
        {
            hSpeed = new Vector2(2, 0);
        }

        rb.MovePosition(rb.position + (hSpeed + vSpeed) * Time.deltaTime);

        hSpeed = new Vector2(0, 0);
        vSpeed = new Vector2(0, 0);

        
    }
    
    /*
     * Getters  
     */

    public int GetHealth()
    {
        return this.health;
    }

    public Vector2 GetvSpeed()
    {
        return this.vSpeed;
    }

    public Vector2 GethSpeed()
    {
        return this.hSpeed;
    }

    public DrinkTemplate Getdrink()
    {
        return this.drink;
    }

    /*
     * Setters 
     */
    public void SetDrink(DrinkTemplate newDrink)
    {
        drink = newDrink;
        //drinkHeld.GetComponent<MaidsDrink>().setDrink(newDrink);
        drinkHeld.GetComponent<SpriteRenderer>().sprite = newDrink.sprite;
    }
}
