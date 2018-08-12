﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Patron : MonoBehaviour {

    Sprite[] character;
    Sprite cloak;

    private float secondsPerMove;
    private float secondsPerAction;

    private SpriteRenderer bodySprite;
    private SpriteRenderer helmSprite;

    private float thirst;
    private float actionTimer;
    private float thirstIncrease;
    private int coins;
    private bool atTable;
    public int pathIndex;
    public Vector2[] path;

    private Race charRace;
    private Class charClass;

    public void SetUpPatron(Class patronClass, Race patronRace, float moveInterval, float actionInterval
        , IEnumerable<Sprite> character, IEnumerable<Vector2> path, Sprite cloak)
    {
        SetUpSprites(patronClass, patronRace, character, cloak);
        ChangeOutfit();
        this.path = path.ToArray();
        coins = Random.Range(5, 15);
        RandomThirst();
    }

    private void SetUpSprites(Class patronClass, Race patronRace, IEnumerable<Sprite> character, Sprite cloaked)
    {
        gameObject.AddComponent<SpriteRenderer>();
        bodySprite = GetComponent<SpriteRenderer>();
        gameObject.name = patronClass.ToString() + patronRace.ToString() + "Patron";
        GameObject child = new GameObject();
        child.name = "Helmet";
        child.transform.parent = gameObject.transform;
        child.AddComponent<SpriteRenderer>();
        helmSprite = child.GetComponent<SpriteRenderer>();

        cloak = cloaked;

        this.character = character.ToArray();

        
    }

    public void ChangeOutfit()
    {
        if(atTable)
        {
            bodySprite.sprite = character.ToArray()[0];
            helmSprite.sprite = character.ToArray()[1];
        }
        else
        {
            helmSprite.sprite = null;
            bodySprite.sprite = cloak;
        }
    }

    public enum Class
    {
        Fighter, Wizard, Thief, Cleric
    }

    public enum Race
    {
        Human, Dwarf, Elf, Orc
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

    private void TraversePath()
    {
        pathIndex++;
        if(pathIndex >= path.Length)
        {
            pathIndex = path.Length - 1;
        }
        if(pathIndex == path.Length-1)
        {
            atTable = true;
        }
        transform.position = path[pathIndex];
    }

    public void NewPath(IEnumerable<Vector2> newPath)
    {
        path = newPath.ToArray();
        pathIndex = 0;
        atTable = false;
    }

    public void GetDrink()//enum of the drink
    {
        thirst = 0;
        actionTimer = 0;
        RandomThirst();
        //Throw event to award player points
        //Remove coin = to points given
        if(coins <= 0)
        {
            //Call event to get new path out
            pathIndex = 0;
        }
    }

	// Update is called once per frame
	void Update ()
    {
        bool tempAtTable = atTable;
        if (coins <= 0)
        {
            if(pathIndex == path.Length-1)
            {
                Destroy(this);
            }
        }
        else if (atTable)
        {
            PerformAction();
            ThirstCheck();
        }
        else
        {
            TraversePath();
        }

        if(tempAtTable != atTable)
        {
            ChangeOutfit();
        }
        
    }

    private void OnValidate()
    {
        secondsPerMove = Mathf.Clamp(secondsPerMove, 0.1f, 1);
        secondsPerAction = Mathf.Clamp(secondsPerAction, 0.5f, 5);
    }
}