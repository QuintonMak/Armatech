using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            float deltaX = player.transform.position.x - transform.position.x;
            float deltaY = player.transform.position.y - transform.position.y;
            //if (player.transform.position.y >= 5f || player.transform.position.y <= -5f) deltaY = 0;
            transform.Translate(new Vector3(deltaX, deltaY, 0));
        }
        
    }
}
