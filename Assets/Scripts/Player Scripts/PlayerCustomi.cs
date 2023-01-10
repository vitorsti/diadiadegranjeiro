using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ScriptableObjectExtensions.Variables;

public class PlayerCustomi : MonoBehaviour
{

    public List<Color> colors = new List<Color>();
    int colorsLength;
    int colorPosition;
    public GameObject playerSprite;
    public GameObject player;
    public Text bodyColorPositionText;
    public string playerName;
    public Text playerNameText;
    public InputField playerNameInput;

    // Start is called before the first frame update
    void Start()
    {

        /*colorsLength = 5;
        do
        {
            RandomColor();
        } while (colors.Count < colorsLength);*/

        if (playerSprite != null)
        {
            //player = GameObject.FindGameObjectWithTag("Player");
            //playerSprite.GetComponent<SpriteRenderer>().color = colors[PlayerPrefs.GetInt("Color")];
        }
        if (playerNameText != null)
            playerNameText.text = PlayerPrefs.GetString("PlayerName", "Jogador");
        colorPosition = PlayerPrefs.GetInt("Color");
        if (bodyColorPositionText != null)
            bodyColorPositionText.text = (colorPosition + 1) + "/" + colorsLength;

    }

    // Update is called once per frame
    void Update()
    {
        if (playerSprite == null)
        {
            //player = GameObject.FindGameObjectWithTag("Player");
            //playerSprite.GetComponent<SpriteRenderer>().color = colors[PlayerPrefs.GetInt("Color")];
        }


    }

    public void RandomColor()
    {
        Color newcolor = Random.ColorHSV();
        colors.Add(newcolor);
    }

    public void ChangeBodyColor(bool upOrDown)
    {

        if (upOrDown)
        {
            colorPosition++;


            if (colorPosition >= colors.Count)
            {
                colorPosition = colors.Count - 1;
            }
            playerSprite.GetComponent<SpriteRenderer>().color = colors[colorPosition];
            PlayerPrefs.SetInt("Color", colorPosition);
        }
        else
        {
            colorPosition--;
            if (colorPosition <= 0)
            {
                colorPosition = 0;
            }
            playerSprite.GetComponent<SpriteRenderer>().color = colors[colorPosition];
            PlayerPrefs.SetInt("Color", colorPosition);
        }

        bodyColorPositionText.text = (colorPosition + 1) + "/" + colorsLength;

    }

    public void PlayerName(string name)
    {
        name = playerNameInput.text;
        playerNameText.text = name;
        playerName = name;
        //PlayerPrefs.SetString("PlayerName", playerName);
    }

    public void LoadPrefs()
    {
        colorPosition = PlayerPrefs.GetInt("Color");
        bodyColorPositionText.text = (colorPosition + 1) + "/" + colorsLength;
    }

    public void SavePrefs()
    {
        //player.GetComponent<SpriteRenderer>().color = colors[colorPosition];
        if (playerName != "")
        {
            PlayerPrefs.SetString("PlayerName", playerName);
        }
        else
        {
            print("nome não salvo, pois está em branco");
            return;
        }

    }

    public void SelectGender(int id)
    {
        PlayerPrefs.SetInt("Gender", id);
        if (SceneManager.GetActiveScene().name == "Game")
        {
            GameObject minigameCanvas = GameObject.FindGameObjectWithTag("TarefaCanvas");
            minigameCanvas.GetComponent<Canvas>().enabled = true;
            this.gameObject.SetActive(false);
        }
        else
        {
            Canvas canvas = FindObjectOfType<Canvas>();
            canvas.GetComponent<TitleSCreenManager>().LoadingScreen.SetActive(true);
            canvas.GetComponent<TitleSCreenManager>().LoadingScreen.GetComponent<SceneLoader>().LoadScene("Game");
            this.gameObject.SetActive(false);
        }
    }


}
