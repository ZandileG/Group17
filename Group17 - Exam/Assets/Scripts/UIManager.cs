using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
   public void ChangeScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void pause()    
    {
        Time.timeScale = 0;
    }

    public void resume()
    {
        Time.timeScale = 1;
    }
}
