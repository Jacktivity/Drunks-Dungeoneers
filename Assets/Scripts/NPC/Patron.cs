using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Patron : MonoBehaviour {

    Sprite[] character;
    Sprite cloak;
    Patron.Class tempClass;

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
    private TableGrid grid;
    private PathFinder pathFinder;
    private float waitingForSpace;
    private float maxWaitTime = 3;

    private Race charRace;
    private Class charClass;

    public void SetUpPatron(Class patronClass, Race patronRace, float moveInterval, float actionInterval
        , IEnumerable<Sprite> character, IEnumerable<Vector2> path, Sprite cloak)
    {
        SetUpSprites(patronClass, patronRace, character, cloak);
        ChangeOutfit();

        this.path = path.ToArray();
        pathIndex = 0;
        waitingForSpace = Random.Range(1f, maxWaitTime);
        pathFinder = Camera.main.GetComponent<PathFinder>();

        coins = Random.Range(5, 15);
        RandomThirst();

        grid = GetComponentInParent<TableGrid>();

        tempClass = patronClass;
    }

    private void SetUpSprites(Class patronClass, Race patronRace, IEnumerable<Sprite> character, Sprite cloaked)
    {        
        bodySprite = gameObject.AddComponent<SpriteRenderer>();
        gameObject.name = patronClass.ToString() + patronRace.ToString() + "Patron";
        GameObject child = new GameObject();
        child.name = "Helmet";
        child.transform.parent = gameObject.transform;

        helmSprite = child.AddComponent<SpriteRenderer>();
        /*
        child.AddComponent<SpriteRenderer>();
        helmSprite = child.GetComponent<SpriteRenderer>();
        */

        cloak = cloaked;

        this.character = character.ToArray();        
    }

    public void ChangeOutfit()
    {
        if(atTable)
        {
            bodySprite.sprite = character.ToArray()[0];
            helmSprite.sprite = character.ToArray()[1];
            if(tempClass.ToString().Equals("Wizard") || tempClass.ToString().Equals("Cleric"))
            {
                helmSprite.transform.localPosition = new Vector2(0, 75);
            }
            else
            {
                helmSprite.transform.localPosition = new Vector2(0, 0.5f);
            }
            
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
            TraversePath();
        }
    }

    private void TraversePath()
    {
        //Stop patrons from standing inside each other
        if (grid.IsTileFree(path[pathIndex + 1]))
        {
            pathIndex++;
            if (pathIndex >= path.Length)
            {
                pathIndex = path.Length - 1;
            }
            if (pathIndex == path.Length - 1)
            {
                atTable = true;
            }
            grid.Taketile(path[pathIndex], path[pathIndex - 1]);

            transform.position = grid.GetWorldPositionOfGrid(path[pathIndex]);
        }
        else
        {
            waitingForSpace -= secondsPerMove;

            if(waitingForSpace < 0)
            {
                List<Vector2> newPath = pathFinder.GetPath(path[pathIndex], path[path.Length -1]);
                if (newPath != null)
                    NewPath(newPath);                
                   
                waitingForSpace = Random.Range(1f, maxWaitTime);
            }
        }
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
            //TraversePath();
            MoveAction();

            if (tempAtTable != atTable)
            {
                ChangeOutfit();
            }
        }        
    }

    private void OnValidate()
    {
        secondsPerMove = Mathf.Clamp(secondsPerMove, 0.5f, 2f);
        secondsPerAction = Mathf.Clamp(secondsPerAction, 0.5f, 5);
    }
}
