using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private int destroyTime = 5;
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

    public void SetRotation(Vector3 rotation)
    {

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
        if (other.TryGetComponent<Player>(out Player player))
        {
            //Debug.Log("Hit");
            if (player.Damage(damage))
                Destroy(gameObject);
        }

    }
}
