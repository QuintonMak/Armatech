using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTwo : EnemyMovement
{
    public GameObject healthBar, canvas, turretCapsule, bullet, gunEnd;
    public EnemyFunctions enemyFunctions;
    public int turretCooldown, turretCounter, salvoCooldown, salvoCounter;
    Vector3 direction;
    
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        enemyFunctions.SetName("Henry Spavor");
        //healthBar = Instantiate(healthBar, GameObject.Find("Canvas").transform);//, new Vector3(0, -28, 0), Quaternion.identity, GameObject.Find("Canvas").transform);
        //dhealthBar.GetComponent<HealthBar>().SetValues(enemyFunctions.currentHealth, enemyFunctions.maxHealth);
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
        }
        
        
        //healthBar.SendMessage("SetHealth", enemyFunctions.currentHealth);

    }

    public override void Move()
    {
        turretCounter++;
        salvoCounter++;
        if(turretCounter < turretCooldown && salvoCounter < salvoCooldown)
        {
            if (Physics2D.Raycast(transform.position, transform.right, 1.3f, 1 << 8))
            {
                rigid.velocity = new Vector3(-transform.right.y, transform.right.x, 0) * speed * multiplier;
                //Debug.Log("wall in front");
            }
            else rigid.velocity = transform.right * speed * multiplier;
        }
        if (turretCounter < turretCooldown + 180 && turretCounter >= turretCooldown)
        {
            //build turret
            if (turretCounter == turretCooldown) BuildTurret();
            rigid.velocity = Vector3.zero;
        }
        if (salvoCounter < salvoCooldown + 120 && salvoCounter >= salvoCooldown)
        {
            Salvo();
            
        }
        


        if (turretCounter > turretCooldown + 180) turretCounter = 0;
        if (salvoCounter > salvoCooldown + 120) salvoCounter = 0;
        
    }

    void BuildTurret()
    {
        GameObject t = Instantiate(turretCapsule, transform.position, Quaternion.identity);
        t.transform.eulerAngles = new Vector3(0, 0, Random.Range(0f, 360f));
    }

    void Salvo()
    {
        if (salvoCounter % 15 == 0)
        {
            for(int j = 0; j < 4; j++)
            {
                for (int i = -2; i < 3; i++)
                {
                    GameObject b = Instantiate(bullet, gunEnd.transform.position, Quaternion.identity);
                    b.SendMessage("Create", transform.right);
                    b.transform.eulerAngles += new Vector3(0, 0, i * 10 + j*90);
                }
            }
            
            
        }
    }
}
