using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAim : MonoBehaviour
{
    [SerializeField] private float rangedAgroRange;
    [SerializeField] private LayerMask playerLayer;
    private RaycastHit2D playerRanged;
    private bool playerInRange;
    private void Update()
    {
        if (playerInRange)
        {
            Vector3 playerPosition = playerRanged.collider.GetComponent<Transform>().transform.position;
            Quaternion rotation = Quaternion.LookRotation(playerPosition - transform.position, transform.TransformDirection(Vector3.up));
            transform.rotation = new Quaternion(0,0,rotation.z,rotation.w);
            //Debug.Log(playerPosition);
            Debug.Log(rotation);
            //Debug.Log(transform.rotation);
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
}
