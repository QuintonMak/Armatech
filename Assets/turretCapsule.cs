using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretCapsule : MonoBehaviour
{
    public GameObject turret;
    public int turretActs;
    public AudioSource shootSound;
    
    // Start is called before the first frame update
    void Start()
    {
        turretActs = 0;
        if (shootSound != null) shootSound.PlayOneShot(shootSound.clip, 1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * 0.05f);
        turretActs++;
        if(turretActs >= 90)
        {
            Instantiate(turret, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.layer == 8)
        {

            Instantiate(turret, transform.position, Quaternion.identity);
            Destroy(gameObject);

        }

    }
}
