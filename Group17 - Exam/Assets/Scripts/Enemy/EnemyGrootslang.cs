using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyGrootslang : MonoBehaviour
{

    [SerializeField] private Transform attackPoint;
    [SerializeField] private Animator animator;
    [SerializeField] private float shotForce = 10f;
    [SerializeField] private GameObject spitPoint;
    [SerializeField] private GameObject prefabProjectile;
    [SerializeField] private float attackRange;
    [SerializeField] private int meleeAgroRange, rangedAgroRange;
    [SerializeField] private int attackCooldown;
    [SerializeField] private int attackMeleeDamage, attackRangeDamage;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private EnemyAim enemyAim;
    [SerializeField] private GameObject head;
    [SerializeField] private LayerMask cropLayer;
    [SerializeField] private AudioClip attackSound, spitSound;

    private AudioSource enemyAudio;
    private Collider2D playerRanged, playerMelee;
    private Vector3 aim;
    private bool playerInRange, playerInMelee;

    private void Start()
    {
        enemyAudio = GetComponent<AudioSource>();
        enemyAim = GetComponent<EnemyAim>();
        playerInRange = false;
        InvokeRepeating("EnemyAttack", 1.0f, 2.0f);
    }

    private void EnemyAttack()
    {
        if (playerInMelee)
        {
            animator.SetTrigger("Melee");
        } else if (playerInRange)
        {
            animator.SetTrigger("Spit");
        }
    }

    private void Update()
    {
        playerInRange = enemyAim.GetPlayerInRange();
        playerMelee = Physics2D.OverlapCircle(transform.position, meleeAgroRange, playerLayer);
        if (playerMelee != null)
            playerInMelee = true;
        else
            playerInMelee = false;
        if (!playerInMelee)
        {
            playerMelee = Physics2D.OverlapCircle(transform.position, meleeAgroRange, cropLayer);
            if (playerMelee != null)
                playerInMelee = true;
            else
                playerInMelee = false;
        }
        if (playerInRange)
        {
            Vector3 rotation = enemyAim.GetRotation();
            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

            //attackPoint.rotation = Quaternion.Euler(0, 0, rotZ);
            //head.transform.rotation = Quaternion.Euler(0, 0, rotZ);
            aim = new (rotation.x,rotation.y,0);
        }
    }

    private void FixedUpdate()
    {

    }

    public void RangedAttack()
    {
        enemyAudio.PlayOneShot(spitSound);
        Quaternion rotation = Quaternion.Euler(spitPoint.transform.eulerAngles.x, spitPoint.transform.eulerAngles.y, spitPoint.transform.eulerAngles.z);
        Vector3 position = new Vector3(spitPoint.transform.position.x, spitPoint.transform.position.y, spitPoint.transform.position.z);
        GameObject projectile = Instantiate(prefabProjectile, position, rotation);
        projectile.GetComponent<EnemyProjectile>().SetDamage(attackRangeDamage);
        projectile.GetComponent<EnemyProjectile>().Fire(new Vector2(aim.x, aim.y).normalized * shotForce);
    }

    public void MeleeAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        enemyAudio.PlayOneShot(attackSound);
        foreach (Collider2D enemy in hitEnemies)
        {
            //Debug.Log("Hit" + enemy.name);
            if (enemy != null)
                enemy.GetComponent<Player>().Damage(attackMeleeDamage);
        }

        Collider2D[] hitCrops = Physics2D.OverlapCircleAll(head.transform.position, attackRange, cropLayer);

        foreach (Collider2D enemy in hitCrops)
        {
            enemy.GetComponent<Crops>().Damage(attackMeleeDamage);
        }
    }



    private void OnDrawGizmosSelected()
    {

        Gizmos.DrawSphere(attackPoint.position, attackRange);
        Gizmos.DrawSphere(attackPoint.position, meleeAgroRange);
        Gizmos.DrawSphere(attackPoint.position, rangedAgroRange);
    }
}
