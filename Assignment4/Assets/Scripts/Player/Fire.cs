using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private Gun playerGun;
    private KeyCode fireButton = KeyCode.Mouse0;
    private KeyCode reloadButton = KeyCode.R;

    private void FixedUpdate()
    {
        if (Input.GetKey(fireButton))
            playerGun.Fire();


        if (Input.GetKey(reloadButton))
            playerGun.Reload();
    }

    
}
