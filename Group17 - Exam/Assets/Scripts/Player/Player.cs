using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currenthealth;
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
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        currenthealth = maxHealth;
    }
    public void Damage(int damage)
    {
        currenthealth -= damage;
        healthBar.value = currenthealth;
        if (currenthealth <= 0)
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

    public void Heal()
    {
        currenthealth = maxHealth;
        healthBar.value = currenthealth;
    }

    public bool DoesPlayerNeedHealing()
    {
        return (currenthealth < maxHealth);
    }
}
