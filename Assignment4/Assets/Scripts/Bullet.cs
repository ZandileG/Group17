using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int destroyTime = 3;

    void Start()
    {
        StartCoroutine(DestroySelf());
    }

    public void Fire(Vector3 velocity)
    {
        GetComponent<Rigidbody>().AddRelativeForce(velocity);
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Enemy")
        {

        }
        else
        {

        }
    }
}
