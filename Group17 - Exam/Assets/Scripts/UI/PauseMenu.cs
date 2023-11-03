using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject menuScreen;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Check if the GameObject is not already active
            if (!menuScreen.activeSelf)
            {
                menuScreen.SetActive(true);
            }
        }
    }
}
