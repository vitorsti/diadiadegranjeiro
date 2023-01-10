using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BillboardManager : MonoBehaviour
{
    public RawImage billboard;
    public Button button;
    public Texture2D defaultTexture;
    public Texture2D loaded;
    public List<Texture2D> textures = new List<Texture2D>();

    [SerializeField]
    string timersText;
    public List<string> logosTimers = new List<string>();

    [SerializeField]
    string urlsText;
    public List<string> logosUrls = new List<string>();
    int index = 0;

    //Debugger
    public float duration;
    bool triggerTimer;
    //


    //public bool changeTexture;

    private void OnValidate()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        loaded = ImagesFromServerMethods.GetCourseLogo(loaded);
        textures.Add(loaded);
        logosTimers.Add("Curso");
        logosUrls.Add("Curso");

        loaded = ImagesFromServerMethods.GetGeradorLogo(loaded);
        textures.Add(loaded);
        logosTimers.Add("Gerador");
        logosUrls.Add("Gerador");

        loaded = ImagesFromServerMethods.GetSiloLogo(loaded);
        textures.Add(loaded);
        logosTimers.Add("Silo");
        logosUrls.Add("Silo");

        loaded = ImagesFromServerMethods.GetCaixaAguaLogo(loaded);
        textures.Add(loaded);
        logosTimers.Add("CaixaAgua");
        logosUrls.Add("CaixaAgua");

        SetLogoTimers();
        SetLogoUrl();

        StartCoroutine(ChangeBillboardTexture());
    }

    void SetLogoTimers()
    {
        timersText = ImagesFromServerMethods.GetLogoTimers(timersText);
        //timersText = "";

        if (timersText != null || timersText != "")
        {
            string[] splited = timersText.Split('.');

            for (int i = 0; i < logosTimers.Count; i++)
            {
                for (int j = 0; j < splited.Length; j++)
                {
                    if (splited[j].Contains(logosTimers[i]))
                    {
                        Debug.LogError("logo timers " + i + " contains: " + logosTimers[i] + " " + j);
                        string replace = splited[j].Replace(logosTimers[i], "");
                        Debug.Log(replace);
                        logosTimers[i] = replace;
                        string replace1 = logosTimers[i].Replace("-", "");
                        logosTimers[i] = replace1;

                    }

                }
            }
        }

        if (timersText == null || timersText == "")
        {
            for (int i = 0; i < logosTimers.Count; i++)
            {
                logosTimers[i] = "5";
            }
        }

    }

    void SetLogoUrl()
    {
        urlsText = ImagesFromServerMethods.GetLogoUrls(urlsText);
        if (urlsText != null || urlsText != "")
        {
            string[] splited = urlsText.Split('|');
            for (int i = 0; i < logosUrls.Count; i++)
            {
                for (int j = 0; j < splited.Length; j++)
                {
                    if (splited[j].Contains(logosUrls[i]))
                    {
                        Debug.LogError("logo urls " + i + " contains: " + logosUrls[i] + " " + j);
                        string replace = splited[j].Replace(logosUrls[i], "");
                        Debug.Log(replace);
                        logosUrls[i] = replace;
                        string replace1 = logosUrls[i].Replace("-", "");
                        logosUrls[i] = replace1;
                    }
                }
            }
        }

        if (urlsText == null || urlsText == "")
        {
            for (int i = 0; i < logosUrls.Count; i++)
            {
                logosUrls[i] = "http://www.yahp.com.br";
            }
        }
    }

    IEnumerator ChangeBillboardTexture()
    {
        if (button.onClick != null)
            button.onClick.RemoveAllListeners();

        if (textures[index] != null)
            billboard.texture = textures[index];
        else
            billboard.texture = defaultTexture;

        button.onClick.AddListener(delegate { GoToUrl(logosUrls[index]); });
        Debug.LogError(billboard.mainTexture.name);
        duration = float.Parse(logosTimers[index]);
        triggerTimer = true;
        yield return new WaitForSeconds(float.Parse(logosTimers[index]));


        index++;
        if (index > logosTimers.Count - 1)
            index = 0;

        StartCoroutine(ChangeBillboardTexture());
    }

    void GoToUrl(string url)
    {
        Debug.Log(url);
        Application.OpenURL(url);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (triggerTimer)
        {
            duration -= Time.deltaTime;
            if (duration <= 0)
            {
                duration = 0.0f;
                triggerTimer = false;
            }
        }
    }
}
