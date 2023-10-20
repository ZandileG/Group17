using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StoryController : MonoBehaviour
{
    
    [SerializeField] private Text choicePositive, choiceNegative, villainDialogue;
    [TextArea]
    [SerializeField] private string textChoicePositive, textChoiceNegative;
    [TextArea]
    [SerializeField] private string villainStatement, villainResponsePositive, villainResponseNegative;
    [SerializeField] private GameObject buttonChoicePositive, buttonChoiceNegative, continueButton;
    private GameController gameController;
    private LevelManager levelManager;
    private void Start()
    {
        buttonChoicePositive.SetActive(true);
        buttonChoiceNegative.SetActive(true);
        continueButton.SetActive(false);
        villainDialogue.text = villainStatement;
        gameController = FindObjectOfType<GameController>();
        levelManager = FindObjectOfType<LevelManager>();
        choicePositive.text = textChoicePositive;
        choiceNegative.text = textChoiceNegative;
    }

    public void ChooseChoice(int weight)
    {
        if (weight < 0)
            villainDialogue.text = villainResponseNegative;
        else
            villainDialogue.text = villainResponsePositive;

        gameController.ModifyVillainOpinion(weight);
        buttonChoicePositive.SetActive(false);
        buttonChoiceNegative.SetActive(false);
        continueButton.SetActive(true);
    }

    public void Continue()
    {
        levelManager.ShowVictoryScreen();
    }
}
