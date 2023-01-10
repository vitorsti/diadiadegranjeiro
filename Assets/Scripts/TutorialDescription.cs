using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;
using System;

public class TutorialDescription : MonoBehaviour
{
    NewTutorialManager tutorialManager;
    TutorialsContainer tutorialsContainer;
    public GameObject video, image, wI, noI, pointerPrefab;
    public string tutorialsContainerPath;
    string videoOrImage;
    public string Title;
    //public Text nameText;
    public Image imageDetails;
    public VideoPlayer videoPlayer;
    public TextMeshProUGUI description, nameText;
    int id;
    [SerializeField] int startId;
    [SerializeField] bool useStartId;
    //bool tutorail;
    public bool withImage;
    public Button close, next, prev;
    Vector3 pointerPrefabPosition;
    void Awake()
    {
        tutorialsContainer = Resources.Load<TutorialsContainer>(tutorialsContainerPath);
        tutorialManager = FindObjectOfType<NewTutorialManager>();
        close.onClick.AddListener(Close);
        next.onClick.AddListener(Next);
        prev.onClick.AddListener(Previous);
    }
    // Start is called before the first frame update
    void Start()
    {
        id = 0;
#if UNITY_EDITOR
        if (useStartId)
        {
            id = startId;
        }
#endif

        nameText.text = Title;
        Set();
        Time.timeScale = 0;
    }

    private void ReplaceText()
    {

        string og = description.text;

        if (og.Contains("endtime"))
        {

            Debug.Log("it contais!!");
            TimeManager timeManager = FindObjectOfType<TimeManager>();

            float timer = timeManager.endTime;

            int minutes = Mathf.FloorToInt(timer / 60F);
            int seconds = Mathf.FloorToInt(timer - minutes * 60);

            string niceTime = string.Format("{0:00}:{1:00}", minutes, seconds);

            og = og.Replace("endtime", niceTime);

            description.text = og;

            Debug.Log(og);

        }
        else
        {
            Debug.Log("It not contains");
        }


    }

    public void Next()
    {
        if (id != tutorialsContainer.datas.Length - 1)
        {
            id++;
            Set();
        }
        else if (id == tutorialsContainer.datas.Length - 1)
        {
            tutorialsContainer.SetFirstTime(false);
            id = tutorialsContainer.datas.Length - 1;
            if (tutorialManager != null)
                tutorialManager.NextTutorial();
            Close();
            //Destroy(this.gameObject);
            return;
        }
    }

    public void Previous()
    {
        if (id != 0)
        {
            id--;
            Set();
        }
        else if (id == 0)
        {
            id = 0;
            return;
        }
    }

    public void Set()
    {
        description.text = tutorialsContainer.GetDescrition(id);

        if (description.text.Contains("!endtime"))
        {
            TimeManager timeManager = FindObjectOfType<TimeManager>();

            float timer = timeManager.endTime;

            int minutes = Mathf.FloorToInt(timer / 60F);
            int seconds = Mathf.FloorToInt(timer - minutes * 60);

            string niceTime = string.Format("{0:00}:{1:00}", minutes, seconds);

            description.text.Replace("!endtime", niceTime);

        }
        /*imageDetails.sprite = tutorialsContainer.GetImage(id);
        videoPlayer.clip = tutorialsContainer.GetVideo(id);*/
        videoOrImage = tutorialsContainer.IsIamgeOrVideo(id).ToString();

        if (videoOrImage == "video" || videoOrImage == "image")
        {
            if (videoOrImage == "video")
            {
                image.SetActive(false);
                video.SetActive(true);
                video.GetComponent<VideoPlayer>().isLooping = true;
                videoPlayer.clip = tutorialsContainer.GetVideo(id);
            }

            if (videoOrImage == "image")
            {
                video.SetActive(false);
                image.SetActive(true);

                imageDetails.sprite = tutorialsContainer.GetImage(id);
            }

            GetComponent<RectTransform>().sizeDelta = wI.GetComponent<RectTransform>().sizeDelta;
            if (!tutorialsContainer.GetEnablePointer(id))
                GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        }
        else if (videoOrImage == "none")
        {
            video.SetActive(false);
            image.SetActive(false);
            GetComponent<RectTransform>().sizeDelta = noI.GetComponent<RectTransform>().sizeDelta;
            if (tutorialsContainer.GetEnablePointer(id))
            {
                if (tutorialsContainer.GetUseCustomPosition(id))
                    GetComponent<RectTransform>().anchoredPosition = tutorialsContainer.GetCustomPosition(id);
                else
                    GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -92);
            }
            else
            {
                if (tutorialsContainer.GetUseCustomPosition(id))
                    GetComponent<RectTransform>().anchoredPosition = tutorialsContainer.GetCustomPosition(id);
                else
                    GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                //GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -92);

            }
        }

        if (tutorialsContainer.GetEnablePointer(id))
        {
            DestroyPointer();

            pointerPrefab = tutorialsContainer.GetPointer(id);
            GameObject go = Instantiate(pointerPrefab, Vector3.zero, transform.rotation * Quaternion.Euler(0, 0, 0), this.transform.parent);
            if (tutorialsContainer.GetPointerPosition(id) != Vector3.zero)
                go.GetComponent<RectTransform>().anchoredPosition = tutorialsContainer.GetPointerPosition(id);
            go.name = "pointer";

        }
        else
        {
            DestroyPointer();
        }

        ReplaceText();
    }

    void DestroyPointer()
    {
        GameObject spawnedPrefeb = GameObject.Find("pointer");
        if (spawnedPrefeb != null)
            Destroy(spawnedPrefeb);
    }

    public void Close()
    {
        Time.timeScale = 1;

        DestroyPointer();

        Destroy(this.gameObject);

    }
}
