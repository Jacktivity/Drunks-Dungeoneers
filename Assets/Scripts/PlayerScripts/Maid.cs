using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maid : MonoBehaviour {

    // Variables

    Animator anim;
    bool walkingRight;
    bool walkingLeft;
    bool walkingAway;
    bool walkingTowards;

    public DrinkTemplate drink;
    public GameObject drinkHeld;

    private int health;
    private int coins;
    private Vector2 hSpeed;
    private Vector2 vSpeed;
    private Vector2 drinkPos;
    private Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        vSpeed = new Vector2(0,0);
        hSpeed = new Vector2(0, 0);
        anim = GetComponent<Animator>();
        health = 5;

        walkingRight = false;
        walkingLeft = false;
        walkingAway = false;
        walkingTowards = false;

        drinkPos = drinkHeld.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
        {
            vSpeed = new Vector2(0, 2);
            walkingAway = true;
            walkingLeft = false;
            walkingRight = false;
            walkingTowards = false;
            drinkHeld.transform.localPosition = new Vector2(-drinkPos.x, drinkPos.y);
        }
        if (Input.GetKey("a"))
        {
           
            hSpeed = new Vector2(-2, 0);
            walkingLeft = true;

            transform.localScale = new Vector3(1,1,1);
        }
        if (Input.GetKey("s"))
        {
            vSpeed = new Vector2(0, -2);
            walkingTowards = true;
            walkingAway = false;
            walkingLeft = false;
            walkingRight = false;
            

        }
        if (Input.GetKey("d"))
        {
            hSpeed = new Vector2(2, 0);
            walkingLeft = true;
            transform.localScale = new Vector3(-1, 1, 1);
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
            
        }

        anim.SetBool("WalkBack", walkingAway);
        anim.SetBool("WalkLeft", walkingLeft);
        anim.SetBool("WalkForward", walkingTowards);
        anim.SetBool("WalkRight", walkingRight);

        if (walkingTowards || walkingLeft)
        {
            drinkHeld.transform.localPosition = new Vector2(drinkPos.x, drinkPos.y);
        }
        else
        {
            drinkHeld.transform.localPosition = new Vector2(-drinkPos.x, drinkPos.y);
        }

        rb.MovePosition(rb.position + (hSpeed + vSpeed) * Time.deltaTime);

        hSpeed = new Vector2(0, 0);
        vSpeed = new Vector2(0, 0);




        ////////////
        //DEBUG INPUT
        ////////////
        //if (Input.GetKeyDown("l"))
        //    coins++;
            
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

    public int GetCoins()
    {
        return this.coins;
    }

    /*
     * Setters 
     */
    public void SetDrink(DrinkTemplate newDrink)
    {
        drink = newDrink;
        drinkHeld.GetComponent<SpriteRenderer>().sprite = newDrink.sprite;
    }
}
