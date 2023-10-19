using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    [SerializeField] private float agroRange;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private int destroyTime = 5;
    [SerializeField] private float projectileForce;
    [SerializeField] private Vector2 projectileDir;
    private Collider2D playerPos;
    private bool playerInRange;
    private int damage = 2;

    private void Start()
    {
        InvokeRepeating("Fire", 0f, 0.2f);
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
            Vector3 rotation = playerPos.transform.position - transform.position;

            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, rotZ);
        }

    }
    private void Fire()
    {
        Vector3 rotation = playerPos.transform.position - transform.position;
        //GetComponent<Rigidbody2D>().AddForce(velocity);
        projectileDir = transform.up * rotation.y + transform.right * rotation.x;
        GetComponent<Rigidbody2D>().AddForce(projectileDir * projectileForce, ForceMode2D.Impulse);
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
            Destroy(gameObject);
        }

    }
}
