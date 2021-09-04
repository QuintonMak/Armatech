using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Explosion : MonoBehaviour
{
    public float cooldown;
    float counter;
    public SpriteRenderer myImage;
    public AudioSource explodeSound;
    // Start is called before the first frame update
    void Start()
    {
        counter = cooldown;
        if(explodeSound != null) explodeSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        counter--;
        myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, counter / (cooldown*2));
        if(counter == 0)
        {
            //Destroy(gameObject);
        }
    }
}
