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
    [SerializeField] private Text inheritedWeapon, newWeapon;
    [SerializeField] private int level;
    [SerializeField] private int waveDelay = 5;
    [SerializeField] bool isFirstLevel = false;
    private GameObject inheritWeapon, selfWeapon;
    private int currentLevel, currentWave, waveCount, totalEnemyCount;

    private void Start()
    {
        Time.timeScale = 0;
        totalEnemyCount = 0;
        choiceUI.SetActive(true);
        victoryUI.SetActive(false);
        defeatUI.SetActive(false);
        playerManager = FindObjectOfType<PlayerManager>();
        gameController = FindObjectOfType<GameController>();
        currentWave = 0;
        currentLevel = gameController.GetCurrentLevel();
        waveCount = gameController.GetWaveCount();
        StartCoroutine(WaveDelay());
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
        if (totalEnemyCount == 0)
        {
            currentWave++;
            if (currentWave >= waveCount)
            {
                victoryUI.SetActive(true);
                Time.timeScale = 0;
            }
            else
                StartCoroutine(WaveDelay());
        }
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < 3; i++)
        {
            int enemyCount = gameController.GetEnemyCount(currentWave, i);
            totalEnemyCount += enemyCount;
            for (int j  = 0; j < enemyCount; j++)
            {
                Instantiate(enemyTypes[i], gameObject.transform);
            }
        }
    }

    public void ChooseOldWeapon()
    {
        playerManager.SetWeapon(inheritWeapon);
        choiceUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void ChooseNewWeapon()
    {
        playerManager.SetWeapon(selfWeapon);
        choiceUI.SetActive(false);
        Time.timeScale = 1;
    }

    IEnumerator WaveDelay()
    {
        yield return new WaitForSeconds(waveDelay);
        SpawnEnemies();
    }

}
