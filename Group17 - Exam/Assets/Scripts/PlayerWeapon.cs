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
    [SerializeField] private float chargeTime = 0f;

    private Camera playerCam;
    private KeyCode fireKey = KeyCode.Mouse0;
    private bool canFire, isCharged;
    private Vector3 mousePos;
    private float randomSpread;
    private float count;

    private void Start()
    {
        count = 0;
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
            if (Input.GetKeyUp(fireKey) && isCharged)
            {
                isCharged = false;
                count = 0;
                Fire(weaponRotation);
                ReleaseArrow();
            }
            if (Input.GetKeyUp(fireKey) && !isCharged)
                count = 0;
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
            Fire(weaponRotation);
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
                Debug.Log(playerAim);
                Quaternion rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + randomSpread);
                Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                GameObject projectile = Instantiate(bullet, position, rotation);
                projectile.GetComponent<Projectile>().Fire(new Vector2(playerAim.x + randomSpread, playerAim.y + randomSpread).normalized * shotForce);

            }
            Debug.Log("Shot");
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

    IEnumerator ChargeUp()
    {
        yield return new WaitForSeconds(chargeTime);
        isCharged = true;
        Debug.Log("Charged");
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
