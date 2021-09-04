using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float speed;
    public Collider2D myCollider;
    public float damage;
    [SerializeField] protected int targetLayer;
    //public GameObject shooter;
    protected GameObject player;

    protected Vector3 direction;

    protected float multiplier;

    public GameObject explosion;

    public AudioSource shootSound, hitSound;//probably not neccesary
    // Start is called before the first frame update
    public void Start()
    {
        direction = transform.right;
        player = GameObject.Find("Player");
        if(shootSound != null) shootSound.PlayOneShot(shootSound.clip, 1);
    }

    // Update is called once per frame
    public void Update()
    {
        
        multiplier = 1;
        if (player != null)
        {
            if (targetLayer == 3 && player.GetComponent<PlayerAbilities>().warpActs < player.GetComponent<PlayerAbilities>().slowDuration)
            {
                multiplier = 0.33f;
            }
        }
        Vector3 d = direction * speed * multiplier * Time.deltaTime;
        transform.Translate(d, Space.World);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
            if (other.gameObject.layer == targetLayer)//hard code the obstacle layer
            {

                other.gameObject.SendMessage("damage", damage);
                
                Destroy(this.gameObject);
                
            }
            else if (other.gameObject.layer == 8)
            {

                if(other.gameObject.tag.Equals("Tower")) other.gameObject.SendMessage("damage", damage);
                
                Destroy(this.gameObject);

            }
            
            
        
    }

    public void CreateMe(Vector3 d, float damage)
    {
        transform.right = Vector3.Normalize(d);
        this.damage = damage;
    }

    public void Create(Vector3 d)
    {
        transform.right = Vector3.Normalize(d);

    }



    void OnBecameInvisible()
    {
        //Destroy(gameObject);
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    
}
