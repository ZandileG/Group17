using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    public GameObject menuScreen;
    private bool isPaused;
    
    private void Start()
    {
        isPaused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Check if the GameObject is not already active
            if (!isPaused)
            {
                musicSource.Stop();
                isPaused = true;
                menuScreen.SetActive(true);
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            } else if (isPaused)
            {
                isPaused = false;
                menuScreen.SetActive(false);
                Resume();
            }
                
        }
    }

    public void Resume()
    {
        musicSource.Play();
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }
}
