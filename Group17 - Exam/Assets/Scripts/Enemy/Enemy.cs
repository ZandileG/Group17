using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int attackDamage;
    [SerializeField] private Slider healthDisplay;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private AudioClip hitSound, AmbientSound;

    private AudioSource enemyAudio;
    private bool isDead;
    // Start is called before the first frame update
    
    private void Start()
    {
        isDead = false;
        enemyAudio = GetComponent<AudioSource>();
        levelManager = FindObjectOfType<LevelManager>();
        healthDisplay.maxValue = health;
        healthDisplay.value = health;
        InvokeRepeating("PlayAmbient", 1f, 10f);
    }

    public int GetDamage()
    {
        return attackDamage;
    }

    private void Kill()
    {
        if (!isDead)
        {
            isDead = true;
            levelManager.KillEnemy();
            Debug.Log("Killed");
            Destroy(gameObject);
        }
    }

    public void Damage(int damage)
    {
        health -= damage;
        healthDisplay.value = health;
        enemyAudio.PlayOneShot(hitSound);
        if (health <= 0)
        {
            Kill();
           
        }
    }

    private void PlayAmbient()
    {
        int random = Random.Range(0, 5);
        StartCoroutine(RandomDelay(random));
    }

    IEnumerator RandomDelay(int delay)
    {
        yield return new WaitForSeconds(delay);
        enemyAudio.PlayOneShot(AmbientSound);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        /*
        if (other.TryGetComponent<Player>(out Player player))
        {
            player.Damage(attackDamage);
        }
        if (other.TryGetComponent<Crops>(out Crops crop))
        {
            crop.Damage(attackDamage);
        }
        */
    }

}
