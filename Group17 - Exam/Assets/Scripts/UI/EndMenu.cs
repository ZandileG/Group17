using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndMenu : MonoBehaviour
{
    private GameController gameController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }
    public void Home()
    {
        gameController.ResetGame();
        SceneManager.LoadScene("MainMenu");

    }

    // Update is called once per frame
    public void Quit()
    {
        Application.Quit();
    }
}
