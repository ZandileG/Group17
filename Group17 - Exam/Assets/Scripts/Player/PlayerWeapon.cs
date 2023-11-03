using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private PlayerManager manager;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject barrel;
    [SerializeField] private GameObject loadedProjectile;
    [SerializeField] private GameObject reloadIndicator;
    [SerializeField] private Text ammoDisplay;
    [SerializeField] private int damage = 2;
    [SerializeField] private float shotDelay = 0.7f;
    [SerializeField] private float shotForce = 50f;
    [SerializeField] private int fireCount = 2;
    [SerializeField] private float fireSpread = 10;
    [SerializeField] private bool hasChargeTime = false;
    [SerializeField] private float chargeRate = 0f;
    [SerializeField] private float reloadTime = 1f;
    [SerializeField] private int ammoCount = 1;
    [SerializeField] private string weaponName;

    private Camera playerCam;
    private const KeyCode fireKey = KeyCode.Mouse0;
    private const KeyCode reloadKey = KeyCode.R;
    private bool canFire, isCharged;
    private Vector3 mousePos;
    private float randomSpread;
    private float currentCharge;
    private int currentAmmo;

    private void Awake()
    {
        canFire = true;   
        isCharged = false;
        manager = FindObjectOfType<PlayerManager>();
        reloadIndicator = manager.GetReloadIndicator();
        ammoDisplay = manager.GetAmmoDisplay();
        playerCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        reloadIndicator.SetActive(false);
        currentAmmo = ammoCount;
        UpdateAmmo();
    }

    private void Update()
    {
        mousePos = playerCam.ScreenToWorldPoint(Input.mousePosition);

        if (canFire && Input.GetKey(reloadKey) && !isCharged)
        {
            ammoDisplay.text = "0";
            canFire = false;
            reloadIndicator.SetActive(true);
            StartCoroutine(Reload());
        }

        Vector3 weaponRotation = mousePos - transform.position;
        if (hasChargeTime && canFire && !manager.GetIsMeleeing() && !manager.GetIsRolling())
        {
            if (Input.GetKey(fireKey) && isCharged)
            {
                if (currentCharge <= 1)
                    currentCharge += chargeRate;
            }
            if (Input.GetKeyUp(fireKey) && isCharged)
            {
                isCharged = false;
                Fire(weaponRotation);
                ReleaseArrow();
            }
            if (Input.GetKeyDown(fireKey) && !isCharged)
            {
                LoadArrow();
                //loadedProjectile.transform.position = Vector3.zero;

                isCharged = true;
            }
            //StartCoroutine(ChargeUp());


        }
        if (Input.GetKey(fireKey) && !hasChargeTime && !manager.GetIsMeleeing() && !manager.GetIsRolling())
        {
            currentCharge = 1f;
            Fire(weaponRotation);

        }
    }


    public void Fire(Vector3 playerAim)
    {
        if (canFire && (currentAmmo > 0 || hasChargeTime))
        {
            if (!hasChargeTime)
            {
                currentAmmo--;
                UpdateAmmo();
            }
            else
            {
                reloadIndicator.SetActive(true);
                ammoDisplay.text = "0";
            }
            //fireSound.Play();
            canFire = false;
            for (int i = 1; i <= fireCount; i++)
            {
                randomSpread = Random.Range(-1 * fireSpread, fireSpread);
                //Debug.Log(playerAim);
                Quaternion rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + randomSpread);
                Vector3 position =  barrel.transform.position;
                GameObject projectile = Instantiate(bullet, position, rotation);
                projectile.GetComponent<Projectile>().SetDamage(damage);
                projectile.GetComponent<Projectile>().Fire(new Vector2(playerAim.x + randomSpread, playerAim.y + randomSpread).normalized * (shotForce * currentCharge));
            }
            currentCharge = 0;
            //Debug.Log("Shot");
            //gameController.FireShot();
            StartCoroutine(FiringDelay());
        } else if (currentAmmo <= 0)
        {
            reloadIndicator.SetActive(true);
            StartCoroutine(Reload());
        }
    }

    public void LoadArrow()
    {
        loadedProjectile.SetActive(true);
    }

    public void ReleaseArrow()
    {
        loadedProjectile.SetActive(false);
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        canFire = true;
        currentAmmo = ammoCount;
        reloadIndicator.SetActive(false);
        UpdateAmmo();
    }

    IEnumerator FiringDelay()
    {
        yield return new WaitForSeconds(shotDelay);
        canFire = true;
        if (hasChargeTime)
        {
            reloadIndicator.SetActive(false);
            UpdateAmmo();
        }
    }

    public bool GetIsShooting()
    {
        return !canFire;
    }
    public float GetForce()
    {
        return shotForce;
    }

    public string GetName()
    {
        return weaponName;
    }

    private void UpdateAmmo()
    {
        ammoDisplay.text = currentAmmo.ToString();
    }
}
