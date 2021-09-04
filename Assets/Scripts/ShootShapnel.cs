using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootShapnel : MonoBehaviour
{
    public GameObject shrapnel;
    public Vector3 startingPoint;
    // Start is called before the first frame update
    void Start()
    {
        startingPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles += new Vector3(0f, 0f, 1f);

        if ((transform.position - startingPoint).magnitude > 15f)
        {
            for (int i = 0; i < 8; i++)
            {
                GameObject b = Instantiate(shrapnel, transform.position, Quaternion.identity);
                b.SendMessage("Create", new Vector3(Mathf.Cos(i * 45), Mathf.Sin(i * 45), 0));
                

            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 8 || other.gameObject.layer == 3)
            for (int i = 0; i < 8; i++)
            {
                GameObject b = Instantiate(shrapnel, transform.position, Quaternion.identity);
                b.SendMessage("Create", new Vector3(Mathf.Cos(i * 45), Mathf.Sin(i * 45), 0));
                

            }
            
        
        
    }
}
