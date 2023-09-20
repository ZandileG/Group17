using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPendulum : MonoBehaviour
{
    public float speed = 1.5f;
    public float limit = 75f;
    public bool randomStart = false;
    private float random = 0;
    private float initialAngle;

    private void Awake()
    {
        if (randomStart)
            random = Random.Range(0f, 1f);
        initialAngle = transform.localRotation.eulerAngles.z;
    }

    void Update()
    {
        float angle = initialAngle + limit * Mathf.Sin(Time.time * speed + random);
        transform.localRotation = Quaternion.Euler(angle, 0, 0);
    }
}

