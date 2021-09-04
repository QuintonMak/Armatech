using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThree : EnemyMovement
{
    public GameObject flame, flameGunEnd, spikeBullet;
    public EnemyFunctions enemyFunctions;
    public int flameCooldown, flameCounter, pullCounter, pullCooldown, spikeCounter, spikeCooldown;
    public Vector3 direction;
    public EnemyShoot[] shooters;
    public AudioSource flameSound;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        shooters = gameObject.GetComponents<EnemyShoot>() as EnemyShoot[];
        enemyFunctions.SetName("Bronsted");
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
        flameCounter++;
        pullCounter++;
        spikeCounter++;
        if (flameCounter < flameCooldown)
        {
            if (Physics2D.Raycast(transform.position, transform.right, 2.4f, 1 << 8))
            {
                rigid.velocity = new Vector3(-transform.right.y, transform.right.x, 0) * speed * multiplier;
                //Debug.Log("wall in front");
            }
            else rigid.velocity = transform.right * speed * multiplier;
            direction = transform.right;
        }

        if(pullCounter < pullCooldown + 60 && pullCounter >= pullCooldown)
        {
            rigid.velocity = Vector3.zero;
            if (pullCounter == pullCooldown)
            {
                player.SendMessage("GravitySpeedSet", true);
                
                foreach (EnemyShoot s in shooters)
                {
                    s.enabled = false;
                }
                this.SendMessage("SetShoot", false);
            }
            else if(pullCounter == pullCooldown + 59)
            {
                player.SendMessage("GravitySpeedSet", false);
                
                foreach (EnemyShoot s in shooters)
                {
                    s.enabled = true;
                }
                this.SendMessage("SetShoot", true );
            }
            player.SendMessage("GravityPull", Vector3.Normalize(transform.position - player.transform.position)*4.5f);
        }
        else if (flameCounter < flameCooldown + 60 && flameCounter >= flameCooldown)
        {
            //shoot fire
            if (flameCounter == flameCooldown) flameSound.Play();
            if (flameCounter == flameCooldown + 59) flameSound.Stop();
            rigid.velocity = Vector3.zero;
            if(flameCounter >= flameCooldown + 1) transform.right = direction;
            FlameThrower();
        }
        else if(spikeCounter == spikeCooldown)
        {
            SpikeShot();
        }
        



        if (flameCounter > flameCooldown + 80) flameCounter = 0;
        if (pullCounter > pullCooldown + 60) pullCounter = 0;
        if (spikeCounter > spikeCooldown) spikeCounter = 0;
    }

    void FlameThrower()
    {
        if (flameCounter % 3 == 0)
        {
            
            GameObject f = Instantiate(flame, flameGunEnd.transform.position, Quaternion.identity);
            float angle = flameCounter - flameCooldown - 30;
            angle *= 2f;
            f.SendMessage("Create", direction);
            f.transform.eulerAngles += new Vector3(0, 0, angle);
        }

    }

    void SpikeShot()
    {
        for(int i = 0; i < 10; i++)
        {
            GameObject b = Instantiate(spikeBullet, transform.position, Quaternion.identity);
            b.transform.eulerAngles += new Vector3(0f, 0f, Random.Range(0f, 360f));
            b.SendMessage("SetSpeed", 4f);
            b.SendMessage("Create", b.transform.right);
        }
    }
}
