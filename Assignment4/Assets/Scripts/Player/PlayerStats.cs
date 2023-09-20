using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerHealth, playerTreasure;
    [SerializeField] UI gameController;
    [SerializeField] AudioSource playerAudioSource;
    [SerializeField] AudioClip playerHurt, treasurePickup;
    private int health;
    private int treasure;

    public void Start()
    {
        resetPlayer();
    }

    public void resetPlayer()
    {
        health = 6;
        treasure = 0;
        playerHealth.text = health.ToString();
        playerTreasure.text = treasure.ToString();
    }

    public void PlayerHit()
    {
        health--;
        playerHealth.text = health.ToString();
        playerAudioSource.PlayOneShot(playerHurt);
        if (health == 0)
            gameController.PlayerLose();
    }

    public void PickUpTreasure()
    {
        treasure++;
        playerTreasure.text = treasure.ToString();
        playerAudioSource.PlayOneShot(treasurePickup);
        if (treasure == 5)
            gameController.PlayerWin();
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetTreasure()
    {
        return treasure;
    }
}
