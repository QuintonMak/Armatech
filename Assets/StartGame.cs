using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void prologue()
    {
        SceneManager.LoadScene("Prologue Text");
    }

    public void stageOne()
    {
        SceneManager.LoadScene("STAGE 1");
    }

    public void begin()
    {
        if (GameMaster.Instance != null)
        {
            GameMaster.Instance.sceneNum = 2;
            GameMaster.Instance.SetDefaultValues();
        }
        SceneManager.LoadScene("Text 1");
    }

    public void playAgain()
    {
        if (GameMaster.Instance != null)
        {
            GameMaster.Instance.sceneNum = 0;
            GameMaster.Instance.SetDefaultValues();
        }
        SceneManager.LoadScene("Start");
    }

    public void next()
    {
        GameMaster.Instance.sceneNum++;
        GameMaster.Instance.LoadScene();
    }

    public void resetButtons()
    {
        GameMaster.Instance.SetDefaultValues();
    }
}
