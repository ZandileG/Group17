using UnityEngine;
using UnityEngine.UI;

public class ClickSound : MonoBehaviour
{
    public Button button;  // Reference to the UI Button
    public AudioSource audioSource;  // Reference to the AudioSource

    public AudioClip soundClip;  // The sound clip you want to play

    private void Start()
    {
        // Attach the button click event handler
        button.onClick.AddListener(PlaySound);
    }

    private void PlaySound()
    {
        // Play the sound clip
        audioSource.PlayOneShot(soundClip);
    }
}
