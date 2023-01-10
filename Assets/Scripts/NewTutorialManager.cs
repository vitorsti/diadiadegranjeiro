using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewTutorialManager : MonoBehaviour
{
    TutorialsContainer tutorialsData;
    public List<GameObject> tutorials = new List<GameObject>();
    public GameObject canvas;
    GameObject go;
    public bool doNotDestroy;
    bool enableTutorial;

    [SerializeField]
    private bool enableTutorialInEditor;

    void Awake()
    {
        TutorialDescription t = FindObjectOfType<TutorialDescription>();

        if (t != null)
        {
            Destroy(t.gameObject);
        }


#if UNITY_EDITOR
        this.gameObject.SetActive(enableTutorialInEditor);

#endif
        if (SceneManager.GetActiveScene().name.Contains("Maker"))
        {
            canvas = FindObjectOfType<Canvas>().gameObject;
        }
        else
            canvas = GameObject.FindGameObjectWithTag("TutorialCanvas");



    }

    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = 0;


        GetTutorialData();

        if (enableTutorial)
        {
            go = Instantiate(tutorials[0], canvas.transform.position, canvas.transform.rotation, canvas.transform);
            go.name = tutorials[0].name;
        }
        else
            NextTutorial();
    }

    void Update()
    {
#if UNITY_EDITOR

        //Destroy(go);
        this.gameObject.SetActive(enableTutorialInEditor);
#endif

        if (tutorials.Count == 0)
        {
            End();
        }
    }

    public void NextTutorial()
    {

        if (tutorials.Count == 1 /*&& go != null*/)
        {
            Destroy(go);
            End();
            //Destroy(this.gameObject);
        }
        else
        {
            Destroy(go);
            tutorials.RemoveAt(0);
            GetTutorialData();
            if (enableTutorial)
            {
                go = Instantiate(tutorials[0], canvas.transform.position, canvas.transform.rotation, canvas.transform);
                go.name = tutorials[0].name;
            }
            else
                NextTutorial();

        }

    }

    public void GetTutorialData()
    {
        tutorialsData = Resources.Load<TutorialsContainer>(tutorials[0].GetComponent<TutorialDescription>().tutorialsContainerPath);
        enableTutorial = tutorialsData.GetFirstTime();
    }

    public void End()
    {
        //print("hahahahah");
        //Time.timeScale = 1;
        if (doNotDestroy)
            this.gameObject.SetActive(false);
        else
            Destroy(this.gameObject, 2f);
    }

}
