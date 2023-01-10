using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DailyQuizManager : MonoBehaviour
{
    public DateTime quitTime;
    public DateTime loginTime;
    public DateTime lastLogin;
    public TimeSpan dif;
    public CallQuizGame callQuizGame;
    public static bool sameDay;

    public bool deleteTimeSaved;

    private void OnValidate()
    {
        if (deleteTimeSaved)
        {
            PlayerPrefs.DeleteKey("dqs");
            PlayerPrefs.DeleteKey("DAILY_QUIZ");
            PlayerPrefs.DeleteKey("dailyQuizGameName");
            PlayerPrefs.DeleteKey("1p");
            PlayerPrefs.DeleteKey("2p");
            PlayerPrefs.DeleteKey("3p");
            PlayerPrefs.DeleteKey("4p");
            PlayerPrefs.DeleteKey("5p");
            deleteTimeSaved = false;
        }
    }

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("dqs", 0) == 1)
        {
            loginTime = System.DateTime.UtcNow;
            lastLogin = System.DateTime.Parse(PlayerPrefs.GetString("DAILY_QUIZ", "Theres no date registered"));


            Debug.Log("Time now: " + loginTime.ToString());
            Debug.Log("Last login: " + lastLogin.ToString());

            CalculateTimeDiferance();

        }
        else
        {
            callQuizGame.RandomQuiz();
            sameDay = false;
        }

        Debug.Log(sameDay);
    }

    public void CalculateTimeDiferance()
    {
        dif = loginTime.Subtract(lastLogin);
        Debug.Log("difference: " + dif.ToString());
        if (dif >= System.TimeSpan.FromDays(1))
        {
            sameDay = false;
            Debug.Log("24h has passed");
            PlayerPrefs.SetInt("1p", 0);
            PlayerPrefs.SetInt("2p", 0);
            PlayerPrefs.SetInt("3p", 0);
            PlayerPrefs.SetInt("4p", 0);
            PlayerPrefs.SetInt("5p", 0);
            callQuizGame.RandomQuiz();
        }
        else
        {
            sameDay = true;
            Debug.Log("not yet");
            callQuizGame.SameQuiz();
        }
    }

    void OnApplicationQuit()
    {
        quitTime = System.DateTime.UtcNow;
        PlayerPrefs.SetString("DAILY_QUIZ", quitTime.ToString());
        Debug.Log("New Date: " + quitTime.ToString());
        PlayerPrefs.SetInt("dqs", 1);
    }
}
