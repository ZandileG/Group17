using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crops : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private Slider healthBar;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject cropHolder;
    [SerializeField] private SpriteRenderer[] crops;
    [SerializeField] private int cropsCount;
    private void Start()
    {
        this.GetComponent<Collider2D>().enabled = true;
        levelManager = FindObjectOfType<LevelManager>();

        cropsCount = 0;
        foreach (SpriteRenderer crop in cropHolder.GetComponentsInChildren<SpriteRenderer>())
        {
            crops[cropsCount] = crop;
            cropsCount++;
        }
        health = cropsCount;
        //healthBar.maxValue = health;
        //healthBar.value = health;
        levelManager.AddCrop(health);
    }
    public void Damage(int damage)
    {
        health -= damage;
        //healthBar.value = health;
        levelManager.DamageCrop(damage);
        for (int i = 0; i < damage; i++)
        {
            if (cropsCount > 0)
            {
                cropsCount--;
                int randomCrop = Random.Range(0, cropsCount);
                crops[randomCrop].enabled = false;
                for (int j = randomCrop; j < cropsCount; j++)
                {
                    crops[j] = crops[j + 1];
                }
            }
        }
        if (health <= 0)
        {
            //healthBar.value = 0;
            this.GetComponent<Collider2D>().enabled = false;
            Debug.Log("Crops Destroyed");
        }
       
    }

}
