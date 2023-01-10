using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialHelper : MonoBehaviour
{
    public string tutorialName;
    Button button;
    GameObject minigameCanvas;
    public bool dog = false;


    // Start is called before the first frame update
    void Awake()
    {
        minigameCanvas = GameObject.FindGameObjectWithTag("TarefaCanvas");
        button = GetComponent<Button>();
        if (dog)
            button.onClick.AddListener(SpawnTutorialDog);
        else
            button.onClick.AddListener(SpawnTutorial);
    }

    public void SpawnTutorial()
    {
        GameObject go = Resources.Load<GameObject>("Tutoriais/Jogo/" + tutorialName);
        Instantiate(go, minigameCanvas.transform.position, transform.rotation * Quaternion.Euler(0, 0, 0), minigameCanvas.transform);
        
    }

    public void SpawnTutorialDog()
    {
        GameObject go = Resources.Load<GameObject>("Tutoriais/Jogo/" + tutorialName);
        Instantiate(go, transform.parent.transform.position, transform.rotation * Quaternion.Euler(0, 0, 0), transform.parent);
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
    }
}
