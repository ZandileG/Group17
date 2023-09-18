using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    private Vector3 barrelPos;
    private Quaternion barrelRot;
    private float shotForce = 5000f;
    private int shotDelay = 1;
    private bool canFire = true;

    public void Fire()
    {
        if (canFire)
        {
            barrelRot = transform.rotation;
            //barrelRot.eulerAngles = new Vector3(barrelRot.eulerAngles.x -10, barrelRot.eulerAngles.y, barrelRot.eulerAngles.z);
            barrelPos = transform.position;
            barrelPos.y += 0.15f;
            Debug.Log("Bullet Fired");
            canFire = false;
            GameObject bullet = Instantiate(bulletPrefab, barrelPos, barrelRot);
            bullet.GetComponent<Bullet>().Fire(new Vector3(0, 0, shotForce));
            StartCoroutine(FiringDelay());
        }

    }

    IEnumerator FiringDelay()
    {
        yield return new WaitForSeconds(shotDelay);
        canFire = true;
    }
}
