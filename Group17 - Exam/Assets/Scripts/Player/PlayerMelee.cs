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
    [SerializeField] private AudioClip hitSound;

    private AudioSource weaponAudio;
    private bool isAttacking;
    private bool canMelee;
    private bool hasHit;
    private KeyCode meleeKey = KeyCode.Mouse1;

    private void Start()
    {
        weaponAudio = GetComponent<AudioSource>();
        manager = FindObjectOfType<PlayerManager>();
        canMelee = true;
        isAttacking = false;
        hasHit = false;
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
            hasHit = true;
            animator.SetTrigger("Melee");

            StartCoroutine(MeleeDelay());
        }
    }

    public void Damage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleePoint.position, meleeRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            //Debug.Log("Hit" + enemy.name);
            if (hasHit)
            {
                if (!weaponAudio.isPlaying)
                    weaponAudio.PlayOneShot(hitSound);
                if (enemy.GetComponent<Enemy>() != null)
                {
                    //Debug.Log("Hit");
                    enemy.GetComponent<Enemy>().Damage(meleeDamage);
                }
                else if (enemy.GetComponentInParent<Enemy>() != null)
                {
                    enemy.GetComponentInParent<Enemy>().Damage(meleeDamage);
                }
                hasHit = false;
            }
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
