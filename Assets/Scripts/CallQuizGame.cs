using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallQuizGame : MonoBehaviour
{
    public GameObject mainCanvas;
    public GameObject[] prefabs;
    public GameObject prefab;
    public bool tewntyFourHoursPassed;

    // Start is called before the first frame update

    private void Start() {

        mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas");
        prefabs = Resources.LoadAll<GameObject>("Quiz/Game");
        GetComponent<Button>().onClick.AddListener(SpawnQuizGame);
    }

    public void RandomQuiz()
    {

        //mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas");
        prefabs = Resources.LoadAll<GameObject>("Quiz/Game");
        //GetComponent<Button>().onClick.AddListener(SpawnQuizGame);
        prefab = prefabs[Random.Range(0, prefabs.Length)];
        if(PlayerPrefs.GetString("dailyQuizGameName", ".") == "."){
            PlayerPrefs.SetString("dailyQuizGameName", prefab.name);
            print(PlayerPrefs.GetString("dailyQuizGameName"));
        }
    }

    public void SameQuiz()
    {
        prefabs = Resources.LoadAll<GameObject>("Quiz/Game");
        for (int i = 0; i < prefabs.Length; i++)
        {
            if (prefabs[i].name == PlayerPrefs.GetString("dailyQuizGameName", "."))
            {
                prefab = prefabs[i];
            }
        }
    }

    public void SpawnQuizGame()
    {
        GameObject go = Instantiate(prefab, mainCanvas.transform.position, mainCanvas.transform.rotation);
        go.name = prefab.name;
        PlayerPrefs.SetString("dailyQuizGameName", prefab.name);
    }
}
