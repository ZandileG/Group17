using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private GameController gameController;
    [SerializeField] private Enemy[] enemyTypes;
    [SerializeField] private GameObject victoryUI, defeatUI;
    [SerializeField] private GameObject choiceUI;
    [SerializeField] private GameObject DialogueUI;
    [SerializeField] private Text inheritedWeapon, newWeapon;
    [SerializeField] private int level;
    [SerializeField] private int waveDelay = 5;
    [SerializeField] bool isFirstLevel = false;
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private AudioClip waveSpawnSound;
    [SerializeField] private Vector3 nullLocation = new Vector3(-2, -16, 0);

    private AudioSource waveAudio;
    private int finalWeaponChoice;
    public int cropHealth;
    private GameObject inheritWeapon, selfWeapon;
    private int currentLevel, currentWave, waveCount, totalEnemyCount;
    private int displayDelay = 1;

    private void Awake()
    {
        cropHealth = 0;
    }
    private void Start()
    {
        Time.timeScale = 0;
        totalEnemyCount = 0;

        choiceUI.SetActive(true);
        victoryUI.SetActive(false);
        defeatUI.SetActive(false);
        DialogueUI.SetActive(false);
        playerManager = FindObjectOfType<PlayerManager>();
        gameController = FindObjectOfType<GameController>();
        waveAudio = GetComponent<AudioSource>();
        currentWave = 0;
        currentLevel = gameController.GetCurrentLevel();
        waveCount = gameController.GetWaveCount();
        if (isFirstLevel)
        {
            GetWeapons();
        }
        else
        {
            GetWeapons();
        }
        inheritedWeapon.text = inheritWeapon.GetComponent<PlayerWeapon>().GetName();
        newWeapon.text = selfWeapon.GetComponent<PlayerWeapon>().GetName();
        StartCoroutine(WaveDelay());
    }

    private void GetWeapons()
    {
        inheritWeapon = gameController.GetWeaponChoice();
        selfWeapon = gameController.GetLevelWeapon();
    }

    public GameObject GetDefeatScreen()
    {
 
        return defeatUI;
    }

    public int GetTotalEnemyCount()
    {
        return totalEnemyCount;
    }

    public void KillEnemy()
    {
        totalEnemyCount--;
        Debug.Log(totalEnemyCount);
        if (totalEnemyCount == 0)
        {
            currentWave++;
            if (currentWave >= waveCount)
            {
                Time.timeScale = 0.25f;
                StartCoroutine(UIDelay());
            }
            else
                StartCoroutine(WaveDelay());
        }
    }

    public void SpawnEnemies()
    {
        waveAudio.PlayOneShot(waveSpawnSound);
        totalEnemyCount = 0;
        for (int i = 0; i < 3; i++)
        {
            int enemyCount = gameController.GetEnemyCount(currentWave, i);
            
            for (int j  = 0; j < enemyCount; j++)
            {
                totalEnemyCount++;
                float randomOffsetX = Random.Range(-5, 5);
                float randomOffsetY = Random.Range(-5, 5);
                Vector3 randomOffsets = new Vector3(randomOffsetX, randomOffsetY, 0);
                int randomPos = Random.Range(0, spawnPoints.Length);

                Instantiate(enemyTypes[i], spawnPoints[randomPos].transform.position + randomOffsets, Quaternion.identity);
            }
        }
    }

    public void AddCrop(int health)
    {
        cropHealth += health;
    }

    public void DamageCrop(int damage)
    {
        cropHealth -= damage;
        if (cropHealth <= 0)
        {
            UnlockCursor();
            defeatUI.SetActive(true);
            Time.timeScale = 0;
            Debug.Log("You Lose");
        }
    }

    public Vector3 GetNullLocation()
    {
        return nullLocation;
    }
    public void ChooseOldWeapon()
    {
        playerManager.SetWeapon(inheritWeapon);
        choiceUI.SetActive(false);
        LockCursor();

        finalWeaponChoice = 0;
        Time.timeScale = 1;

    }

    public void ChooseNewWeapon()
    {
        playerManager.SetWeapon(selfWeapon);
        choiceUI.SetActive(false);
        LockCursor();

        finalWeaponChoice = 1;
        Time.timeScale = 1;
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ShowVictoryScreen()
    {
        if (finalWeaponChoice == 0)
        {
            gameController.SetWeaponChoice(gameController.GetWeaponChoiceValue());
        }
        else
        {
            gameController.SetWeaponChoice(gameController.GetCurrentLevel() - 1);
        }
        UnlockCursor();
        DialogueUI.SetActive(false);
        victoryUI.SetActive(true);
        Time.timeScale = 0;
    }

    IEnumerator WaveDelay()
    {
        yield return new WaitForSeconds(waveDelay);
        SpawnEnemies();
    }

    IEnumerator UIDelay()
    {
        yield return new WaitForSeconds(displayDelay);
        Time.timeScale = 0f;
        DialogueUI.SetActive(true);
        UnlockCursor();
    }

}
