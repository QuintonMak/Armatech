using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private float cooldown;
    private float damage;
    
    public int actsSinceLastShot;
    public GameObject bullet, gunEnd;
    public AudioSource shootSound;

    // Start is called before the first frame update
    void Start()
    {
        damage = GameMaster.Instance.damage;
        cooldown = GameMaster.Instance.cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 0;
        transform.right = Vector3.Normalize(mousePos - transform.position);
        if (Input.GetMouseButton(0) && actsSinceLastShot >= cooldown)
        {
            GameObject b = Instantiate(bullet, gunEnd.transform.position, Quaternion.identity);

            b.GetComponent<Bullet>().CreateMe(transform.right, damage);
            //b.SendMessage("Create", transform.right);
            

            actsSinceLastShot = 0;
            shootSound.PlayOneShot(shootSound.clip, 1);
        }

        if(actsSinceLastShot <= cooldown) actsSinceLastShot++;

        
    }

    
}
