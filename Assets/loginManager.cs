using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class loginManager : MonoBehaviour
{
    public TMP_Dropdown myDrop;
    public TMP_InputField myInputField;
    public List<string> names = new List<string>();
    public TextMeshProUGUI courseName;
    public TextMeshProUGUI playerName;
    public GameObject confirmationScreen;
    string text;
    bool selected;

    private void Awake()
    {
        if (confirmationScreen.activeInHierarchy)
            confirmationScreen.SetActive(false);

        selected = false;
        myDrop = FindObjectOfType<TMP_Dropdown>();
        myInputField = FindObjectOfType<TMP_InputField>();
        StartCoroutine(GetFromServer());
    }
    // Start is called before the first frame update
    IEnumerator GetFromServer()
    {
        myDrop.interactable = false;
        Debug.LogError("Getting names...");
        UnityWebRequest request = UnityWebRequest.Get("https://yahp.prodb.com.br/cursos.txt");
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError(request.error);
            myDrop.ClearOptions();
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
        myDrop.interactable = true;
        myDrop.ClearOptions();
        myDrop.AddOptions(names);
        //displayName.text = myDrop.options[myDrop.value].text;

    }

    public void ValueChangedDropDown()
    {
        //courseName.text = myDrop.options[myDrop.value].text;
        //PlayerPrefs.SetString("course_name", myDrop.options[myDrop.value].text);
        //PlayerPrefs.Save();
        selected = true;

    }

    public void ValueChangedInputField()
    {

    }

    // Update is called once per frame
    public void Check()
    {
        if (selected == true && myInputField.text != "")
        {
            confirmationScreen.SetActive(true);
            string replaceC = courseName.text;
            courseName.text = replaceC.Replace("+courseName", myDrop.options[myDrop.value].text);
            string replaceP = playerName.text;
            playerName.text = replaceP.Replace("+playerName", myInputField.text);
        }
        LoginScreenInputsError loginScreenInputsError = this.GetComponent<LoginScreenInputsError>();
        loginScreenInputsError.StartFlicker(selected, myInputField.text);
    }

    public void No()
    {
        string replaceC = courseName.text;
        courseName.text = replaceC.Replace(myDrop.options[myDrop.value].text, "+courseName");
        string replaceP = playerName.text;
        playerName.text = replaceP.Replace(myInputField.text, "+playerName");
        confirmationScreen.SetActive(false);
    }

    public void Continue()
    {
        PlayerPrefs.SetString("player_name", myInputField.text);
        PlayerPrefs.SetString("course_name", myDrop.options[myDrop.value].text);
        PlayerPrefs.Save();

        ImagesFromServerMethods.DownloadImages();

        Canvas canvas = FindObjectOfType<Canvas>();

        canvas.GetComponent<TitleSCreenManager>().loginScreen.SetActive(false);
        canvas.GetComponent<TitleSCreenManager>().characterChooseScreen.SetActive(true);
    }
}
