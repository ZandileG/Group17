using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] GameObject playerScreen, winScreen, pauseScreen, loseScreen;

    private bool isPaused;

    private void Start()
    {
        isPaused = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.Escape) )
        {
            if (!isPaused)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                isPaused = true;
                playerScreen.SetActive(false);
                pauseScreen.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    public void PlayerLose()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isPaused = true;
        playerScreen.SetActive(false);
        loseScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PlayerWin()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isPaused = true;
        playerScreen.SetActive(false);
        winScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isPaused = false;
        playerScreen.SetActive(true);
        pauseScreen.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
