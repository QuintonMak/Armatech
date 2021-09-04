using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rigid;
    
    public float speed, maxSpeed;
    public bool shield;
    
    [SerializeField] public float maxHealth, currentHealth, maxProtection, currentProtection;
    public GameObject healthBar, protectionBar, deathExplosion;

    public Vector3 velocity;
    public SpriteRenderer mySprite;
    public Sprite defaultSprite, shieldSprite;
    public Color c;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = GameMaster.Instance.maxHealth;
        currentHealth = GameMaster.Instance.currentHealth;
        maxProtection = GameMaster.Instance.maxProtection;
        currentProtection = GameMaster.Instance.currentProtection;
        
        healthBar.GetComponent<HealthBar>().SetValues(currentHealth, maxHealth);
        protectionBar.GetComponent<HealthBar>().SetValues(currentProtection, maxProtection);
        maxSpeed = speed;

        
    }

    // Update is called once per frame
    void Update()
    {
        c = mySprite.color;

        velocity = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        velocity = Vector3.Normalize(velocity); //change to unit vector
        
        velocity = velocity * speed; // v(unit) * v

        //transform.Translate(displacement, Space.World);

        rigid.velocity = velocity;

        if(currentHealth <= 0)
        {
            Death();
            

        }
    }

    void FixedUpdate()
    {
        
    }

    void Death()
    {
        GameMaster.Instance.playerDeath();
        for (int j = 0; j < 4; j++)
        {
            Instantiate(deathExplosion, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0), Quaternion.identity);
        }
        GameMaster.Instance.sceneNum = GameMaster.Instance.checkPointNum;
        Destroy(gameObject);
        
    }

    

    public void damage(float damage)
    {
        if (shield) damage = damage * 0.50f;
        if (currentProtection > 0) currentProtection -= damage;
        else currentHealth -= damage;

        if (currentProtection < 0)
        {
            currentHealth += currentProtection;
            currentProtection = 0;
        }

        healthBar.SendMessage("SetHealth", currentHealth);
        protectionBar.SendMessage("SetHealth", currentProtection);

    }

    public void RepairMe()
    {
        StartCoroutine("Repair");
    }

    IEnumerator Repair()
    {

        for (int i = 0; i < 120; i++)
        {
            if (i % 24 == 0)
            {
                if (currentProtection < maxProtection - maxProtection/10) currentProtection += maxProtection / 10;
                else currentProtection = maxProtection;
                protectionBar.SendMessage("SetHealth", currentProtection);
                //if (currentHealth < maxHealth - maxHealth / 10 && GameMaster.Instance.upgrades[1]) currentHealth += maxHealth / 10;
                //else if (GameMaster.Instance.upgrades[2]) currentHealth = maxHealth;
                healthBar.SendMessage("SetHealth", currentHealth);
            }
            if (i < 119)
            {
                shield = GameMaster.Instance.upgrades[10];
                if(shield) mySprite.sprite = shieldSprite;

            }
            else
            {
                shield = false;
                mySprite.sprite = defaultSprite;
            }

            yield return null;
        }
    }

    public void SavePlayer()
    {
        GameMaster.Instance.maxHealth = maxHealth;
        GameMaster.Instance.currentHealth = currentHealth;
        GameMaster.Instance.maxProtection = maxProtection;
        GameMaster.Instance.currentProtection = currentProtection;
    }

    public void SlowMe()
    {
        StartCoroutine("Slow");
    }

    IEnumerator Slow()
    {

        for (int i = 0; i <= 100; i++)
        {
            if (i < 100)
            {
                speed = maxSpeed * 0.5f;
                mySprite.color = new Color(32f/255f, 190f/255f, 204f/255f, 1);
            }
            else if (i == 100)
            {
                speed = maxSpeed;
                mySprite.color = new Color(1, 1, 1, 1);
            }
            yield return null;
        }
    }

    public void GravitySpeedSet(bool pulled)
    {
        if (pulled) speed = 0;
        else speed = maxSpeed;
    }

    public void GravityPull(Vector3 velocity)
    {
        rigid.velocity = velocity;
    }
    
}
