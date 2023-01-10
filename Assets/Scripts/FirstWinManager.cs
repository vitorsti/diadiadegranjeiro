//Programmed by: Leonardo Tadeu Piassa Squarizzi
using System;
using UnityEngine;

namespace Misc
{
    public class FirstWinManager : MonoBehaviour
    {
        public static FirstWinManager instance = null;
        DateTime loginTime;
        DateTime oldDate;
        DateTime firstWinTime;

        public bool firstWin;
        int firstWinDelay = 1;


        void Awake()
        {
            #region Singleton Pattern Setup
            //Check if an instance of FirstWinManager already exists...
            if (instance == null)
            {
                //If not, set the instance to this.
                instance = this;
            }
            //If the instance already exists and it's not this...
            else if (instance != this)
            {
                //Destroy this instance.
                Destroy(gameObject);
            }
            #endregion
            //Mark the GameObject as DontDestroyOnLoad.
            DontDestroyOnLoad(this.gameObject);

            loginTime = System.DateTime.Now;
            Debug.Log("Data Agora: " + loginTime);
        }
        void Start()
        {
            if (PlayerPrefs.GetInt("STFIR_WIN_ACTIVE", 0) == 0)
            {
                PlayerPrefs.SetInt("STFIR_WIN_ACTIVE", 1);

            }
            else if (PlayerPrefs.GetInt("STFIR_WIN_ACTIVE") == 1)
            {

                AddMoney();

            }
            /*//FIRST_WIN_ACTIVE = 1 means first win is up, otherwise it has to be calculate
            if (PlayerPrefs.GetInt("FIRST_WIN_ACTIVE", 1) == 0)
            {
                //Store the current time when it starts
                loginTime = System.DateTime.Now;
                Debug.Log("Data Agora: " + loginTime);

                //Grab the old times from the player prefs as a long
                long fwDate = Convert.ToInt64(PlayerPrefs.GetString("FIRST_WIN_DATE"));

                //Convert the old time from binary to a DataTime variable
                firstWinTime = DateTime.FromBinary(fwDate);
                Debug.Log("Data First WIn: " + firstWinTime);


                if (loginTime >= firstWinTime)
                {
                    firstWin = true;
                    PlayerPrefs.SetInt("FIRST_WIN_ACTIVE", 1);
                    Debug.Log("Ativou First Win");
                    //AddMoney();

                }

                else
                {
                    firstWin = false;
                    PlayerPrefs.SetInt("FIRST_WIN_ACTIVE", 0);
                    Debug.Log("Não ativou porque não deu o timing");
                }
            }

            else
            {
                PlayerPrefs.SetInt("FIRST_WIN_ACTIVE", 0);
                firstWin = true;
                Debug.Log("Ja estava ativa");
                AddMoney();
            }*/
        }

        public void SetNewFirstWinDate()
        {
            /*firstWinTime = System.DateTime.Now.AddHours(firstWinDelay);
            firstWin = false;
            PlayerPrefs.SetInt("FIRST_WIN_ACTIVE", 0);
            PlayerPrefs.SetString("FIRST_WIN_DATE", firstWinTime.ToBinary().ToString());
            Debug.Log("Nova First win data: " + firstWinTime);*/
        }

        void OnApplicationQuit()
        {
            //Savee the current system time as a string in the player prefs class
            PlayerPrefs.SetString("QUIT_TIME", System.DateTime.Now.ToBinary().ToString());
            Debug.Log("Saving this date to prefs: " + System.DateTime.Now);
        }

        public void AddMoney()
        {

            IdleManager idle = FindObjectOfType<IdleManager>();
            long qt = Convert.ToInt64(PlayerPrefs.GetString("QUIT_TIME"));
            DateTime quitTimeDate = DateTime.FromBinary(qt);
            int time = oldDate.CompareTo(loginTime);

            if (time < 0)
            {

                float howMuchTimePassed = (float)(loginTime - quitTimeDate).TotalSeconds;
                float addMoney = howMuchTimePassed * idle.howMuchMoney;
                MoneyManager.AddMoney("gold", addMoney);
            }

        }

    }
}