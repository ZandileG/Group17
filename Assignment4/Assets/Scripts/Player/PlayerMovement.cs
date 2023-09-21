using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform orientation;
    [SerializeField] LayerMask ground;

    private KeyCode jumpKey = KeyCode.Space;
    private KeyCode sprintkey = KeyCode.LeftShift;


    private float moveSpeed = 100f;
    private float sprintSpeed = 250f;
    private float airVelocityMultiplier = 1.1f;
    private bool isSprinting;

    private float jumpForce = 350f;
    private float jumpCooldown = 0.5f;
    private int jumpCount = 2;
    private bool canJump;

    
    private float playerHeight = 20f;
    private bool isGrounded;
    private float groundDrag = 1.5f;

    private Vector3 moveDirection;
    private Rigidbody rb;

    private float horizInput;
    private float vertInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        canJump = true;
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, ground);

        Inputs();
        SpeedControl();

        if (isGrounded)
        {
            rb.drag = groundDrag;
            jumpCount = 2;
        }
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Inputs()
    {
        horizInput = Input.GetAxisRaw("Horizontal");
        vertInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(sprintkey))
            isSprinting = true;
        else
            isSprinting = false;

        if (canJump && jumpCount > 0 && Input.GetKey(jumpKey))
        {
            jumpCount--;
            canJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void Move()
    {

        moveDirection = new Vector3(orientation.forward.x,0,orientation.forward.z) * vertInput + orientation.right * horizInput;
        if (isGrounded)
            if (isSprinting)
                rb.AddForce(moveDirection.normalized * sprintSpeed * 10f, ForceMode.Force);
            else
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else if (!isGrounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airVelocityMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (!isGrounded)
        {
            if (flatVel.magnitude > sprintSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
        else
        {
            if (isSprinting)
            {
                if (flatVel.magnitude > sprintSpeed)
                {
                    Vector3 limitedVel = flatVel.normalized * sprintSpeed;
                    rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
                }
            }
            else
            {
                if (flatVel.magnitude > moveSpeed)
                {
                    Vector3 limitedVel = flatVel.normalized * moveSpeed;
                    rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
                }
            }
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(new Vector3(0,1f,0) * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        canJump = true;
    }
}
