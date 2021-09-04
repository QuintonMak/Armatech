using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowBullet : Bullet
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

        if (other.gameObject.layer == targetLayer)//hard code the obstacle layer
        {

            other.gameObject.SendMessage("damage", damage);
            other.gameObject.SendMessage("SlowMe");
            Destroy(this.gameObject);
        }
        else if (other.gameObject.layer == 8)
        {


            Destroy(this.gameObject);

        }

    }
}
