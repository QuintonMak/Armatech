using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThreeRocket : Bullet
{
    float distance;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        distance = 0;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        Vector3 d = direction * speed * multiplier * Time.deltaTime;
        distance += d.magnitude;
        if (distance >= 12)
        {
            Explode();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.layer == targetLayer || other.gameObject.layer == 8)//hard code the obstacle layer
        {
            Explode();
        }
    }

    void Explode()
    {
        Collider2D[] Colliders;
        Colliders = Physics2D.OverlapCircleAll(transform.position, 3f);
        for (int i = 0; i < Colliders.Length; i++)
        {
            if (Colliders[i].gameObject.layer == targetLayer) Colliders[i].gameObject.SendMessage("damage", damage);
            //else if (Colliders[i].gameObject.layer == 8) Destroy(Colliders[i].gameObject);
        }

        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
