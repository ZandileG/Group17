using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerMelee playerMelee;
    [SerializeField] private PlayerWeapon playerWeapon;
    [SerializeField] private PlayerAim playerAim;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject rotatePoint, reloadIndicator;
    [SerializeField] private Text ammoDisplay;
    private GameObject equipedRangedWeapon;


    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();       
    }

    public GameObject GetUI()
    {
        return levelManager.GetDefeatScreen();
    }

    public Text GetAmmoDisplay()
    {
        return ammoDisplay;
    }

    public GameObject GetReloadIndicator()
    {
        return reloadIndicator;
    }
    public void SetWeapon(GameObject newWeapon)
    {
        equipedRangedWeapon = Instantiate(newWeapon, gameObject.transform.position, gameObject.transform.rotation);
        playerWeapon = equipedRangedWeapon.GetComponent<PlayerWeapon>();
        equipedRangedWeapon.transform.parent = rotatePoint.transform;
        playerAim.SetWeapon(equipedRangedWeapon);
    }

    public void SetMelee(PlayerMelee newMelee)
    {
        playerMelee = newMelee;
    }

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
