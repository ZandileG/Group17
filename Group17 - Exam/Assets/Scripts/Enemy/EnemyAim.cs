using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAim : MonoBehaviour
{
    [SerializeField] private float rangedAgroRange;
    [SerializeField] private LayerMask playerLayer;
    private Vector2 rotation;
    private RaycastHit2D playerRanged;
    private bool playerInRange;
    private void Update()
    {
        if (playerInRange)
        {
            Vector3 playerPosition = playerRanged.collider.GetComponent<Transform>().transform.position;
            rotation = playerPosition - transform.position;

            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, rotZ);
        }
    }
    private void FixedUpdate()
    {
        playerRanged = Physics2D.CircleCast(transform.position, rangedAgroRange, Vector3.zero, 0,playerLayer);
        if (playerRanged)
            playerInRange = true;
        else
            playerInRange = false;
    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.DrawSphere(transform.position, rangedAgroRange);

    }

    public Vector2 GetRotation()
    {
        return rotation;
    }
}
