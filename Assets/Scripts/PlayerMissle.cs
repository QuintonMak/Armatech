using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMissle : Bullet
{
    GameObject targetEnemy;
    Vector3 enemyVector;
    Camera camera;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        targetEnemy = FindClosestEnemy();
        
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        if(targetEnemy != null)
        {
             enemyVector = targetEnemy.transform.position - transform.position;

             transform.right = Vector3.RotateTowards(transform.right, enemyVector, Mathf.PI / 30, 0);
             direction = transform.right;
        }
        else targetEnemy = FindClosestEnemy();

    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemies");
        
        GameObject closest = null;
        float angle = Mathf.Infinity;
        
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 screenPoint = camera.WorldToViewportPoint(go.transform.position);
            bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;//see if enemy is on screen
            Vector3 diff = go.transform.position - position;
            float curAngle = Vector3.Angle(diff, direction);
            
            if (curAngle < angle && onScreen)
            {
                closest = go;
                angle = curAngle;
                
            }
        }
        return closest;
    }
}
