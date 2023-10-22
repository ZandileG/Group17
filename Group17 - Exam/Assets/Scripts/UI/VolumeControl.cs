using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Button muteButton;       // Button to mute the audio
    public Button unmuteButton;     // Button to unmute the audio
    public AudioSource audioSource; // Reference to the AudioSource

    private float originalVolume;   // Store the original volume before muting

    private void Start()
    {
        // Store the original volume
        originalVolume = audioSource.volume;

        // Attach click event handlers to the buttons
        muteButton.onClick.AddListener(MuteAudio);
        unmuteButton.onClick.AddListener(UnmuteAudio);
    }

    private void MuteAudio()
    {
        // Mute the audio by setting volume to 0
        audioSource.volume = 0f;

        // Toggle button activation
        muteButton.gameObject.SetActive(false);
        unmuteButton.gameObject.SetActive(true);
    }

    private void UnmuteAudio()
    {
        // Unmute the audio by setting volume back to the original value
        audioSource.volume = originalVolume;

        // Toggle button activation
        muteButton.gameObject.SetActive(true);
        unmuteButton.gameObject.SetActive(false);
    }
}
