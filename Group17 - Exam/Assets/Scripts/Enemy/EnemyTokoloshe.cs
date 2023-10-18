using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTokoloshe : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject head;
    [SerializeField] private Animator animator;
    [SerializeField] private float attackRange;
    [SerializeField] private int meleeAgroRange;
    [SerializeField] private int attackCooldown;
    [SerializeField] private int attackMeleeDamage;
    [SerializeField] private LayerMask playerLayer;
    private Collider2D playerMelee;
    private bool playerInMelee;

    private void Start()
    {
        InvokeRepeating("EnemyAttack", 0f, 2f);
    }

    private void EnemyAttack()
    {
        if (playerInMelee)
        {
            animator.SetTrigger("Melee");
        }
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        playerMelee = Physics2D.OverlapCircle(transform.position, meleeAgroRange, playerLayer);
        if (playerMelee != null)
            playerInMelee = true;
        else
            playerInMelee = false;
    }

    public void MeleeAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(head.transform.position, attackRange, playerLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            //Debug.Log("Hit" + enemy.name);
            enemy.GetComponent<Player>().Damage(attackMeleeDamage);
        }
    }



    private void OnDrawGizmosSelected()
    {

        Gizmos.DrawSphere(attackPoint.position, attackRange);
        Gizmos.DrawSphere(head.transform.position, meleeAgroRange);
    }
}
