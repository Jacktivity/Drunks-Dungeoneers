using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patron : MonoBehaviour {
    
    [SerializeField] private float secondsPerMove;
    [SerializeField] private float secondsPerAction;

    private SpriteRenderer bodySprite;
    private SpriteRenderer helmSprite;
    private Sprite[] bodies = (Sprite[])Resources.LoadAll("Races",typeof (Sprite));
    private Sprite[] heads = (Sprite[])Resources.LoadAll("Classes", typeof(Sprite));


    private float thirst;
    private float actionTimer;
    private float thirstIncrease;
    private int coins;
    private bool atTable;

    public Patron(Class patronClass, Race patronRace) //enum.Drink preference
    {
        gameObject.AddComponent<SpriteRenderer>();
        bodySprite = GetComponent<SpriteRenderer>();
        GameObject child = new GameObject();
        child.transform.parent = gameObject.transform;
        child.AddComponent<SpriteRenderer>();
        helmSprite = child.GetComponent<SpriteRenderer>();

        bodySprite.sprite = bodies[(int)patronRace];
        helmSprite.sprite = heads[(int)patronClass];
        coins = Random.Range(5, 15);
        RandomThirst();
    }

    public enum Class
    {
        Fighter, Wizard, Thief, Cleric
    }

    public enum Race
    {
        Human, Dwarf, Elf
    }

    private void RandomThirst()
    {
        thirstIncrease = Random.Range(0.01f, 0.05f);
    }

    private void IncreaseThirst(float increase)
    {
        if(thirst + increase > 1)
        {
            thirst = 1;
        }
        else
        {
            thirst += increase;
        }
    }

    public void PatronHit(float damage)
    {
        IncreaseThirst(damage);
        //Particle effect?
    }

    private void ThirstCheck()
    {
        if (thirst > 0.5)
        {
            //Fire a thirst icon event
        }
    }

    private void PerformAction()
    {
        actionTimer += Time.deltaTime;
        if (actionTimer >= secondsPerAction)
        {
            if (thirst + thirstIncrease > 1)
            {
                thirst = 1;
            }
            actionTimer = 0;
            float choice = Random.Range(0f, 1f);
            if (choice >= thirst)
            {
                //Attack a target
            }
        }
    }

    private void MoveAction()
    {
        actionTimer += Time.deltaTime;
        if(actionTimer >= secondsPerMove)
        {
            actionTimer = 0;
            //Move patron toward table
        }
    }

    public void GetDrink()//enum of the drink
    {
        thirst = 0;
        actionTimer = 0;
        RandomThirst();
        //Throw event to award player points
        //Remove coin = to points given
    }

	// Update is called once per frame
	void Update ()
    {
        if(atTable)
        {
            PerformAction();
            ThirstCheck();
        }
        else
        {

        }
        
    }

    private void OnValidate()
    {
        secondsPerMove = Mathf.Clamp(secondsPerMove, 0.1f, 1);
        secondsPerAction = Mathf.Clamp(secondsPerAction, 0.5f, 5);
    }
}
