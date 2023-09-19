using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private Transform orientation;

    private float vertSens = 150f;
    private float horizSens = 150f;

    private float vertRotation;
    private float horizRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        float horizMouse = Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * horizSens;
        float vertMouse = Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * vertSens;

        vertRotation += horizMouse;
        horizRotation -= vertMouse;
        horizRotation = Mathf.Clamp(horizRotation, -80f, 80f);

        transform.rotation = Quaternion.Euler(horizRotation, vertRotation, 0);
        orientation.rotation = Quaternion.Euler(horizRotation, vertRotation, 0);
    }
}
