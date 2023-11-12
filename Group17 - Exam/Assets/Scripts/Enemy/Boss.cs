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
    [SerializeField] private float dashForce;
    private Rigidbody2D bossRB;
    private Vector3 playerPos;
    private int count;
    private Collider2D playerColl;
    private bool playerInRange, facingRight;

    void Start()
    {
        count = 1;
        facingRight = true;
        bossRB = GetComponent<Rigidbody2D>();
        InvokeRepeating("Attack", 1f, 1.5f);
    }


    void Update()
    {
        playerColl = Physics2D.OverlapCircle(transform.position, agroRange, playerLayer);
        if (playerColl != null)
            playerInRange = true;
        else
            playerInRange = false;
        if (playerInRange)
        {
            Vector3 playerPosition = playerColl.GetComponent<Transform>().transform.position;
            playerPos = playerPosition - transform.position;
        }
    }

    private void Attack()
    {

        int randomAttack = Random.Range(0, 50);
        switch (randomAttack)
        {
            case int n when (n >= 0 && n < 20):
                animator.SetTrigger("Dash");
                break;
            case int n when (n >= 20 && n < 30):
                animator.SetTrigger("Spin");
                break;
            case int n when (n >= 30 && n < 35):
                animator.SetTrigger("Wave");
                break;
            case int n when (n >= 35 && n < 40):
                animator.SetTrigger("Spray");
                break;
            case int n when (n >= 40 && n < 45):
                animator.SetTrigger("Burst");
                break;
            case int n when (n >= 45 && n <= 50):
                animator.SetTrigger("Homing");
                break;
            default:
                break;
        }

    }
    //Animation: Dash
    public void dashAttack()
    {
        if (playerInRange)
        {
            float rotZ = Mathf.Atan2(playerPos.y, playerPos.x) * Mathf.Rad2Deg;
            Vector3 position = transform.position;
            Quaternion projRotation = Quaternion.Euler(0, 0, rotZ);
            bossRB.AddForce(playerPos * dashForce, ForceMode2D.Force);
        }
    }
    //Animation: Spin
    public void spinAttack()
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int n = -1; n <= 1; n++)
            {
                if (!(n == 0 && i == 0))
                {
                    Vector3 position = transform.position;
                    int desiredAngle = 0;
                    switch (i)
                    {
                        case -1:
                            desiredAngle = 180 - (n * 45);
                            break;
                        case 0:
                            desiredAngle = 90 * n;
                            break;
                        case 1:
                            desiredAngle = (n * 45);
                            break;
                        default:
                            break;
                    }
                    Quaternion projRotation = Quaternion.Euler(0, 0, desiredAngle);
                    GameObject projectile = Instantiate(normalProjectile, position, projRotation);
                    projectile.GetComponent<EnemyProjectile>().SetDamage(rangedDamage);
                    projectile.GetComponent<EnemyProjectile>().Fire(new Vector2(i, n) * shotForce);
                }
            }
        }

    }
    //Animation: Wave
    public void waveAttack()
    {
        int n, i;
        switch (count)
        {
            case 1:
                count++;
                n = 0;
                i = 1;
                break;
            case 2:
                count++;
                n = 1;
                i = 1;
                break;
            case 3:
                count++;
                n = 1;
                i = 0;
                break;
            case 4:
                count++;
                n = 1;
                i = -1;
                break;
            case 5:
                count++;
                n = 0;
                i = -1;
                break;
            case 6:
                count++;
                n = -1;
                i = -1;
                break;
            case 7:
                count++;
                n = -1;
                i = 0;
                break;
            case 8:
                count = 1;
                n = -1;
                i = 1;
                break;
            default:
                n = 0;
                i = 0;
                break;
        }
        Vector3 position = transform.position;
        int desiredAngle = 0;
        switch (i)
        {
            case -1:
                desiredAngle = 180 - (n * 45);
                break;
            case 0:
                desiredAngle = 90 * n;
                break;
            case 1:
                desiredAngle = (n * 45);
                break;
            default:
                break;
        }
        Quaternion projRotation = Quaternion.Euler(0, 0, desiredAngle);
        GameObject projectile = Instantiate(normalProjectile, position, projRotation);
        projectile.GetComponent<EnemyProjectile>().SetDamage(rangedDamage);
        projectile.GetComponent<EnemyProjectile>().Fire(new Vector2(i, n) * shotForce);
    }
    //Animation: Spray
    public void sprayAttack()
    {
        if (playerInRange)
        {
            for (int n = -1; n <= 1; n++)
            {
                float rotZ = Mathf.Atan2(playerPos.y, playerPos.x) * Mathf.Rad2Deg;
                Vector3 position = transform.position;
                Quaternion projRotation = Quaternion.Euler(0, 0, rotZ);
                GameObject projectile = Instantiate(normalProjectile, position, projRotation);
                projectile.GetComponent<EnemyProjectile>().SetDamage(rangedDamage);
                projectile.GetComponent<EnemyProjectile>().Fire(new Vector2(playerPos.x + n, playerPos.y + n).normalized * shotForce);
            }
        }
    }
    //Animation: Burst
    public void burstAttack()
    {
        if (playerInRange)
        {
            float rotZ = Mathf.Atan2(playerPos.y, playerPos.x) * Mathf.Rad2Deg;
            Vector3 position = transform.position;
            Quaternion projRotation = Quaternion.Euler(0, 0, rotZ);
            GameObject projectile = Instantiate(normalProjectile, position, projRotation);
            projectile.GetComponent<EnemyProjectile>().SetDamage(rangedDamage);
            projectile.GetComponent<EnemyProjectile>().Fire(new Vector2(playerPos.x, playerPos.y).normalized * shotForce);
        }
    }
    //Animation: Homing
    public void followAttack()
    {
        Quaternion rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        Vector3 position = transform.position;
        GameObject projectile = Instantiate(followProjectile, position, rotation);
    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.DrawSphere(attackPoint.position, agroRange);
    }
}
