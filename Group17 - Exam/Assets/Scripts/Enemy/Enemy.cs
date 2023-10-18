using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int attackDamage;
    [SerializeField] private Slider healthDisplay;
    [SerializeField] private LevelManager levelManager;
    // Start is called before the first frame update
    
    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        healthDisplay.maxValue = health;
        healthDisplay.value = health;
    }

    public int GetDamage()
    {
        return attackDamage;
    }

    private void Kill()
    {
        levelManager.KillEnemy();
        Debug.Log("Killed");
        Destroy(gameObject);
    }

    public void Damage(int damage)
    {
        health -= damage;
        healthDisplay.value = health;
        if (health <= 0)
        {
            Kill();
            //Debug.Log("Enemy Killed");
           
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            player.Damage(attackDamage);
        }
        if (other.TryGetComponent<Crops>(out Crops crop))
        {
            crop.Damage(attackDamage);
        }
    }

}
