using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpPoint : MonoBehaviour
{
    public GameObject player;
    private bool loaded;
    public Collider2D myCollider;
    
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
            if ((player.transform.position - transform.position).magnitude <= 1.2f)
            {
                if (GameObject.FindGameObjectsWithTag("Enemies").Length == 0 && !loaded)
                {
                    player.SendMessage("SavePlayer");
                    GameMaster.Instance.sceneNum++;
                    GameMaster.Instance.LoadScene();
                    loaded = true;
                }
            }
        }
        
    }

    void FixedUpdate()
    {
        myCollider.isTrigger = true;
    }
    /**
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 3 && GameObject.FindGameObjectsWithTag("Enemies").Length == 0)
        {
            other.SendMessage("SavePlayer");
            GameMaster.Instance.sceneNum++;
            SceneManager.LoadScene(GameMaster.Instance.sceneNames[GameMaster.Instance.sceneNum]);
        }
    }
    */
}
