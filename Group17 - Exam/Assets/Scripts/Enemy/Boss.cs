using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject aim;
    [SerializeField] private float attackRange;
    [SerializeField] private int agroRange;
    [SerializeField] private int attackCooldown;
    [SerializeField] private int rangedDamage;
    [SerializeField] private float shotForce;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject normalProjectile, followProjectile;
    private Collider2D playerPos;
    private bool playerInRange;

    void Start()
    {
        InvokeRepeating("Attack",2f, 5f);        
    }


    void Update()
    {
        playerPos = Physics2D.OverlapCircle(transform.position, agroRange, playerLayer);
        if (playerPos != null)
            playerInRange = true;
        else
            playerInRange = false;
    }

    private void Attack()
    {
        /*
        int randomAttack = Random.Range(0, 50);
        switch (randomAttack)
        {
            case int n when (n >= 1 && n < 20):
                animator.SetTrigger("Dash");
                break;
            case int n when (n >= 20 && n < 30):
                animator.SetTrigger("Spin");
                break;
            case int n when (n >= 30 && n < 40):
                animator.SetTrigger("Wave");
                break;
            case int n when (n >= 40 && n < 45):
                animator.SetTrigger("Spray");
                break;
            case int n when (n >= 45 && n < 50):
                animator.SetTrigger("Burst");
                break;
            case 50:
                animator.SetTrigger("Follow");
                break;
            default:
                break;
        }
        */
        animator.SetTrigger("Homing");
    }

    public void dashAttack()
    {

    }
    public void spinAttack()
    {                   
        Quaternion rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        for (int i = -1; i <= 1; i++)
        {
            for (int n = -1; n <= 1; n++)
            {
                Vector3 position = transform.position;
                GameObject projectile = Instantiate(normalProjectile, position, rotation);
                projectile.GetComponent<EnemyProjectile>().SetDamage(rangedDamage);
                projectile.GetComponent<EnemyProjectile>().Fire(new Vector2(i, n) * shotForce);
            }
        }

    }
    public void waveAttack()
    {
        Quaternion rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        for (int i = 0; i <= 1; i++)
        {
            for (int n = 0; n <= 1; n++)
            {
                Vector3 position = transform.position;
                GameObject projectile = Instantiate(normalProjectile, position, rotation);
                projectile.GetComponent<EnemyProjectile>().SetDamage(rangedDamage);
                projectile.GetComponent<EnemyProjectile>().Fire(new Vector2(i, n) * shotForce);
            }
        }
    }
    public void sprayAttack()
    {

    }
    private void burstAttack()
    {
        Quaternion rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        for (int i = -1; i <= 1; i++)
        {
            Vector3 position = transform.position;
            GameObject projectile = Instantiate(normalProjectile, position, rotation);
            projectile.GetComponent<EnemyProjectile>().SetDamage(rangedDamage);
            projectile.GetComponent<EnemyProjectile>().Fire(new Vector2(i, 0) * shotForce);

        }
    }
    public void followAttack()
    {
        Quaternion rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        Vector3 position = transform.position;
        GameObject projectile = Instantiate(followProjectile, position, rotation);
    }


}
