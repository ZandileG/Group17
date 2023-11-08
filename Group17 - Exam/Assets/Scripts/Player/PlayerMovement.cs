using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Transform orientation;
    [SerializeField] private float playerWalkSpeed = 5f;
    [SerializeField] private float playerSprintSpeed = 15f;
    [SerializeField] private int playerMaxStamina = 1000;
    [SerializeField] private float playerRollInvilFrames = 0.5f;
    [SerializeField] private float playerRollSpeed = 10f;
    [SerializeField] Slider staminaBar;
    [SerializeField] private AudioClip walkSound;

    private AudioSource playerAudio;
    private int playerStamina;
    private Rigidbody2D playerRB;
    private KeyCode rollKey = KeyCode.Space, sprintKey = KeyCode.LeftShift;

    Vector2 moveDirection;
    private bool isSprinting, isRolling;
    private float vertInput, horizInput;

    private void Start()
    {
        player = GetComponent<Player>();
        playerRB = GetComponent<Rigidbody2D>();
        orientation = GetComponent<Transform>();
        playerAudio = GetComponent<AudioSource>();
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

        if (horizInput > 0 || vertInput > 0)
            if (!playerAudio.isPlaying)
            playerAudio.PlayOneShot(walkSound);

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
        playerRB.AddForce(moveDirection.normalized * playerRollSpeed * 20f, ForceMode2D.Impulse);
        playerStamina -= 10;
        StartCoroutine(RollDelay());
    }

    public bool GetIsRolling()
    {
        return isRolling;
    }

    IEnumerator SprintDelay()
    {
        yield return new WaitForSeconds(0.5f);
        isSprinting = true;    
    }

    IEnumerator RollDelay()
    {
        player.SetInvil(true);
        StartCoroutine(RollInvil());
        yield return new WaitForSeconds(1);

        isRolling = false;
    }

    IEnumerator RollInvil()
    {
        yield return new WaitForSeconds(playerRollInvilFrames);
        player.SetInvil(false);
    }

}
