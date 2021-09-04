using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOne : EnemyMovement
{
    public int dodgeCounter, chargeCounter;
    public int dodgeDirection;
    Vector3 dodgeVector, chargeVector;

    public float chargeDamage;

    public GameObject healthBar, canvas;
    public EnemyFunctions enemyFunctions;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        dodgeCounter = 0;
        enemyFunctions.currentHealth -= Standoff.hitDamage;
        enemyFunctions.SetName("James Torre");
        //healthBar = Instantiate(healthBar, GameObject.Find("Canvas").transform);//, new Vector3(0, -28, 0), Quaternion.identity, GameObject.Find("Canvas").transform);
        //healthBar.GetComponent<HealthBar>().SetValues(enemyFunctions.currentHealth, enemyFunctions.maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            multiplier = 1;
            if (player.GetComponent<PlayerAbilities>().warpActs < player.GetComponent<PlayerAbilities>().slowDuration)
            {
                multiplier = 0.5f;
            }
            Move();
            this.SendMessage("SetShoot", true);
            Charge();
        }
        
        //healthBar.SendMessage("SetHealth", enemyFunctions.currentHealth);
        
    }

    
    public override void Move()
    {
        dodgeCounter++;
        
        if (dodgeCounter < 103)
        {
            if (Physics2D.Raycast(transform.position, transform.right, 0.6f, 1 << 8))
            {
                rigid.velocity = new Vector3(-transform.right.y, transform.right.x, 0) * speed * multiplier;
                //Debug.Log("wall in front");
            }
            else rigid.velocity = transform.right * speed * multiplier;
        }
        else if (dodgeCounter < 110)
        {
            if(dodgeCounter == 104)
            {
                int rand = Random.Range(0, 2);
                if (rand == 0) rand = -1;
                dodgeDirection = rand;
                dodgeVector = new Vector3(-transform.right.y, transform.right.x, 0);
            }
            transform.up = dodgeVector;
            rigid.velocity = dodgeVector * speed * multiplier * 10 * dodgeDirection;
            
        }
        else
        {
            dodgeCounter = 0;
        }
    }

    void Charge()
    {
        chargeCounter++;
        
        if (chargeCounter >= 300 && chargeCounter <= 324)
        {
            if (chargeCounter == 300)
            {
                chargeVector = transform.right;
                this.SendMessage("SetShoot", false);
            }
            else if(chargeCounter == 324)
            {
                this.SendMessage("SetShoot", true);
            }
            transform.right = chargeVector;
            rigid.velocity = chargeVector * speed * multiplier * 15;
        }
        else if(chargeCounter >= 400)
        {
            chargeCounter = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 3)
        {
            other.gameObject.SendMessage("damage", chargeDamage);
            
        }
    }
}
