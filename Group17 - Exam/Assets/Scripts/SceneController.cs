using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameController gameController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();    
    }

    public void NextLevel()
    {
        gameController.NextLevel();
        SceneManager.LoadScene("Level"+gameController.GetCurrentLevel().ToString());
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Level" + gameController.GetCurrentLevel().ToString());
    }

    public void MainMenu()
    {
        gameController.ResetGame();
        SceneManager.LoadScene("StartScreen");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
