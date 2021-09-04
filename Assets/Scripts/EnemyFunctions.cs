using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//All enemies should have these functionalities
public class EnemyFunctions : MonoBehaviour
{
    private GameObject player;
    public float maxHealth, currentHealth;
    public GameObject healthBar, deathExplosion;
    public bool isBoss, speedTower, regenTower;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        currentHealth = maxHealth;
        
        if (isBoss)
        {
            healthBar = Instantiate(healthBar, GameObject.Find("Canvas").transform);//, new Vector3(0, -28, 0), Quaternion.identity, GameObject.Find("Canvas").transform);
            healthBar.GetComponent<HealthBar>().SetValues(currentHealth, maxHealth);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null && !speedTower && !regenTower)
        {
            transform.right = Vector3.Normalize(player.transform.position - transform.position);//face the player
            if (healthBar != null)
            {

                healthBar.SendMessage("SetHealth", currentHealth);
            }
            if (currentHealth > maxHealth) currentHealth = maxHealth;
            if (currentHealth <= 0)
            {
                player.SendMessage("GravitySpeedSet", false);
                
            }
        }
        if (currentHealth <= 0)
        {
            
            Death();
        }


    }

    void Death()
    {
        if (isBoss)
        {
            for (int j = 0; j < 6; j++)
            {
                Instantiate(deathExplosion, transform.position + new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), 0), Quaternion.identity);
                Destroy(healthBar);
            }
            Instantiate(deathExplosion, transform.position, Quaternion.identity);
        }
        else if (speedTower || regenTower)
        {
            try {
                if (speedTower)
                {
                    GameObject.Find("Boss4").SendMessage("decreaseSpeed");
                }
                else GameObject.Find("Boss4").SendMessage("decreaseRegen");
            }
            catch (NullReferenceException e)
            {

            }
            
            for (int j = 0; j < 10; j++)
            {
                Instantiate(deathExplosion, transform.position + new Vector3(UnityEngine.Random.Range(-3f, 3f), UnityEngine.Random.Range(-3f, 3f), 0), Quaternion.identity);
                
            }
            Instantiate(deathExplosion, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(deathExplosion, transform.position, Quaternion.identity);
        }
        
        
        Destroy(gameObject);

    }

    public void damage(float damage)
    {
        currentHealth -= damage;
        
    }

    public void SetName(string myName)
    {
        healthBar.GetComponent<HealthBar>().ownerName.text = myName;
    }
}
