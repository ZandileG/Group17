using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private string playerName;
    [SerializeField] Slider healthBar;
    [SerializeField] private BoxCollider2D playerHitbox;
    [SerializeField] private float InvilFrames;
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
            Debug.Log("You Died");
        }
        playerHitbox.enabled = false;
        StartCoroutine(PlayerInvil());
    }

    IEnumerator PlayerInvil()
    {
        yield return new WaitForSeconds(InvilFrames);
        playerHitbox.enabled = true;
    }
}
