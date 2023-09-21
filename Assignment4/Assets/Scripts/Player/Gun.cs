using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Animation gunAnimations;
    [SerializeField] AudioSource gunSource, bulletSource;
    [SerializeField] AudioClip fireSound, reloadSound, emptySound, impactSound, hitSound;
    [SerializeField] Camera playerCam;

    private string reloadAnimation = "Reload";
    private string recoilAnimation = "Recoil";

    private int reloadDelay = 3;
    private int shotDelay = 1;
    private int bulletCount = 6;
    private bool canFire = true;
    private bool isReloading = false;

    public void Fire()
    {
        if (canFire && bulletCount > 0)
        {
            if (transform.parent.parent.name == "Player")
                bulletCount--;
            gunSource.PlayOneShot(fireSound);
            //Debug.Log("Bullet Fired");
            canFire = false;
            Hit();
            gunAnimations.Play(recoilAnimation);
            StartCoroutine(FiringDelay());
        }
        else
        if (bulletCount == 0)
        {
            if (!gunSource.isPlaying)
                gunSource.PlayOneShot(emptySound);
        }

    }

    public void Reload()
    {
        if (!isReloading && bulletCount < 6)
        {
            isReloading = true;
            gunSource.PlayOneShot(reloadSound);
            gunAnimations.Play(reloadAnimation);
            StartCoroutine(ReloadDelay());
        }
    }

    IEnumerator FiringDelay()
    {
        yield return new WaitForSeconds(shotDelay);
        canFire = true;
    }

    IEnumerator ReloadDelay()
    {
        yield return new WaitForSeconds(reloadDelay);
        isReloading = false;
        bulletCount = 6;
    }

    private void Hit()
    {
        RaycastHit hitObject;

        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hitObject, 1000f))
        {
            if (hitObject.transform.name == "Enemy")
            {
                if (!bulletSource.isPlaying)
                    bulletSource.PlayOneShot(hitSound);
                Destroy(hitObject.transform.gameObject);
            }
            else
            {
                if (!bulletSource.isPlaying)
                    bulletSource.PlayOneShot(impactSound);
            }
        }
    }
}
