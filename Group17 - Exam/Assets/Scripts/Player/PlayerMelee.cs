using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    [SerializeField] private PlayerManager manager;
    [SerializeField] private Animator animator;
    [SerializeField] private int meleeDamage = 1;
    [SerializeField] private float meleeRange = 0.5f;
    [SerializeField] private Transform meleePoint;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float meleeCooldown = 1f;
    private bool isAttacking;
    private bool canMelee;
    private KeyCode meleeKey = KeyCode.Mouse1;

    private void Start()
    {
        canMelee = true;
        isAttacking = false;
    }

    private void Update()
    {

        if (Input.GetKey(meleeKey))
        {
            Melee();
        }
    }

    private void Melee()
    {
        if (canMelee && !manager.GetIsShooting())
        {
            canMelee = false;
            isAttacking = true;
            animator.SetTrigger("Melee");

            //StartCoroutine(StartUp());
            //StartCoroutine(StopMelee());
            StartCoroutine(MeleeDelay());
        }
    }

    public void Damage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleePoint.position, meleeRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit" + enemy.name);
            enemy.GetComponent<Enemy>().Damage(meleeDamage);
        }
    }


    IEnumerator MeleeDelay()
    {
        yield return new WaitForSeconds(meleeCooldown);
        canMelee = true;

    }

    public void ResetMelee()
    {
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.DrawSphere(meleePoint.position, meleeRange);        
    }

    public bool GetIsMeleeing()
    {
        return isAttacking;
    }


}
