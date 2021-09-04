using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance;
    public GameObject player, standardEnemy, tankEnemy, meleeEnemy, mortarEnemy, turret, bossOne, bossTwo;
    public GameObject wall, shack, log, machine, brokenWall;
   
    //Player stats
    public float maxHealth, currentHealth, maxProtection, currentProtection;
    public float damage, cooldown;

    public float repairCooldown;
    public float warpCooldown, slowDuration;
    public float missleCooldown, numMissles, missleDamage;


    public int sceneNum, checkPointNum, titleSceneNum;
    public int skillPoints;

    //public string[] sceneNames;
    public bool[] upgrades = new bool[11];

    

    void Awake()
    {
        Application.targetFrameRate = 60;
        sceneNum = 0;
        try
        {
            if (GameObject.Find("TitleScreen") != null) GameMaster.Instance.titleSceneNum = GameMaster.Instance.sceneNum;
        }
        catch (NullReferenceException e)
        {

        }
        
        //sceneNames = new string[4];
        //for (int i = 0; i < sceneNames.Length; i++)
        //{
        //    sceneNames[i] = "Scene" + i;
        //}
        if (Instance == null) //first stage
        {
            //PlaceBlocks();
            //for (int i = -2; i < 3; i++)
            //{
            //    Vector3 pos = new Vector3(22, i * 10f, 0);
            //    Instantiate(standardEnemy, pos, Quaternion.identity);
            //}
            //Instantiate(bossOne, new Vector3(10, 0, 0), Quaternion.identity);
        }
        else if(Instance.sceneNum == 3)
        {
            Instance.checkPointNum = 1;
        }
        else if(Instance.sceneNum == 4)
        {
            Instance.currentHealth = Instance.maxHealth;
            Instance.currentProtection = Instance.maxProtection;
            PlaceObstacles(log, true, 15);
            PlaceObstacles(shack, false, 6);
            
            PlaceEnemies(standardEnemy, 5);
            PlaceEnemies(mortarEnemy, 1);
        }
        else if (Instance.sceneNum == 7)//boss 1
        {
            //Instantiate(bossOne, new Vector3(10, 0, 0), Quaternion.identity);
        }
        else if(Instance.sceneNum == 9)
        {
            if(Instance.checkPointNum != 8) Instance.skillPoints += 3;
            Instance.checkPointNum = 8;
            
        }
        else if (Instance.sceneNum == 11)
        {
            Instance.currentHealth = Instance.maxHealth;
            Instance.currentProtection = Instance.maxProtection;
            PlaceWalls();
            PlaceObstacles(machine, false, 10);
            PlaceEnemies(mortarEnemy, 2);
            PlaceEnemies(meleeEnemy, 2);
            PlaceEnemies(standardEnemy, 3);
            PlaceTurrets();
        }
        else if(Instance.sceneNum == 13)//boss 2
        {
            //Instantiate(bossTwo, new Vector3(10, 0, 0), Quaternion.identity);
        }
        else if(Instance.sceneNum == 15)
        {
            if (Instance.checkPointNum != 14) Instance.skillPoints += 3;
            Instance.checkPointNum = 14;
            
        }
        else if (Instance.sceneNum == 17)
        {
            Instance.currentHealth = Instance.maxHealth;
            Instance.currentProtection = Instance.maxProtection;
            PlacePerpendicular(brokenWall, 30);
            PlaceEnemies(tankEnemy, 3);
            PlaceEnemies(standardEnemy, 8);
            PlaceEnemies(meleeEnemy, 4);
            PlaceEnemies(mortarEnemy, 4);
        }
        else if(Instance.sceneNum == 19)
        {
            if (Instance.checkPointNum != 18) Instance.skillPoints += 2;
            Instance.checkPointNum = 18;
            
        }
        else if(Instance.sceneNum == 21)//boss 3
        {
            Instance.currentHealth = Instance.maxHealth;
            Instance.currentProtection = Instance.maxProtection;
        }
        else if(Instance.sceneNum == 24)
        {
            if (Instance.checkPointNum != 23) Instance.skillPoints += 2;
            Instance.checkPointNum = 23;
        }
        else if(Instance.sceneNum == 26)//boss 4
        {
            Instance.currentHealth = Instance.maxHealth;
            Instance.currentProtection = Instance.maxProtection;
        }
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;


        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene()
    {
        try
        {
            SceneManager.LoadScene(sceneNum);
        }
        catch(IndexOutOfRangeException e)
        {

        }
        
        

    }

    void PlaceWalls()
    {
        for (int i = -2; i < 3; i++)
        {
            for (int j = -2; j < 3; j++)
            {
                Vector3 pos = new Vector3(i * 5f, UnityEngine.Random.Range(-20f, 20f), 0);
                GameObject w = Instantiate(wall, pos, Quaternion.identity);
                w.transform.eulerAngles = new Vector3(0, 0, 90f * UnityEngine.Random.Range(0, 2));
            }
        }
    }

    
    /// <summary>
    /// Places a certain number of a certain obstacle into the world
    /// </summary>
    /// <param name="o"></param>
    /// <param name="loopNum"></param>
    /// <param name="randRot"></param> whether to randomize rotations or not
    void PlaceObstacles(GameObject o, bool randRot, int numObstacles)
    {
        for (int i = 0; i < numObstacles; i++)
        {
            
                Vector3 pos = new Vector3(UnityEngine.Random.Range(-20, 20), UnityEngine.Random.Range(-20, 20), 0);
                GameObject w = Instantiate(o, pos, Quaternion.identity);
                if(randRot) w.transform.eulerAngles = new Vector3(0, 0, UnityEngine.Random.Range(0f, 90f));
            
        }
    }

    void PlacePerpendicular(GameObject o, int numObstacles)
    {
        for (int i = 0; i < numObstacles; i++)
        {

            Vector3 pos = new Vector3(UnityEngine.Random.Range(-20, 20), UnityEngine.Random.Range(-20, 20), 0);
            GameObject w = Instantiate(o, pos, Quaternion.identity);
            w.transform.eulerAngles = new Vector3(0, 0, 90f * UnityEngine.Random.Range(0, 4));

        }
    }

    void PlaceEnemies(GameObject e, int numEnemies)
    {
        for (int i = 0; i < numEnemies; i++)
        {
            Vector3 pos = new Vector3(22, UnityEngine.Random.Range(-20, 20), 0);
            Instantiate(e, pos, Quaternion.identity);

        }
    }

    void PlaceTurrets()
    {
        for (int i = 0; i < 4; i++)
        {
            Vector3 pos = new Vector3(UnityEngine.Random.Range(-10, 20), UnityEngine.Random.Range(-20, 20), 0);
            Instantiate(turret, pos, Quaternion.identity);

        }
    }



    public void Upgrade(int upgradeNum)
    {
        if(skillPoints > 0)
        {
            switch (upgradeNum)
            {
                case 0:
                    
                    Instance.maxProtection += 30;
                    break;
                case 1:
                    repairCooldown = repairCooldown * 0.66f;
                    break;
                case 2:
                    Instance.maxHealth += 60;
                    break;
                case 3:
                    damage += 5;
                    break;
                case 6:
                    numMissles += 8;
                    break;
                case 5:
                    cooldown = cooldown * 0.66f;
                    break;
                case 7:
                    warpCooldown = warpCooldown * 0.66f;
                    break;
                case 8:
                    slowDuration += 100;
                    break;
                case 4:
                    missleDamage += 50;
                    break;
            }
            upgrades[upgradeNum] = true;
        }
    }

    public void SetDefaultValues()
    {
        GameMaster.Instance.maxHealth = 100;
        
        GameMaster.Instance.maxProtection = 50;
        GameMaster.Instance.repairCooldown = 600;
        GameMaster.Instance.warpCooldown = 600;
        GameMaster.Instance.missleCooldown = 900;

        GameMaster.Instance.slowDuration = 150;
        GameMaster.Instance.numMissles = 0;
        GameMaster.Instance.missleDamage = 100;
        GameMaster.Instance.damage = 12;
        GameMaster.Instance.cooldown = 10;

        for(int i = 0; i < upgrades.Length; i++)
        {
            upgrades[i] = false;
        }
        GameObject[] objects = GameObject.FindGameObjectsWithTag("UpgradeButton");
        foreach(GameObject o in objects)
        {
            o.SendMessage("Reset");
        }
    }

    public void playerDeath()
    {
        StartCoroutine("death");
    }

    IEnumerator death()
    {
        for (int i = 0; i < 60; i++)
        {

            if (i == 59) SceneManager.LoadScene(GameMaster.Instance.checkPointNum);
            yield return null;
        }
    }
}
