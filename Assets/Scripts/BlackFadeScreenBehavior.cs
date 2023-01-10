using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class BlackFadeScreenBehavior : MonoBehaviour
{
    public GameObject blackOutSquare;
    public GameObject player, playerTarget;
    public Vector3 homePosiotion;
    public float fadeSpeed;
    void Awake()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
    }
    void OnEnable()
    {
        FadeIn();
    }

    // Update is called once per frame

    public void FadeIn()
    {
        StartCoroutine(FadeBlackOutSquare());
    }

    public void FadeOut()
    {
        StartCoroutine(FadeBlackOutSquare(false));
    }

    public void PlayerHome()
    {
        
        player.transform.position = homePosiotion;
        player.GetComponent<NavMeshAgent>().destination = homePosiotion;

       
    }

    public IEnumerator FadeBlackOutSquare(bool fadeToBlack = true)
    {
        Color objectColor = blackOutSquare.GetComponent<Image>().color;
        float fadeAmount;

        if (fadeToBlack)
        {
            while (blackOutSquare.GetComponent<Image>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }

            PlayerHome();
            Camera.main.GetComponent<CameraBehavior>().GoToPlayer();
            FadeOut();
            TimeManager.ResetTime();
        }
        else
        {
            while (blackOutSquare.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }

            this.gameObject.SetActive(false);
            
        }

    }
}
