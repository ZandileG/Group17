using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target; // The player's transform to follow
    public Vector3 offset = new Vector3(1.0f, 1.5f, -3.0f); // Offset from the player

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the desired camera position
            Vector3 desiredPosition = target.position + offset;

            // Smoothly move the camera towards the desired position
            transform.position = Vector3.Lerp(transform.position, desiredPosition, 0.1f);
        }
    }
}
