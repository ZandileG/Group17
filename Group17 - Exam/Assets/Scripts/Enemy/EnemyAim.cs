using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAim : MonoBehaviour
{
    [SerializeField] private float rangedAgroRange;
    [SerializeField] private LayerMask playerLayer;
    private Vector2 closestPlayer;
    private Vector3[] playersInRange;
    private Vector2 distance;
    private RaycastHit2D[] playerRanged;
    private bool playerInRange;

    private void Start()
    {
        playerInRange = false;
    }

    private void Update()
    {

    }
    private void FixedUpdate()
    {
        playerInRange = false;
        playerRanged = Physics2D.CircleCastAll(transform.position, rangedAgroRange, Vector3.zero, 0,playerLayer);
        foreach (RaycastHit2D hit in playerRanged)
        {
            playerInRange = true;
            break;
        }

        if (playerInRange)
        {
            closestPlayer = Vector2.zero;
            foreach (RaycastHit2D hit in playerRanged)
            {
                Vector3 playerPosition = hit.collider.GetComponent<Transform>().transform.position;
                distance = playerPosition - transform.position;
                if (distance.x <= closestPlayer.x || distance.y <= closestPlayer.y)
                    closestPlayer = distance;
            }



            float rotZ = Mathf.Atan2(closestPlayer.y, closestPlayer.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, rotZ);
        }
    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.DrawSphere(transform.position, rangedAgroRange);

    }

    public bool GetPlayerInRange()
    {
        return playerInRange;
    }

    public Vector2 GetRotation()
    {
        return closestPlayer;
    }
}
