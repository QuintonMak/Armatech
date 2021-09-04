using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRocket : Bullet
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.layer == targetLayer || other.gameObject.layer == 8)//hard code the obstacle layer
        {
            Collider2D[] Colliders;
            Colliders = Physics2D.OverlapCircleAll(transform.position, 3);
            for (int i = 0; i < Colliders.Length; i++)
{
                if (Colliders[i].gameObject.layer == targetLayer) Colliders[i].gameObject.SendMessage("damage", damage);
                //else if (Colliders[i].gameObject.layer == 8) Destroy(Colliders[i].gameObject);
            }

            Instantiate(explosion, transform.position, Quaternion.identity);
            
            Destroy(this.gameObject);
        }
        

    }
}
