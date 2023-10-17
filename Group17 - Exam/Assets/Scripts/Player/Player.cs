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
    [SerializeField] private GameObject defeatUI;
    [SerializeField] private float InvilFrames;
    [SerializeField] private PlayerManager playerManager;
    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        defeatUI = playerManager.GetUI();
        defeatUI.SetActive(false);
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
            defeatUI.SetActive(true);
            Time.timeScale = 0;
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
