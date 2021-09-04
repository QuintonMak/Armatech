using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFour : EnemyMovement
{
    public GameObject airStrikeBlast;
    
    public float regen;
    public EnemyFunctions enemyFunctions;
    public int strikeCounter, strikeCooldown;
    public Vector3 direction;
    public EnemyShoot[] shooters;

    new void Start()
    {
        base.Start();
        shooters = gameObject.GetComponents<EnemyShoot>() as EnemyShoot[];
        enemyFunctions.SetName("The General");
        //healthBar = Instantiate(healthBar, GameObject.Find("Canvas").transform);//, new Vector3(0, -28, 0), Quaternion.identity, GameObject.Find("Canvas").transform);
        //dhealthBar.GetComponent<HealthBar>().SetValues(enemyFunctions.currentHealth, enemyFunctions.maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            multiplier = 1;
            if (player.GetComponent<PlayerAbilities>().warpActs < player.GetComponent<PlayerAbilities>().slowDuration)
            {
                multiplier = 0.5f;
            }



            Move();
            
        }


        //healthBar.SendMessage("SetHealth", enemyFunctions.currentHealth);

    }

    public override void Move()
    {
        
        if (strikeCounter < strikeCooldown)
        {
            if (Physics2D.Raycast(transform.position, transform.right, 2.4f, 1 << 8))
            {
                rigid.velocity = new Vector3(-transform.right.y, transform.right.x, 0) * speed * multiplier;
                //Debug.Log("wall in front");
            }
            else rigid.velocity = transform.right * speed * multiplier;
            direction = transform.right;
        }
        else
        {
            AirStrike();
        }

        strikeCounter++;
        if (strikeCounter > strikeCooldown) strikeCounter = 0;
    }

    

    void AirStrike()
    {
        StartCoroutine("StartAirStrike");
    }

    IEnumerator StartAirStrike()
    {
        for(int i = 0; i < 25; i++)
        {
            Instantiate(airStrikeBlast, new Vector3(Random.Range(-15f, 15f), Random.Range(-15f, 15f), 0), Quaternion.identity);
            yield return null;
        }
    }

    public void decreaseRegen()
    {
        regen -= regen*0.5f;
    }

    public void damage(float damage)
    {
        enemyFunctions.currentHealth += damage*regen;

    }

    public void decreaseSpeed()
    {
        speed-= 2f;
    }
}
