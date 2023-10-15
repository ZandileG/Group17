using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crops : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] Slider healthBar;
    private void Start()
    {
        healthBar.maxValue = health;
        healthBar.value = health;
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
    }

}
