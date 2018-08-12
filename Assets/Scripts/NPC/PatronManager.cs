using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PatronManager : MonoBehaviour {
    [SerializeField] private Sprite[] bodies;
    [SerializeField] private Sprite[] heads;
    [SerializeField] private Sprite cloak;

    List<Patron> patrons;
	// Use this for initialization
	void Start () {
        patrons = new List<Patron>();
    }
	
    public void MakePatron(IEnumerable<Vector2> destination)
    {
        Patron.Race randRace = (Patron.Race)Random.Range(0, 4);
        Patron.Class randClass = (Patron.Class)Random.Range(0, 3);

        Sprite head = heads[(int)randClass];
        Sprite body = bodies[(int)randRace];

        Sprite[] character = { head, body };

        GameObject patron = new GameObject();
        patron.tag = "Patron";
        patron.AddComponent<Patron>();
        patron.transform.parent = gameObject.transform;
        

        patron.GetComponent<Patron>().SetUpPatron(randClass,randRace,0.1f,1f,character,destination, cloak);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
