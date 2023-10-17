using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] PlayerMelee playerMelee;
    [SerializeField] PlayerWeapon playerWeapon;
    [SerializeField] PlayerAim playerAim;
    [SerializeField] PlayerMovement playerMovement;


    public bool GetIsRolling()
    {
        return playerMovement.GetIsRolling();
    }

    public bool GetIsMeleeing()
    {
        return playerMelee.GetIsMeleeing();
    }

    public bool GetIsShooting()
    {
        return playerWeapon.GetIsShooting();
    }
}
