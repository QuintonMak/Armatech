using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBullet : Bullet
{
    int numBounces;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        numBounces = 0;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.layer == targetLayer)//hard code the obstacle layer
        {

            other.gameObject.SendMessage("damage", damage);
            Destroy(this.gameObject);
        }
        

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.layer == 8)
        {
            if (numBounces < 2)
            {
                numBounces++;
                ContactPoint2D[] contacts = new ContactPoint2D[10];
                col.GetContacts(contacts);
                Vector3 normal = contacts[0].normal;
                direction = Vector3.Reflect(direction, normal);
                transform.right = direction;
            }
            else Destroy(this.gameObject);
        }
    }

    void Bounce()
    {
        numBounces++;

    }
}
