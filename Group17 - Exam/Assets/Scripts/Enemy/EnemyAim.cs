using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAim : MonoBehaviour
{
    [SerializeField] private float rangedAgroRange;
    [SerializeField] private LayerMask playerLayer, cropLayer;
    [SerializeField] private bool focusCrops;
    private Vector3 nullLocation;
    private Vector2 closest;
    private Vector3[] playersInRange, cropsInRange;
    private Vector2 distance;
    private RaycastHit2D[] playerRanged, cropRanged;
    private bool playerInRange, facingRight, cropInRange;
    
    private void Start()
    {
        playerInRange = false;
        cropInRange = false;
        facingRight = true;
        nullLocation = FindObjectOfType<LevelManager>().GetNullLocation();
    }


    private void Update()
    {
        playerInRange = false;
        cropInRange = false;
        playerRanged = Physics2D.CircleCastAll(transform.position, rangedAgroRange, Vector3.zero, 0,playerLayer);
        cropRanged = Physics2D.CircleCastAll(transform.position, rangedAgroRange, Vector3.zero, 0, cropLayer);
        foreach (RaycastHit2D hit in playerRanged)
        {
            if (hit == true)
                playerInRange = true;
        }
        foreach (RaycastHit2D hit in cropRanged)
        {
            if (hit == true)
                cropInRange = true;
        }
        closest = Vector2.zero;
        if (focusCrops)
        {
            if (cropInRange)
            {
                FindCrops();
            } else if (playerInRange)
            {
                FindPlayer();
            }else
            {
                closest = nullLocation - transform.position;
            }
        } else
        {
            if (playerInRange)
            {
                FindPlayer();
            }
            else if (cropInRange)
            {
                FindCrops();
            }
            else
            {
                closest = nullLocation - transform.position;
            }
        }
        closest.Normalize();
        float rotZ = Mathf.Atan2(closest.y, closest.x) * Mathf.Rad2Deg;
        if (closest.x >= 0)
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

    private void FindPlayer()
    {
        foreach (RaycastHit2D hit in playerRanged)
        {
            Vector3 playerPosition = hit.collider.GetComponent<Transform>().transform.position;
            distance = playerPosition - transform.position;
            if (distance.x <= closest.x || distance.y <= closest.y)
                closest = distance;
        }
    }

    private void FindCrops()
    {
        foreach (RaycastHit2D hit in cropRanged)
        {
            Vector3 cropPosition = hit.collider.GetComponent<Transform>().transform.position;
            distance = cropPosition - transform.position;
            if (distance.x <= closest.x || distance.y <= closest.y)
                closest = distance;
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
        return closest;
    }

    private void Flip()
    {
        Vector3 currentScale = this.transform.localScale;
        currentScale.x *= -1;
        this.transform.localScale = currentScale;
    }
}
