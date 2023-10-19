using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public float timeToDisappear = 1.0f;   // Time in seconds before the object disappears.
    public float timeToReappear = 3.0f;    // Time in seconds before the object reappears.

    private Collider2D collider;
    public GameObject gate;
    //private Renderer renderer;
    private bool isPlayerInside = false;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
        //renderer = GetComponent < Renderer();
    }

    private void Update()
    {
        if (isPlayerInside)
        {
            // Start the timer for disappearance.
            timeToDisappear -= Time.deltaTime;

            if (timeToDisappear <= 0f)
            {
                // Object disappears.
                collider.enabled = false;
                gate.SetActive(false);
                //renderer.enabled = false;

                // Start the timer for reappearance.
                timeToReappear -= Time.deltaTime;

                if (timeToReappear <= 0f)
                {
                    // Object reappears.
                    collider.enabled = true;
                    gate.SetActive(true);
                    //renderer.enabled = true;

                    // Reset timers and state.
                    timeToDisappear = 1.0f;
                    timeToReappear = 3.0f;
                    isPlayerInside = false;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // The player has entered the object's trigger zone.
            isPlayerInside = true;
        }
    }
}

