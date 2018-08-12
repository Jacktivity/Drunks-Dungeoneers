using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
    private SpriteRenderer attackSprite;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private GameObject attacker;

    private float speed;
    private float time;

	// Use this for initialization
	void Start () {
        attackSprite = GetComponent<SpriteRenderer>();
        startPosition = transform.position;
	}
	
    public void SetUp(Sprite attackSprite, Vector3 destination, float speed, GameObject attacker)
    {
        this.speed = speed;
        this.attackSprite.sprite = attackSprite;
        this.attacker = attacker;
        startPosition = transform.position;
        endPosition = destination;
    }

    private void Movement()
    {
        float x = Mathf.Lerp(startPosition.x, endPosition.x, time);
        float y = Mathf.Lerp(startPosition.y, endPosition.y, time);
        float z = Mathf.Lerp(startPosition.z, endPosition.z, time);

        transform.position = new Vector3(x, y, z) + Noise();
    }

    //Override for other effects
    private Vector3 Noise()
    {
        return new Vector3(0, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Patron" && collision.gameObject != attacker)
        {
            collision.gameObject.GetComponent<Patron>().PatronHit(0.1f);
        }
    }

    public void SetDestination(Vector3 end)
    {
        endPosition = end;
    }

    // Update is called once per frame
    void Update () {
        time += Time.deltaTime * speed;
        if(transform.position == endPosition)
        {
            //Do something?
            Destroy(this);
        }
        Movement();
	}
}
