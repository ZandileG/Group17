using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private GameObject playerWeapon;
    [SerializeField] private GameObject player;
    [SerializeField] private bool hasWeapon = false;
    private bool gunFacingRight;

    private Camera playerCam;
    private Vector3 mousePos, cameraPos;
    // Start is called before the first frame update
    void Start()
    {
        playerCam = FindObjectOfType<Camera>();     
        gunFacingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasWeapon)
        {
            mousePos = playerCam.ScreenToWorldPoint(Input.mousePosition);

            Vector3 rotation = mousePos - transform.position;

            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, rotZ);
            cameraPos = new Vector3(player.transform.position.x, player.transform.position.y, -10);
            playerCam.transform.position = cameraPos;

            if (((transform.rotation.eulerAngles.z < 90 && transform.rotation.eulerAngles.z >= 0) || (transform.rotation.eulerAngles.z > 270 && transform.rotation.eulerAngles.z <= 360)) && !gunFacingRight)
            {
                Flip();
                gunFacingRight = true;
            }
            else if (transform.rotation.eulerAngles.z < 270 && transform.rotation.eulerAngles.z > 90 && gunFacingRight)
            {
                Flip();
                gunFacingRight = false;
            }
        }
        
    }

    public void SetWeapon(GameObject newWeapon)
    {
        playerWeapon = newWeapon;
        hasWeapon = true;
    }
    private void Flip()
    {
        Vector3 currentScale = playerWeapon.transform.localScale;
        currentScale.y *= -1;
        playerWeapon.transform.localScale = currentScale;
    }

}
