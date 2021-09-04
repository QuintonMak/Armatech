using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UpgradeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler
{
    public Button self;
    public Image myImage;
    public int upgradeNum, cost, prereq;
    public bool selected, highlighted, canupgrade;
    public string description;
    GameObject textbox;

    // Start is called before the first frame update
    void Start()
    {
        textbox = GameObject.Find("ButtonDescription");
        myImage = GetComponent<Image>();
        selected = GameMaster.Instance.upgrades[upgradeNum];
    }

    // Update is called once per frame
    void Update()
    {
        
        if (selected) myImage.color = new Color(0, 197, 21, 255);
        if (highlighted) textbox.GetComponent<Text>().text = description + "\n\nCost: " + cost + " skill point(s). Points Available: " + GameMaster.Instance.skillPoints;
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //do your stuff when highlighted
        highlighted = true;
        //textbox.GetComponent<Text>().text = description + "\nCost: " + cost + " skill points. Points Available: " + GameMaster.Instance.skillPoints;
        myImage.color = new Color(118f/255f, 118f / 255f, 118f / 255f, 166f/255f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlighted = false;
        textbox.GetComponent<Text>().text = "";
        myImage.color = new Color(1, 1, 1, 166f/255f);
    }

    public void OnSelect(BaseEventData eventData)
    {
        //do your stuff when selected
        canupgrade = prereq == -1 || GameMaster.Instance.upgrades[prereq];
        if (!selected && GameMaster.Instance.skillPoints >= cost && !GameMaster.Instance.upgrades[upgradeNum] && canupgrade)
        {
            GameMaster.Instance.Upgrade(upgradeNum);
            GameMaster.Instance.skillPoints -= cost;
            selected = true;

        }
        
    }

    public void Reset()
    {
        if (selected)
        {
            GameMaster.Instance.skillPoints += cost;
            
        }
            selected = false;
        highlighted = false;
        myImage.color = new Color(1, 1, 1, 166f / 255f);

    }

    
}
