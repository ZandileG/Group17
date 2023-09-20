using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapseFLoor : MonoBehaviour
{
    public GameObject collapsingFloor; // Reference to the collapsing floor GameObject
    public GameObject collapsingFloor2;
    public float collapseDelay = 1.0f; // Time delay before the floor collapses

    private bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has crossed the trigger line
        if (other.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            StartCoroutine(CollapsingFloor());
        }
    }

    private IEnumerator CollapsingFloor()
    {
        yield return new WaitForSeconds(collapseDelay);

        // Disable the Collider on the collapsing floor
        collapsingFloor.GetComponent<Collider>().enabled = false;
        collapsingFloor2.GetComponent<Collider>().enabled = false;

        // Enable Rigidbody physics for the collapsing floor
        Rigidbody rb = collapsingFloor.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddForce(Vector3.down * 500.0f); // Apply force to make it collapse

        Rigidbody rb2 = collapsingFloor2.GetComponent<Rigidbody>();
        rb2.isKinematic = false;
        rb2.AddForce(Vector3.down * 500.0f);

        // You can also play a collapsing animation or sound here

        // Destroy the collapsing floor after a delay if needed
        // Destroy(collapsingFloor, 5.0f);
    }
}

