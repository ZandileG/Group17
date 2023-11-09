using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    [SerializeField] private float agroRange;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private int destroyTime = 5;
    [SerializeField] private float projectileForce;
    [SerializeField] private Vector2 projectileDir;
    private Collider2D playerPos;
    public bool playerInRange;
    private int damage = 2;

    private void Start()
    {
        //InvokeRepeating("Fire", 0f, 0.2f);
        StartCoroutine(DestroySelf());
    }

    private void Update()
    {
        playerPos = Physics2D.OverlapCircle(transform.position, agroRange, playerLayer);
        if (playerPos != null)
            playerInRange = true;
        else
            playerInRange = false;
        if (playerInRange)
        {
            Fire();
        }
        else
        {
            
        }

    }
    private void Fire()
    {
        Vector3 rotation = playerPos.transform.position - transform.position;
        //GetComponent<Rigidbody2D>().AddForce(velocity);
        rotation.Normalize();
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        Debug.Log(rotZ.ToString());
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        GetComponent<Rigidbody2D>().AddForce(rotation * projectileForce, ForceMode2D.Impulse);
        //GetComponent<Rigidbody2D>().velocity = aim * projectileForce;
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
            player.Damage(damage);
            
        }

    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.DrawSphere(transform.position, agroRange);

    }
}
