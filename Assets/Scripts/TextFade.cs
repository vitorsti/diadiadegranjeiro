using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFade : MonoBehaviour
{
    TextMeshProUGUI myTmp;
    GameObject prevFadeText;
    public float fadeSpeed;
    //public float speed;
    // Start is called before the first frame update
    void Awake()
    {
        prevFadeText = FindObjectOfType<TextFade>().gameObject;

        if (prevFadeText != null && prevFadeText != this.gameObject)
            Destroy(prevFadeText);

        myTmp = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        //transform.Translate(Vector3.down * speed *Time.deltaTime);
    }

    // Update is called once per frame
    void Start()
    {
        //StartCoroutine(Fade());
    }

    public static void SetText(string text){

        TextFade textFade = FindObjectOfType<TextFade>();
        textFade.myTmp.text = text;
        textFade.StartCoroutine(textFade.Fade());
    }

    public IEnumerator Fade()
    {
        Color c = myTmp.color;
        float fadeAmount;


        while (myTmp.color.a > 0)
        {
            fadeAmount = c.a - (fadeSpeed * Time.deltaTime);

            c = new Color(c.r, c.g, c.b, fadeAmount);
            myTmp.color = c;
            yield return null;
        }

        Destroy(this.gameObject);

    }

}
