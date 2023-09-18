using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private Gun playerGun;

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            playerGun.Fire();
        }
    }

    
}
