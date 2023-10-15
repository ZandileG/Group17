using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int destroyTime = 5;
    [SerializeField] private bool canPierce = false;
    private int damage = 0;

    void Start()
    {
        StartCoroutine(DestroySelf());
    }

    public void Fire(Vector2 velocity)
    {

        //GetComponent<Rigidbody2D>().AddForce(velocity);
        GetComponent<Rigidbody2D>().velocity = velocity;
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }

    public int GetDamage()
    {
        return damage;
    }

    public void SetDamage(int d)
    {
        damage = d;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            //Debug.Log("Hit");
            enemy.Damage(damage);
            if (!canPierce)
                Destroy(gameObject);
        }
    }


}

