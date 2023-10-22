using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void Home()
    {
        SceneManager.LoadScene("MainMenu");

    }

    // Update is called once per frame
    public void Quit ()
    {
        Application.Quit(); 
    }
}
