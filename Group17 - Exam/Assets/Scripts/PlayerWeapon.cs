using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject aim;
    [SerializeField] private GameObject loadedProjectile;
    [SerializeField] private int damage = 2;
    [SerializeField] private float shotDelay = 0.7f;
    [SerializeField] private float shotForce = 50f;
    [SerializeField] private int fireCount = 2;
    [SerializeField] private float fireSpread = 10;
    [SerializeField] private bool hasChargeTime = false;
    [SerializeField] private float chargeRate = 0f;

    private Camera playerCam;
    private KeyCode fireKey = KeyCode.Mouse0;
    private bool canFire, isCharged;
    private Vector3 mousePos;
    private float randomSpread;
    private float currentCharge;

    private void Start()
    {
        canFire = true;   
        isCharged = false;
        playerCam = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        mousePos = playerCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 weaponRotation = mousePos - transform.position;
        if (hasChargeTime)
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
        else
            if (Input.GetKey(fireKey))
        {
            currentCharge = 1f;
            Fire(weaponRotation);

        }
    }

    private void FixedUpdate()
    {

    }

    public void Fire(Vector3 playerAim)
    {
        if (canFire)
        {
            //fireSound.Play();
            canFire = false;
            for (int i = 1; i <= fireCount; i++)
            {
                randomSpread = Random.Range(-1 * fireSpread, fireSpread);
                //Debug.Log(playerAim);
                Quaternion rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + randomSpread);
                Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                GameObject projectile = Instantiate(bullet, position, rotation);
                projectile.GetComponent<Projectile>().SetDamage(damage);
                projectile.GetComponent<Projectile>().Fire(new Vector2(playerAim.x + randomSpread, playerAim.y + randomSpread).normalized * shotForce * currentCharge);
                currentCharge = 0;

            }
            //Debug.Log("Shot");
            //gameController.FireShot();
            StartCoroutine(FiringDelay());
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

    

    IEnumerator FiringDelay()
    {
        yield return new WaitForSeconds(shotDelay);
        canFire = true;
    }

    public float GetForce()
    {
        return shotForce;
    }
}
