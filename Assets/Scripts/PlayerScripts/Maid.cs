using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maid : MonoBehaviour {

    // Variables
    private int health;
    private float hSpeed;
    private float vSpeed;
    private DrinkTemplate drink;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            Debug.Log("Forward key pressed.");
        }
        else if (Input.GetKeyDown("a"))
        {

        }
        else if (Input.GetKeyDown("s"))
        {

        }
        else if (Input.GetKeyDown("d"))
        {

        }
    }
    
    /*
     * Getters  
     */

    public int getHealth()
    {
        return this.health;
    }

    public float getvSpeed()
    {
        return this.vSpeed;
    }

    public float gethSpeed()
    {
        return this.hSpeed;
    }

    public DrinkTemplate getdrink()
    {
        return this.drink;
    }

    /*
     * Setters 
     */
}
