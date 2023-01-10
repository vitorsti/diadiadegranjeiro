using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutotrialAnimationsTrigger : MonoBehaviour
{
    GameManager gameManager;
    TutorialManager tutorialManager;
    public void SpawnPlayer()
    {
        gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
        gameManager.EnablePlayer(true);
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    public void NextTutorial(){
        tutorialManager = (TutorialManager)FindObjectOfType(typeof(TutorialManager));
        tutorialManager.NextTutorial();
    }

    public void CustomizationButton(){
        gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
        gameManager.EnableCustomizationButton(false);
    }

       



}
