using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int damage;
    // Start is called before the first frame update
    

    private void Kill()
    {
        Destroy(gameObject);
    }

    public void Damage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            //Kill();
            Debug.Log("Enemy Killed");
           
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            player.Damage(damage);    
        }
    }
}
