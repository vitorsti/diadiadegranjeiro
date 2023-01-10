using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GetColorAndLogoTest : MonoBehaviour
{
    public RawImage logo;
    public Image color1, color2;

    public Color c1, c2;
    string serverUrl = "https://yahp.prodb.com.br/";
    public List<string> colors = new List<string>();
    public string cor1, cor2;

    // Start is called before the first frame update
    void Awake()
    {
        PlayerPrefs.SetString("course_name", "Yahp");
        StartCoroutine(DownloadImage(serverUrl + PlayerPrefs.GetString("course_name") + "/"));
        StartCoroutine(DownloadColors(serverUrl + PlayerPrefs.GetString("course_name") + "/"));
    }

    IEnumerator DownloadImage(string MediaUrl)
    {
        Debug.LogError("Downloading image...");
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl + "logo" + ".png");
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError(request.error);
            //GetImageFromDiskButtonClick();
        }
        else
        {
            logo.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;

        }
    }

    IEnumerator DownloadColors(string MediaUrl)
    {
        Debug.LogError("Downloading image...");
        UnityWebRequest request = UnityWebRequest.Get(MediaUrl + "Cores.txt");
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError(request.error);
            //GetImageFromDiskButtonClick();
        }
        else
        {
            //Debug.Log(request.downloadHandler.text);
            string text = request.downloadHandler.text;
            Debug.Log(text);
            string[] splited = text.Split('.');
            for (int i = 0; i < splited.Length; i++)
            {
                colors.Add(splited[i]);
            }

            yield return new WaitForSeconds(0.2f);
            SetColors();

        }
    }

    public void SetColors()
    {
        Debug.Log("seting ccolors");
        cor1 = colors[0].Replace("primaria", "");
        Debug.Log(cor1);
        cor2 = colors[1].Replace("secundaria", "");
        Debug.Log(cor2);
        ColorUtility.TryParseHtmlString(cor1, out c1);
        ColorUtility.TryParseHtmlString(cor2, out c2);
        color1.color = c1;
        color2.color = c2;
    }
}
