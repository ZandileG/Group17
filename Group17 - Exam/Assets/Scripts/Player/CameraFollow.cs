using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the player character.

    private void LateUpdate()
    {
        if (target != null)
        {
            // Set the camera's position to follow the player (X and Y only, Z remains the same).
            Vector3 newPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = newPosition;
        }
    }
}

