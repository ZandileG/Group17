using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform orientation;
    [SerializeField] private float playerWalkSpeed = 5f;
    [SerializeField] private float playerSprintSpeed = 15f;
    [SerializeField] private int playerMaxStamina = 1000;
    [SerializeField] private float playerRollInvilFrames = 0.5f;
    [SerializeField] Slider staminaBar;
    [SerializeField] private BoxCollider2D playerHitbox;

    private int playerStamina;
    private Rigidbody2D playerRB;
    private KeyCode rollKey = KeyCode.Space, sprintKey = KeyCode.LeftShift;

    Vector2 moveDirection;
    private bool isSprinting, isRolling;
    private float vertInput, horizInput;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        orientation = GetComponent<Transform>();
        playerStamina = playerMaxStamina;
        staminaBar.maxValue = playerMaxStamina;
        staminaBar.value = playerMaxStamina;
        isSprinting = false;
        isRolling = false;
    }

    private void Update()
    {
        Inputs();
        staminaBar.value = playerStamina;
    }

    private void FixedUpdate()
    {
        if (playerStamina < playerMaxStamina && !isSprinting && !isRolling)
            playerStamina += 5;
        if (isSprinting && (playerRB.velocity.x > 0 || playerRB.velocity.y > 0 || playerRB.velocity.x < 0 || playerRB.velocity.y < 0))
        {
            if (playerStamina > 0)
                playerStamina--;
        }
        Move();
    }
    
    private void Inputs()
    {
        horizInput = Input.GetAxisRaw("Horizontal");
        vertInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(sprintKey) && playerStamina > 5)
        {
            StartCoroutine(SprintDelay());
        }
        else
        {
            isSprinting = false;
        }

        if (Input.GetKeyDown(rollKey) && !isRolling && playerStamina > 10 && (playerRB.velocity.x > 0 || playerRB.velocity.y > 0 || playerRB.velocity.x < 0 || playerRB.velocity.y < 0))
        {
            Roll();
        }

    }

    private void Move()
    {
        moveDirection = orientation.up * vertInput + orientation.right * horizInput;
        if (!isRolling)
            if (isSprinting)
                playerRB.AddForce(moveDirection.normalized * playerSprintSpeed * 10f, ForceMode2D.Force);
            else
                playerRB.AddForce(moveDirection.normalized * playerWalkSpeed * 10f, ForceMode2D.Force);
    }

    private void Roll()
    {
        isRolling = true; 
        moveDirection = orientation.up * vertInput + orientation.right * horizInput;
        playerRB.AddForce(moveDirection.normalized * playerWalkSpeed * 20f, ForceMode2D.Impulse);
        playerStamina -= 10;
        StartCoroutine(RollDelay());
    }

    IEnumerator SprintDelay()
    {
        yield return new WaitForSeconds(0.5f);
        isSprinting = true;    
    }

    IEnumerator RollDelay()
    {
        if (playerHitbox.enabled)
            playerHitbox.enabled = false;
        StartCoroutine(RollInvil());
        yield return new WaitForSeconds(1);

        isRolling = false;
    }

    IEnumerator RollInvil()
    {
        yield return new WaitForSeconds(playerRollInvilFrames);
        playerHitbox.enabled = true;
    }

}
