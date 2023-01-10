using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class DropDownTest : MonoBehaviour
{
    public TMP_Dropdown myDrop;
    public List<string> names = new List<string>();
    public TextMeshProUGUI displayName;
    string text = "n1.n2.n3.n4";

    // Start is called before the first frame update
    void Start()
    {
        myDrop = GetComponent<TMP_Dropdown>();
        StartCoroutine(GetFromServer());
        /*string[] splited = text.Split('.');
        for (int i = 0; i < splited.Length; i++)
        {
            names.Add(splited[i]);
        }
        SetDropDownNames();*/


    }

    IEnumerator GetFromServer()
    {
        Debug.LogError("Getting names...");
        UnityWebRequest request = UnityWebRequest.Get("https://yahp.prodb.com.br/cursos.txt");
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError(request.error);
            //GetImageFromDiskButtonClick();
        }
        else
        {
            text = request.downloadHandler.text;
            Debug.Log(text);
            string[] splited = text.Split('.');
            for (int i = 0; i < splited.Length; i++)
            {
                names.Add(splited[i]);
            }
            yield return new WaitForSeconds(0.1f);
            SetDropDownNames();
        }
    }

    void SetDropDownNames()
    {

        myDrop.ClearOptions();
        myDrop.AddOptions(names);
        displayName.text = myDrop.options[myDrop.value].text;

    }

    public void SetDisplayText()
    {
        displayName.text = myDrop.options[myDrop.value].text;
        PlayerPrefs.SetString("course_name", myDrop.options[myDrop.value].text);

    }
    // Update is called once per frame
    void Update()
    {

    }
}
