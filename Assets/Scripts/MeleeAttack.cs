using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public int hitRate, actsPassed;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        actsPassed = hitRate;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (actsPassed >= hitRate)
        {
            if (other.gameObject.layer == 3) {
                other.gameObject.SendMessage("damage", damage);
                actsPassed = 0;
            }
        }
        else actsPassed++;
    }

    void OnCollisionExit2D(Collision2D collider)
    {
        actsPassed = hitRate;
    }
}
