using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class MissionManager : MonoBehaviour
{

    public GameObject content;
    public GameObject scrollView;
    public GameObject actualMission;
    public Slider healthBarSlider;
    public int healthBar;
    public List<GameObject> missionsList = new List<GameObject>();
    public int missionListCount;
    RectTransform rt;

    // Start is called before the first frame update
    void Start()
    {
        rt = content.GetComponent<RectTransform>();
        foreach (GameObject i in missionsList)
        {

            GameObject obj = Instantiate(i);
            obj.transform.SetParent(content.transform, false);
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y + 30);
        }

        healthBarSlider.value = healthBar;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            Enabledisable();

        }

        /*for (int i = 0; i < missionsList.Count; i++)
        {

            GameObject obj = Instantiate(missionsList[i].gameObject);
            obj.transform.SetParent(content.transform, false);
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y + 30);
            break;
        }*/

        healthBarSlider.value = healthBar;

    }

    public void AddMission(GameObject missionToAdd)
    {

        int i = Random.Range(1, 20);
        string missionText = "Mission: " + i;
        missionToAdd.GetComponentInChildren<Text>().text = missionText;
        missionsList.Add(missionToAdd);

        GameObject obj = Instantiate(missionToAdd);
        obj.transform.SetParent(content.transform, false);
        obj.GetComponent<Button>().onClick.AddListener(SetCurrentMission);
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y + 30);

        /*foreach (GameObject j in missionsList)
        {
            GameObject obj = Instantiate(j.gameObject);
            obj.transform.SetParent(content.transform, false);
            obj.GetComponent<Button>().onClick.AddListener(SetCurrentMission);
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y + 30);
        }*/

    }

    public void SetCurrentMission()
    {

        // puxar o preab atraves de id de qual seria o mini game
        string name = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
        actualMission.GetComponentInChildren<Text>().text = "Current Mission - " + name;
    }

    public void SpawnMission(string ResourcesPath)
    {
        if (healthBar <= 50)
        {
            // https://docs.unity3d.com/ScriptReference/Resources.Load.html
            //missionsList = (GameObject<>)Resources.LoadAll("RegiaoUm");
            //GameObject obj = Resources.Load<GameObject>("RegiaoUm/MiniGameUm");
            //AddMission(obj);
            //Debug.Log("Mission spawned");
            foreach (GameObject g in Resources.LoadAll(ResourcesPath, typeof(GameObject)))
            {
                Debug.Log("prefab found: " + g.name);
                missionsList.Add(g);
            }
        }

        if (missionsList == null || missionsList.Count == 0)
        {
            Debug.Log("No missions spawned");
        }
        else
        {
            foreach (GameObject i in missionsList)
            {

                int j = Random.Range(1, 20);
                string missionText = "Mission: " + j;
                i.GetComponentInChildren<Text>().text = missionText;

                GameObject obj = Instantiate(i);
                obj.transform.SetParent(content.transform, false);
                obj.GetComponent<Button>().onClick.AddListener(SetCurrentMission);
                rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y + 30);
            }
        }


    }

    public void removeHealth(int amountToRemove)
    {
        if (healthBar > 50)
        {
            healthBar -= amountToRemove;
        }
        else
        {
            Debug.Log("cant remove more health");
            return;
        }
        //SpawnMission();

    }

    public void Enabledisable()
    {
        if (scrollView.activeSelf == true)
        {

            scrollView.SetActive(false);
        }
        else
            scrollView.SetActive(true);
    }
}
