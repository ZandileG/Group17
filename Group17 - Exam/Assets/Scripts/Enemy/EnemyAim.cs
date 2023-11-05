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
    private bool playerInRange, facingRight;

    private void Start()
    {
        playerInRange = false;
        facingRight = true;
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
            if (hit == true)
                playerInRange = true;
        }
        closestPlayer = Vector2.zero;
        if (playerInRange)
        {


            foreach (RaycastHit2D hit in playerRanged)
            {
                Vector3 playerPosition = hit.collider.GetComponent<Transform>().transform.position;
                distance = playerPosition - transform.position;
                if (distance.x <= closestPlayer.x || distance.y <= closestPlayer.y)
                    closestPlayer = distance;
            }
          
            //transform.rotation = Quaternion.Euler(0, 0, rotZ);
        }
        else
        {
            closestPlayer = Vector3.zero - transform.position;
        }
        float rotZ = Mathf.Atan2(closestPlayer.y, closestPlayer.x) * Mathf.Rad2Deg;
        if (closestPlayer.x >= 0)
        {
            if (!facingRight)
            {
                Flip();
                facingRight = true;
            }
        }
        else
        {
            if (facingRight)
            {
                Flip();
                facingRight = false;
            }
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

    private void Flip()
    {
        Vector3 currentScale = this.transform.localScale;
        currentScale.x *= -1;
        this.transform.localScale = currentScale;
    }
}
