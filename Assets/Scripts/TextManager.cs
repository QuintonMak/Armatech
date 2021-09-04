using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Text gameText;
    int lineNum;
    public string[] script;
    
    void Start()
    {
        lineNum = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            gameText.text = script[lineNum];
        }
        catch (IndexOutOfRangeException e)
        {
            if(GameMaster.Instance != null)
            {
               
                GameMaster.Instance.sceneNum++;
                GameMaster.Instance.LoadScene();
            }
            
        }
        
        
    }

    public void nextLine()
    {
        lineNum++;
    }

    public void skip()
    {
        if (GameMaster.Instance != null)
        {

            GameMaster.Instance.sceneNum++;
            GameMaster.Instance.LoadScene();
        }
    }
}
