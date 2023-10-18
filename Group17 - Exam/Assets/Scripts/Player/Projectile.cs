using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int destroyTime = 5;
    [SerializeField] private bool canPierce = false;
    private bool hasHit;
    private int damage = 0;

    void Start()
    {
        hasHit = false;
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
        if (!hasHit || canPierce)
        {
            if (other.TryGetComponent<Enemy>(out Enemy enemy))
            {
                //Debug.Log("Hit");
                enemy.Damage(damage);
                hasHit = true;
                if (!canPierce)
                    Destroy(gameObject);

            }
            else if (other.GetComponentInParent<Enemy>() != null)
            {
                Enemy _enemy = other.GetComponentInParent<Enemy>();
                _enemy.Damage(damage);
                hasHit = true;
                if (!canPierce)
                    Destroy(gameObject);
            }
        }
    }


}

