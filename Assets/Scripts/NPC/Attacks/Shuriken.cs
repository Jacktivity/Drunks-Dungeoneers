using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : Attack {
    private int bounces;
    protected override void Miss(Collision collision)
    {
        base.Miss(collision);
        bounces++;
        if(bounces >= 4)
        {
            Destroy(this);
        }
        else
        {
            float x = transform.position.x - collision.gameObject.transform.position.x;
            if(x >= 0)
            {

            }
            
        }
    }
}
