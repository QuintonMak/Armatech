using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilities : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public float repairCooldown, repairActs;
    public Image repairIcon;

    public float warpCooldown, warpActs, slowDuration;
    public Image warpIcon;
    

    public float teleportDistance;

    public float missleCooldown, missleActs, numMissles, missleDamage;
    public AudioSource missleSound, warpSound, repairSound;
    public Image missleIcon;
    public GameObject missle, rocket, gunEnd;
    // Start is called before the first frame update
    void Start()
    {
        repairCooldown = GameMaster.Instance.repairCooldown;
        warpCooldown = GameMaster.Instance.warpCooldown;
        missleCooldown = GameMaster.Instance.missleCooldown;

        repairActs = repairCooldown;
        warpActs = warpCooldown;
        missleActs = missleCooldown;

        slowDuration = GameMaster.Instance.slowDuration;
        numMissles = GameMaster.Instance.numMissles;
        missleDamage = GameMaster.Instance.missleDamage;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e") && repairActs >= repairCooldown)
        {
            RepairSelf();
        }
        else if(Input.GetKeyDown("f") && warpActs >= warpCooldown )
        {
            TimeWarp();
            
        }
        else if (Input.GetMouseButton(1) && missleActs >= missleCooldown)
        {
            MissleSalvo();

        }
        CoolDowns();
        
    }

    void CoolDowns()
    {
        
        repairActs++;

        repairIcon.fillAmount = Mathf.Clamp(repairActs / repairCooldown, 0, 1);

        //if (warpActs == 0) playerMovement.shield = GameMaster.Instance.upgrades[9];
        //if (warpActs == slowDuration) playerMovement.shield = false;
        warpActs++;
        warpIcon.fillAmount = Mathf.Clamp(warpActs / warpCooldown, 0, 1);

        missleActs++;
        missleIcon.fillAmount = Mathf.Clamp(missleActs / missleCooldown, 0, 1);
    }

    void RepairSelf()
    {
         playerMovement.RepairMe();
         repairActs = 0;
         repairSound.PlayOneShot(repairSound.clip, 1);
    }

    void TimeWarp()
    {
        
        warpActs = 0;
        if (GameMaster.Instance.upgrades[9])
        {
            repairActs += repairCooldown * 0.25f;
            missleActs += missleCooldown * 0.25f;
        }
        warpSound.PlayOneShot(warpSound.clip, 1);

    }

    void MissleSalvo()
    {
        
        GameObject b = Instantiate(rocket, gunEnd.transform.position, Quaternion.identity);

        b.GetComponent<PlayerRocket>().CreateMe(transform.right, missleDamage);

        for (int i = 0; i < numMissles; i++)
        {
            GameObject m = Instantiate(missle, transform.position, Quaternion.identity);
            m.GetComponent<PlayerMissle>().CreateMe(transform.right, 50f);
            m.transform.Rotate(new Vector3(0, 0, 360 * i / numMissles));
        }
        missleActs = 0;
        missleSound.PlayOneShot(missleSound.clip, 1);
    }
    
    public void SavePlayer()
    {
        GameMaster.Instance.repairCooldown = repairCooldown;
        GameMaster.Instance.warpCooldown = warpCooldown;
        GameMaster.Instance.missleCooldown = missleCooldown;

        GameMaster.Instance.slowDuration = slowDuration;
        GameMaster.Instance.numMissles = numMissles;
        GameMaster.Instance.missleDamage = missleDamage;
    }

    
}
