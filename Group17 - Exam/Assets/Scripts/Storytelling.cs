using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Storytelling : MonoBehaviour
{
    public Text storyText;
    public string[] storySentences;
    public float textSpeed = 0.1f;

    private int currentSentenceIndex;
    private bool isTextAnimating;

    private void Start()
    {
        StartCoroutine(AnimateText());
    }

    private void Update()
    {
        // Skip or advance text with player input (e.g., mouse click or a key press).
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.A))
        {
            if (isTextAnimating)
            {
                // Skip the animation if text is currently animating.
                StopAllCoroutines();
                isTextAnimating = false;
                storyText.text = storySentences[currentSentenceIndex];
            }
            else
            {
                // Display the next sentence.
                currentSentenceIndex++;
                if (currentSentenceIndex < storySentences.Length)
                {
                    StartCoroutine(AnimateText());
                }
                else
                {
                    // The story is complete. Transition to the main game scene.
                    SceneManager.LoadScene(2);
                }
            }
        }
    }

    IEnumerator AnimateText()
    {
        isTextAnimating = true;
        string sentence = storySentences[currentSentenceIndex];
        storyText.text = "";

        for (int i = 0; i < sentence.Length; i++)
        {
            storyText.text += sentence[i];
            yield return new WaitForSeconds(textSpeed);
        }

        isTextAnimating = false;
    }
}


