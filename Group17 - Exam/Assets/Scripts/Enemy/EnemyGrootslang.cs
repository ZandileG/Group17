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
    private Collider2D playerRanged, playerMelee;
    private Vector3 aim;
    private bool playerInRange, playerInMelee;

    private void Start()
    {
        playerInRange = false;
        InvokeRepeating("EnemyAttack", 10f, 5f);
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
        
        if (playerInRange)
        {
            Vector3 playerPosition = playerRanged.GetComponent<Transform>().transform.position;
            Quaternion rotation = Quaternion.LookRotation(playerPosition - transform.position, transform.TransformDirection(Vector3.up));
            aim = new (rotation.x,rotation.y, rotation.z);
            attackPoint.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        }
    }

    private void FixedUpdate()
    {
        playerRanged = Physics2D.OverlapCircle(transform.position, rangedAgroRange, playerLayer);
        if (playerRanged != null)
            playerInRange = true;
        else
            playerInRange = false;
        playerMelee = Physics2D.OverlapCircle(transform.position, meleeAgroRange, playerLayer);
        if (playerMelee != null)
            playerInMelee = true;
        else
            playerInMelee = false;
    }

    public void RangedAttack()
    {
        Quaternion rotation = Quaternion.Euler(spitPoint.transform.eulerAngles.x, spitPoint.transform.eulerAngles.y, spitPoint.transform.eulerAngles.z);
        Vector3 position = new Vector3(spitPoint.transform.position.x, spitPoint.transform.position.y, spitPoint.transform.position.z);
        GameObject projectile = Instantiate(prefabProjectile, position, rotation);
        projectile.GetComponent<EnemyProjectile>().SetDamage(attackRangeDamage);
        projectile.GetComponent<EnemyProjectile>().Fire(new Vector2(aim.x, aim.y).normalized * shotForce);
    }

    public void MeleeAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            //Debug.Log("Hit" + enemy.name);
            enemy.GetComponent<Player>().Damage(attackMeleeDamage);
        }
    }



    private void OnDrawGizmosSelected()
    {

        Gizmos.DrawSphere(attackPoint.position, attackRange);
        Gizmos.DrawSphere(attackPoint.position, meleeAgroRange);
        Gizmos.DrawSphere(attackPoint.position, rangedAgroRange);
    }
}
