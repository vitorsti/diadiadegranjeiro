using UnityEngine;
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.UI;

public static class PlayGamesMethods
{
    public static void AddScoreToLeaderboard(int score)
    {
        GameObject.FindObjectOfType<PlayGames>().AddScoreToLeaderboard(score);
    }

    public static void ShowLeaderboard()
    {
        GameObject.FindObjectOfType<PlayGames>().ShowLeaderboard();
    }

    public static void InitializeServices()
    {
        GameObject.FindObjectOfType<PlayGames>().InitializeServices();
    }

    /*public static float GetScore()
    {
        //return GameObject.FindObjectOfType<PlayGames>().GetScore();
    }*/
}

public class PlayGames : MonoBehaviour
{
    public int playerScore;
    string leaderboardID = "CgkIqJ-9vpIWEAIQAA";
    string achievementID = "";
    public static PlayGamesPlatform platform;
    public Text text;
    public InputField input;

    void Start()
    {
        //InitializeServices();

        //if (platform == null)
        //{
        //    PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        //    PlayGamesPlatform.InitializeInstance(config);
        //    PlayGamesPlatform.DebugLogEnabled = true;
        //    platform = PlayGamesPlatform.Activate();
        //}

        //Social.Active.localUser.Authenticate(success =>
        //{
        //    if (success)
        //    {
        //        Debug.Log("Logged in successfully");
        //    }
        //    else
        //    {
        //        Debug.Log("Login Failed");
        //    }
        //});

        //UnlockAchievement();

    }
    private void Update()
    {
    }

    private void OnGUI()
    {
        //GUI.Label(new Rect(10, 225, 100, 20), GetScore());
    }

    public void InitializeServices()
    {
        if (platform == null)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            platform = PlayGamesPlatform.Activate();
        }

        Social.Active.localUser.Authenticate(success =>
        {
            if (success)
            {
                Debug.Log("Logged in successfully");
            }
            else
            {
                Debug.Log("Login Failed");
            }
        });
    }

    public void AddScoreToLeaderboard()
    {
        if (Social.Active.localUser.authenticated)
        {
            Social.ReportScore(playerScore, leaderboardID, success => { });
        }
        else
        {
            InitializeServices();

            if (Social.Active.localUser.authenticated)
            {
                Social.ReportScore(playerScore, leaderboardID, success => { });
            }
        }
    }

    public void AddScoreToLeaderboard(int score)
    {
        if (Social.Active.localUser.authenticated)
        {
            Social.ReportScore(score, leaderboardID, success => { });
        }
        else
        {
            InitializeServices();

            if (Social.Active.localUser.authenticated)
            {
                Social.ReportScore(score, leaderboardID, success => { });
            }
        }
    }

    public void ShowLeaderboard()
    {
        if (Social.Active.localUser.authenticated)
        {
            Social.ShowLeaderboardUI();
        }
        else
            InitializeServices();

    }

    public void ShowAchievements()
    {
        if (Social.Active.localUser.authenticated)
        {
            Social.ShowAchievementsUI();
        }
    }

    public void UnlockAchievement()
    {
        if (Social.Active.localUser.authenticated)
        {
            Social.ReportProgress(achievementID, 100f, success => { });
        }
    }

    public void ShowScore()
    {
        //text.text = GetScore();
    }

    public void SendScore()
    {
        if (input.text != " ")
            AddScoreToLeaderboard(int.Parse(input.text));
        else
            Debug.LogError("campo vazio");
        input.text = " ";
    }

    /*public string GetScore()
    {
        //

        PlayGamesPlatform.Instance.LoadScores(
             leaderboardID,
             LeaderboardStart.PlayerCentered,
             1,
             LeaderboardCollection.Public,
             LeaderboardTimeSpan.AllTime,
         (LeaderboardScoreData data) =>
         {
             Debug.LogError(data.Valid);
             Debug.LogError(data.Id);
             Debug.LogError(data.PlayerScore);
             Debug.LogError(data.PlayerScore.userID);
             Debug.LogError(data.PlayerScore.formattedValue);
             playerScore = int.Parse(data.PlayerScore.formattedValue); //playerScore = 19; Debug.Log(playerScore);

         });

        return playerScore.ToString();
    }*/

}