using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dagger : Bullet
{
    public float turningActs, counter;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        
    }

    // Update is called once per frame
    new void Update()
    {
        ChangeDirection();
        base.Update();
        
    }

    void ChangeDirection()
    {
        counter++;
        try
        {
            if (counter <= turningActs) transform.right = Vector3.Normalize(player.transform.position - transform.position);
        }
        catch(MissingReferenceException e)
        {

        }
        
        
        direction = transform.right;
    }

    
}
