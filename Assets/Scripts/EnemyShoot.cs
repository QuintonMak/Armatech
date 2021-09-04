using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public float range, spread;
    public int shootRate, shootCycle, actsSinceLastCycle, numShots;//shootCycle > shootRate 
    int shootModulus;//number so that the shooting starts right at shootCycle - shootRate * numShots
    private GameObject player;
    public GameObject bullet;
    public GameObject gunEnd;
    public bool delayAttack, canShoot;//canShoot needed for boss1
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        if(delayAttack==false) actsSinceLastCycle = shootCycle - shootRate * numShots;
        canShoot = true;
        shootModulus = (shootCycle - shootRate * numShots) % shootRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if ((player.transform.position - transform.position).magnitude <= range)//if in range, shoot
            {

                if (actsSinceLastCycle >= shootCycle - shootRate * numShots && canShoot)
                {

                    if (actsSinceLastCycle % shootRate == shootModulus)
                    {
                        GameObject b = Instantiate(bullet, gunEnd.transform.position, Quaternion.identity);
                        b.SendMessage("Create", transform.right);
                        b.transform.eulerAngles += new Vector3(0, 0, Random.Range(-spread, spread));

                    }

                }
                if (actsSinceLastCycle >= shootCycle - 1) actsSinceLastCycle = 0;
                else actsSinceLastCycle++;
            }
            else
            {
                if (!delayAttack) actsSinceLastCycle = shootCycle - shootRate * numShots;
                else    actsSinceLastCycle = 0;

            }





        }
    }

    void FixedUpdate()
    {
        
    }

    public void SetShoot(bool s)
    {
        canShoot = s;
    }

    
}
