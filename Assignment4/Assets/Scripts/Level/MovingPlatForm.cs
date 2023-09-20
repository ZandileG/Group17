using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatForm : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float moveSpeed = 2.0f;

    private Vector3 currentTarget;

    private void Start()
    {
        currentTarget = endPoint.position;
    }

    private void Update()
    {
        // Move the platform towards the current target
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, moveSpeed * Time.deltaTime);

        // If the platform reaches the current target, switch to the other target
        if (transform.position == currentTarget)
        {
            currentTarget = (currentTarget == startPoint.position) ? endPoint.position : startPoint.position;
        }
    }
}

