using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public List<GameObject> tutorials = new List<GameObject>();
    GameObject[] tutorialsArray;
    public GameObject[] tutorialsTextGameobjetc;
    public List<Text> tutorialsTexts = new List<Text>();
    public int texts;
    public string playerName;
    public string test;
    GameManager gameManager;
    public GameObject tutorialCustomScrrenSaveButton;
    public bool enableTutorial;

    // Start is called before the first frame update
    void Start()
    {
        /*foreach(GameObject i in tutorials){
            i.SetActive(false);
        }*/
        if (enableTutorial)
        {
            gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
            gameManager.EnablePlayerMovementAndCameraControler(false);
            gameManager.EnablePlayer(true);
            gameManager.PlayerCustomScreen(true);
            tutorialCustomScrrenSaveButton.SetActive(true);

            tutorials[0].SetActive(true);

            playerName = PlayerPrefs.GetString("PlayerName", "Jogador");
            tutorialsTexts[1].text = tutorialsTexts[1].text.Replace("&name", playerName);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (enableTutorial)
        {
            string temp = PlayerPrefs.GetString("PlayerName", "Jogador");
            if (playerName != temp && temp != null)
            {
                tutorialsTexts[1].text = tutorialsTexts[1].text.Replace(playerName, temp);
                print("nome do jogador alterado, de " + playerName + " para " + temp);
                playerName = temp;
                enableTutorial = false;
            }
        }
    }

    public void FindAllTutorialText()
    {
        if (texts > 0)
        {
            Debug.Log("Todos os textos já foram achados");
            return;
        }

        foreach (GameObject i in tutorials)
        {
            i.SetActive(true);
        }

        tutorialsTextGameobjetc = GameObject.FindGameObjectsWithTag("TutorialText");

        foreach (GameObject i in tutorialsTextGameobjetc)
        {
            tutorialsTexts.Add(i.GetComponent<Text>());
            texts++;
        }

        foreach (GameObject i in tutorials)
        {
            i.SetActive(false);
        }

    }

    public void FindAllTutorials()
    {
        if (tutorialsArray.Length > 0 && tutorials.Count > 0)
        {
            Debug.Log("Tutoriais já foram achados");
            return;
        }
        tutorialsArray = GameObject.FindGameObjectsWithTag("Tutorial");
        foreach (GameObject i in tutorialsArray)
        {

            tutorials.Add(i);
        }

        Debug.Log("Tutoriais encontrados");
    }

    public void NextTutorial()
    {
        if (tutorials.Count == 0)
        {
            return;
        }

        if (tutorials.Count > 1)
        {
            tutorials[0].gameObject.SetActive(false);
            tutorials.RemoveAt(0);
            tutorials[0].gameObject.SetActive(true);
        }
        else if (tutorials.Count == 1)
        {
            tutorials[0].gameObject.SetActive(false);
        }


    }

    public void EnableTutorial()
    {
        //tutorials[0].SetActive(true);
        gameManager.EnablePlayerMovementAndCameraControler(false);
    }

    public void TutorialEnd()
    {
        tutorials.RemoveAt(0);
        tutorials[0].gameObject.SetActive(true);
    }




}
