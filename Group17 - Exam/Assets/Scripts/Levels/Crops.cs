using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crops : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] Slider healthBar;
    [SerializeField] LevelManager levelManager;
    private void Start()
    {
        this.GetComponent<Collider2D>().enabled = true;
        levelManager = FindObjectOfType<LevelManager>();
        healthBar.maxValue = health;
        healthBar.value = health;
        levelManager.AddCrop(health);
    }
    public void Damage(int damage)
    {
        health -= damage;
        healthBar.value = health;
        if (health <= 0)
        {
            healthBar.value = 0;
            Debug.Log("Crops Destroyed");
        }
        this.GetComponent<Collider2D>().enabled = false;
    }

}
