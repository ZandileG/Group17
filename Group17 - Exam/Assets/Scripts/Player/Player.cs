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
    [SerializeField] private GameObject defeatUI;
    [SerializeField] private float InvilFrames;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private GameObject dummy;   
    [SerializeField] private int dummyCount;
    [SerializeField] private bool isDummy = false;
    private KeyCode dropDummy = KeyCode.Q;
    private bool isInvil;

    private void Start()
    {
        if (!isDummy)
        {
            isInvil = false;
            playerManager = GetComponent<PlayerManager>();
            defeatUI = playerManager.GetUI();
            defeatUI.SetActive(false);
            healthBar.maxValue = maxHealth;
            healthBar.value = maxHealth;
            currenthealth = maxHealth;
        }
    }

    private void Update()
    {
        if (!isDummy)
        {
            if (Input.GetKey(dropDummy))
            {
                if (dummyCount > 0)
                {
                    SpawnDummy();

                }
            }
        }
    }
    public void Damage(int damage)
    {
        if (!isDummy)
        {
            if (!isInvil)
            {
                currenthealth -= damage;
                healthBar.value = currenthealth;
                if (currenthealth <= 0)
                {
                    UnlockCursor();
                    healthBar.value = 0;
                    defeatUI.SetActive(true);
                    Time.timeScale = 0;
                }
                isInvil = true;
                StartCoroutine(PlayerInvil());
            }
        } else
        {
            currenthealth -= damage;
            if (currenthealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    IEnumerator PlayerInvil()
    {
        yield return new WaitForSeconds(InvilFrames);
        isInvil = false;
    }

    public void Heal()
    {
        currenthealth = maxHealth;
        healthBar.value = currenthealth;
    }

    public bool IsDummy()
    {
        return isDummy;
    }

    private void SpawnDummy()
    {
        GameObject newDummy = Instantiate(dummy, this.transform.position, this.transform.rotation);
        dummyCount--;
    }

    public void SetHealth(int newHealth)
    {
        maxHealth = newHealth;
    }

    public void SetInvil(bool state)
    {
        isInvil = state;
    }
    public bool DoesPlayerNeedHealing()
    {
        return (currenthealth < maxHealth);
    }
}
