using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Standoff : MonoBehaviour
{
    public float delayActs, actsPassed;
    public static float hitDamage;
    
    public bool shot, started;
    public TextManager text;
    public HealthBar bossHealth;
    public GameObject button, standOffButton;
    public AudioSource shootSound;
    // Start is called before the first frame update
    void Start()
    {
        actsPassed = 0;
        delayActs = Random.Range(60f, 150f);
        hitDamage = 0;
        bossHealth.SetValues(2000f, 2000f);
        button.SetActive(false);
        started = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (started)
        {
            actsPassed++;
            if (standOffButton != null) Destroy(standOffButton); 
        }
        if(actsPassed >= delayActs)
        {
            if(!shot) transform.Translate(35, 0, 0);
            if (Input.GetMouseButton(0) && !shot)
            {
                if (Mathf.Abs(transform.localPosition.x) <= 120) hitDamage = 400;
                else hitDamage = 0;
                shot = true;
                bossHealth.Damage(hitDamage);
                if(Mathf.Abs(transform.localPosition.x) <= 120) text.script[1] = "HIT!";
                else text.script[1] = "MISS!";
                text.nextLine();
                button.SetActive(true);
                shootSound.PlayOneShot(shootSound.clip, 1);
            }
        }
    }

    public void beginStandoff()
    {
        started = true;
    }

}
