using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] LevelManager currentLevelManager;
    //Cointainer for the different weapon choices, set up in the format
    //Weapon, Weapon tier.
    [SerializeField] GameObject[] bowWeapons, revolverWeapons, shotgunWeapons, smgWeapons, arWeapons;
    [SerializeField] GameObject[,] playerWeapons = new GameObject[5,6];
    private int weaponChoice = 3;
    private int currentLevel;
    private int[] waveCount = new int[5] { 2, 2, 3, 3, 5 };
    private int villainOpinion;
    //Container for how many enemies per level
    //Declared in the format of: level, wave, enemy count
    //Enemy count is formated as Tokoloshe Count, Grootslang Count, Ga-Gorib Count
    private int[,,] enemyCount = new int[5, 5, 3] {
        { {1,1,1}, 
          {2,2,1}, 
          {0,0,0}, 
          {0,0,0},
          {0,0,0}
        },
        { {3,2,1},
          {3,2,2},
          {0,0,0},
          {0,0,0},
          {0,0,0}
        },
        { {3,2,3},
          {5,2,2},
          {0,3,3},
          {0,0,0},
          {0,0,0}
        },
        { {10,1,1},
          {5,3,3},
          {8,3,4},
          {0,0,0},
          {0,0,0}
        },
        { {15,0,0},
          {0,6,0},
          {0,0,8},
          {8,5,6},
          {15,6,8}
        }

    };

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        villainOpinion = 0;
        for (int i = 0; i < 6; i++)
        {
            playerWeapons[0, i] = bowWeapons[i];
            playerWeapons[1, i] = revolverWeapons[i];
            playerWeapons[2, i] = shotgunWeapons[i];
            playerWeapons[3, i] = smgWeapons[i];
            playerWeapons[4, i] = arWeapons[i];
        }
        currentLevel = 6;

    }

    public int GetWeaponChoiceValue()
    {
        return weaponChoice;
    }

    private void Start()
    {
        currentLevelManager = FindObjectOfType<LevelManager>();
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public int GetWaveCount()
    {
        int i = currentLevel - 1;
        return waveCount[i];
    }

    public int GetEnemyCount(int wave, int enemy)
    {
        int i = currentLevel - 1;
        return enemyCount[i, wave, enemy];
    }

    public GameObject GetWeaponChoice()
    {
        int i = currentLevel - 1;
        return playerWeapons[weaponChoice, i];
    }

    public GameObject GetLevelWeapon()
    {
        int i = currentLevel - 1;
        return playerWeapons[i,i];
    }

    public void NextLevel()
    {
        currentLevel++;
    }

    public void SetWeaponChoice(int value)
    {
        weaponChoice = value;
    }

    public void ModifyVillainOpinion(int value)
    {
        villainOpinion += value;
    }
    private void FindLevelManager()
    {
        currentLevelManager = FindObjectOfType<LevelManager>();
    }

    public void ResetGame()
    {
        villainOpinion = 0;
        currentLevel = 0;
        weaponChoice = 0;
    }

}
