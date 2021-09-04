using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airstrike : MonoBehaviour
{
    public float damage;
    public GameObject explosion;
    public int counter, striketime;
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    void FixedUpdate()
    {
        counter++;
        if (counter >= striketime)
        {
            Strike();
        }
    }

    void Strike()
    {
        Collider2D[] Colliders;
        Colliders = Physics2D.OverlapCircleAll(transform.position, 2);
        for (int i = 0; i < Colliders.Length; i++)
        {
            if (Colliders[i].gameObject.layer == 3) Colliders[i].gameObject.SendMessage("damage", damage);
            //else if (Colliders[i].gameObject.layer == 8) Destroy(Colliders[i].gameObject);
        }

        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
