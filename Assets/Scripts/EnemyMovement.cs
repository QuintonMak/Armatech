using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rigid;
    protected GameObject player;
    protected float multiplier;
    // Start is called before the first frame update
    public void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        multiplier = 1;
        if(player != null)
        {
            if (player.GetComponent<PlayerAbilities>().warpActs < player.GetComponent<PlayerAbilities>().slowDuration)
            {
                multiplier = 0.5f;
            }
        }
        
        Move();
    }

    public virtual void Move()
    {
        
        if (Physics2D.Raycast(transform.position, transform.right, 0.8f, 1 << 8))
        {
            rigid.velocity = new Vector3(-transform.right.y, transform.right.x, 0) * speed * multiplier;
            //Debug.Log("wall in front");
        }
        else rigid.velocity = transform.right * speed * multiplier;
        
        
    }

    


}
