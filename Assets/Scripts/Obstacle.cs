using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Rigidbody2D r;
    public bool freezeRot, canspawn;
    public Collider2D collider;
    public float radius;
    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody2D>();
        canspawn = false;
        //collider = GetComponent<Collider2D>();
        if(freezeRot) r.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
        while (!canspawn)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, 1 << 8);
            canspawn = colliders.Length == 0;//if nothing in circle, can spawn
            if(colliders.Length == 1)
            {
                canspawn = colliders[0].Equals(collider);//if myself is in circle, can spawn
            }
            if(!canspawn) transform.position = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);//otherwise, spawn somewhere else
            else
            {
                canspawn = true;
                break;
            }
        }
        if (r.constraints != RigidbodyConstraints2D.FreezeAll) r.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void damage()
    {

    }
}
