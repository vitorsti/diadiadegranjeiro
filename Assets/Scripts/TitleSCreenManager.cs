using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class TitleSCreenManager : MonoBehaviour
{
    public VideoPlayer videoPlayer1, videoPlayer2;
    public GameObject videoPlayerObjct1, videoPlayerObject2;
    public GameObject titleScreenObjct, characterChooseScreen, LoadingScreen, loginScreen;
    private bool title, v1, v2;
    public string tutorialSceneName, gameSceneName;
    public float n;
    public double videoLength;
    public double video1, video2;

    public bool removeVideo2;

    private void OnValidate()
    {
        if (removeVideo2)
        {
            videoPlayer2 = null;
            videoPlayerObject2 = null;
            removeVideo2 = false;
        }
    }

    void Awake()
    {
        n = 1f;
        //PlayGamesMethods.InitializeServices();

    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        videoLength = videoPlayer1.frameCount;

        title = false;
        v1 = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (videoPlayer1 != null && v1)
        {
            video1 = videoPlayer1.frame + 1;
        }

        if (videoPlayer2 != null && v2)
        {
            video2 = videoPlayer2.frame + 1;
        }

        if (video1 == videoLength && v1)
        {
            Destroy(videoPlayerObjct1);
            Destroy(videoPlayer1.gameObject);
            videoPlayer1.gameObject.SetActive(false);

            if (videoPlayer2 != null && videoPlayerObject2 != null)
            {
                videoPlayer2.gameObject.SetActive(true);
                videoPlayerObject2.SetActive(true);
                videoLength = videoPlayer2.frameCount;
                v2 = true;
            }
            else
            {
                GoToTitle(1);
            }
        }

        if (video2 == videoLength && v2)
        {
            GoToTitle(2);
        }
        //videoPlayer.clip.length
        // if (Input.GetMouseButtonDown(0))
        // {
        //     if (videoPlayerObjct1 != null)
        //     {
        //         if (video1 >= 10 && v1)
        //         {
        //             Destroy(videoPlayerObjct1);
        //             Destroy(videoPlayer1.gameObject);
        //             videoPlayer1.gameObject.SetActive(false);

        //             if (videoPlayer2 != null && videoPlayerObject2 != null)
        //             {
        //                 videoPlayer2.gameObject.SetActive(true);
        //                 videoPlayerObject2.SetActive(true);
        //                 videoLength = videoPlayer2.frameCount;
        //                 v1 = false;
        //                 v2 = true;
        //             }
        //             else
        //             {

        //                 titleScreenObjct.SetActive(true);
        //                 title = true;
        //                 v1 = false;
        //             }
        //         }
        //     }

        //     if (videoPlayerObject2 != null)
        //     {

        //         if (video2 >= 10 && v2)
        //         {

        //             Destroy(videoPlayer2.gameObject);
        //             Destroy(videoPlayerObject2);
        //             titleScreenObjct.SetActive(true);
        //             title = true;
        //             v2 = false;
        //         }
        //     }

        //     if (n == 0)
        //     {
        //         titleScreenObjct.SetActive(false);
        //         //Destroy(titleScreenObjct);

        //         /*if(primeira vez q o jogo esta iniciando// PlayerPrefs.GetInt("checarTutorial") == 0){
        //             carregar cena do tutorial
        //         }else{
        //             carregar jogo
        //         }*/
        //         if (PlayerPrefs.GetInt("FirstTimeRuning", 0) == 0)
        //             loginScreen.SetActive(true);
        //         //characterChooseScreen.SetActive(true);
        //         else
        //         {
        //             LoadingScreen.SetActive(true);
        //             LoadingScreen.GetComponent<SceneLoader>().LoadScene(gameSceneName);
        //         }

        //     }
        // }

        // if (title)
        // {
        //     n = n - Time.deltaTime;
        //     if (n <= 0)
        //         n = 0;
        //     //SceneManager.LoadScene(tutorialSceneName);
        // }
    }

    public void Skip()
    {
        if (videoPlayerObjct1 != null)
        {
            if (video1 >= 10 && v1)
            {
                Destroy(videoPlayerObjct1);
                Destroy(videoPlayer1.gameObject);
                videoPlayer1.gameObject.SetActive(false);

                if (videoPlayer2 != null && videoPlayerObject2 != null)
                {
                    videoPlayer2.gameObject.SetActive(true);
                    videoPlayerObject2.SetActive(true);
                    videoLength = videoPlayer2.frameCount;
                    v1 = false;
                    v2 = true;
                }
                else
                {

                    GoToTitle(1);
                }
            }
        }

        if (videoPlayerObject2 != null)
        {

            if (video2 >= 10 && v2)
            {

               GoToTitle(2);
            }
        }
    }
    void GoToTitle(int type)
    {
        if (type == 1)
        {
            titleScreenObjct.SetActive(true);
            //title = true;
            v1 = false;
        }
        if (type == 2)
        {
            Destroy(videoPlayer2.gameObject);
            Destroy(videoPlayerObject2);
            titleScreenObjct.SetActive(true);
            //title = true;
            v2 = false;
        }
        StartCoroutine(CountdowTimer(n));
    }
    public void Title()
    {
        //debug only comment line above after using
        //PlayerPrefs.SetInt("FirstTimeRuning",0);

        if (title)
        {
            titleScreenObjct.SetActive(false);
            //Destroy(titleScreenObjct);

            /*if(primeira vez q o jogo esta iniciando// PlayerPrefs.GetInt("checarTutorial") == 0){
                carregar cena do tutorial
            }else{
                carregar jogo
            }*/
            if (PlayerPrefs.GetInt("FirstTimeRuning", 0) == 0)
                loginScreen.SetActive(true);
            //characterChooseScreen.SetActive(true);
            else
            {
                LoadingScreen.SetActive(true);
                LoadingScreen.GetComponent<SceneLoader>().LoadScene(gameSceneName);
            }

        }


    }

    IEnumerator CountdowTimer(float timer)
    {
        yield return new WaitForSeconds(timer);
        title = true;
    }
}
