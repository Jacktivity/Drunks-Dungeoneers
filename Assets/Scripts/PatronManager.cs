﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PatronManager : MonoBehaviour {
    Sprite[] bodies;
    Sprite[] heads;

    List<Patron> patrons;
	// Use this for initialization
	void Start () {
        patrons = new List<Patron>();
        bodies = (Sprite[])Resources.LoadAll("Races", typeof(Sprite));
        heads = (Sprite[])Resources.LoadAll("Classes", typeof(Sprite));
    }
	
    public void MakePatron(IEnumerable<Vector2> destination)
    {
        Patron.Race randRace = (Patron.Race)Random.Range(0, 4);
        Patron.Class randClass = (Patron.Class)Random.Range(0, 3);

        Sprite head = heads[(int)randClass];
        Sprite body = bodies[(int)randRace];

        Sprite[] character = { head, body };

        patrons.Add(new Patron(randClass,randRace,0.5f,1f,character, destination));
    }

	// Update is called once per frame
	void Update () {
		
	}
}
