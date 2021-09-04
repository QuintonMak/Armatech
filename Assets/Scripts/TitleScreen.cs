using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    public GameObject imageObject;
    public Image myImage;
    public Text textBox;
    public float counter, appearTime, fadeTime, fadeActs, fadeInActs, fadeInTime;
    public string myText;
    public string[] textArray;
    public int checkPointNum;
    void Awake()
    {
        myImage = imageObject.GetComponent<Image>();
        if (GameMaster.Instance.titleSceneNum == GameMaster.Instance.sceneNum)
        {
            Destroy(myImage.gameObject);
        }


    }
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        textBox.text = "";
        
        myText = "";
        foreach(string s in textArray)
        {
            myText += s + "\n";
            textBox.alignment = TextAnchor.MiddleCenter;
        }
        myText = myText.Substring(0, myText.Length - 1);//backspace to get ride of extra empty line
        
    }

    // Update is called once per frame
    void Update()
    {
        
        counter++;
        if(counter >= fadeTime)
        {
            myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, (fadeActs + fadeTime - counter) / fadeActs);
            textBox.color = new Color(textBox.color.r, textBox.color.g, textBox.color.b, (fadeActs + fadeTime - counter) / fadeActs);
        }
        else if(counter >= appearTime)
        {
            
            textBox.text = myText;
            
            //fade in the text   
            textBox.color = new Color(textBox.color.r, textBox.color.g, textBox.color.b, (counter - appearTime) / fadeInActs);
            
        }


        if (myImage.color.a <= 0)
        {
            if (GameObject.FindGameObjectsWithTag("Title").Length == 1) GameMaster.Instance.titleSceneNum = GameMaster.Instance.sceneNum;//if last title page, set titleSceneNum
            Destroy(myImage.gameObject);
        }
    }
}
