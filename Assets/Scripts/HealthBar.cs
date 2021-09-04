using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public GameObject player;
    public PlayerMovement playerHealth;
    public Text ownerName;

    private void Start()
    {
        //player = GameObject.Find("Player");
        //playerHealth = player.GetComponent<PlayerMovement>();
        //healthBar.maxValue = GameMaster.Instance.maxHealth;
        //healthBar.value = GameMaster.Instance.maxHealth;
        
    }

    private void Update()
    {
        if (healthBar.value < 0) healthBar.value = 0;
    }

    public void SetValues(float curr, float max)
    {
        healthBar.maxValue = max;
        healthBar.value = curr;
    }

    public void SetHealth(float hp)
    {
        healthBar.value = hp;
    }

    public void Damage(float damage)
    {
        healthBar.value -= damage;
    }
}