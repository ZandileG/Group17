using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPack : MonoBehaviour
{
    [SerializeField] private Slider timer;
    [SerializeField] private GameObject timerDisplay;
    [SerializeField] private int scanRange = 3;
    [SerializeField] private LayerMask playerLayer;
    private Collider2D player;
    private int healthTimer, maxTime = 5;
    private bool playerInRange;
    // Start is called before the first frame update
    void Start()
    {
        timer.maxValue = maxTime;
        timer.value = 0;
        healthTimer = 0;       
        timerDisplay.SetActive(false);
        playerInRange = false;
        InvokeRepeating("HealTimer", 1f, 1f);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        player  = Physics2D.OverlapCircle(transform.position,scanRange,playerLayer);
        if (player != null)
            if (player.GetComponent<Player>().IsDummy())
            playerInRange = false;
            else
                playerInRange = true;
        else
            playerInRange = false;
    }

    private void HealTimer()
    {
        if (playerInRange && player.GetComponent<Player>().DoesPlayerNeedHealing())
        {
            timerDisplay.SetActive(true);
            if (healthTimer == maxTime)
            {
                player.GetComponent<Player>().Heal();
                timerDisplay.SetActive(false);
                healthTimer = 0;
                timer.value = 0;
            }
            else
            {
                timer.value = healthTimer;
                healthTimer++;
            }
                
        }
        else
        {
            timerDisplay.SetActive(false);
            healthTimer = 0;
            timer.value = 0;
        }
    }

    private void OnDrawGizmosSelected()
    {

        //Gizmos.DrawSphere(transform.position, scanRange);
    }


}
