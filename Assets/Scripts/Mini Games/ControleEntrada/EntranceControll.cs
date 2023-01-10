using EventProperties;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class EntranceControll : MonoBehaviour
{
    [Header("----- Jogo -----")]
    [SerializeField] bool refuse;
    [SerializeField] bool hasWrong;
    [SerializeField] int chooseCloth;
    [SerializeField] int wrongCloth;
    [SerializeField] int selectCloth;
    [SerializeField] Button[] buttons;

    [Header("----- UI -----")]
    public TextMeshProUGUI regionTxt;

    [Header("----- Database -----")]
    public Cloth[] correctCloths;

    [Header("----- Documento -----")]
    public Cloth docs;
    public Image[] docImages;

    [Header("----- Pessoa -----")]
    public Cloth person;

    [Serializable]
    public struct Cloth
    {
        public string regionName;
        public Sprite[] clothParts;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    public void CustomUpdate()
    {
        if (refuse)
            foreach (Button bt in buttons)
                bt.gameObject.GetComponent<Button>().enabled = true;


    }

    public void ClickedCloth(Button _button)
    {
        int i = Array.FindIndex(buttons, x => x.name == _button.name);
        selectCloth = i;
    }

    void StartGame()
    {
        //Sorteia uma roupa e coloca no documento para o jogador ver a roupa
        chooseCloth = UnityEngine.Random.Range(0, correctCloths.Length);

        docs.clothParts = new Sprite[4] { null, null, null, null };
        person.clothParts = new Sprite[4] { null, null, null, null };

        regionTxt.text = correctCloths[chooseCloth].regionName.ToString();
        for (int index = 0; index < person.clothParts.Length; index++)
        {
            docs.clothParts[index] = correctCloths[chooseCloth].clothParts[index];
            docImages[index].sprite = docs.clothParts[index];

            buttons[index].image.sprite = person.clothParts[index];
            person.clothParts[index] = correctCloths[chooseCloth].clothParts[index];
        }

        wrongCloth = UnityEngine.Random.Range(0, 4);
        int i = UnityEngine.Random.Range(0, correctCloths.Length);

        if (i == chooseCloth)
            hasWrong = false;
        else
        {
            hasWrong = true;
            person.clothParts[wrongCloth] = correctCloths[i].clothParts[wrongCloth];
            buttons[wrongCloth].image.sprite = person.clothParts[wrongCloth];
        }
    }

    public void RejectEntrance()
    {
        refuse = true;
    }

    public void Accept()
    {
        if (!hasWrong)
        {
            Debug.Log("GG");
            Close();
        }
        else
        {
            if (selectCloth == wrongCloth)
                Debug.Log("Acertou o errado");
            else
                Debug.Log("Errou o errado");
        }
    }

    public void Close()
    {
        GameObject obj = GameObject.Find("caminhao");
        if (obj != null)
        {
            //obj.GetComponent<MeshRenderer>().enabled = true;
            obj.GetComponent<Animator>().speed = 1;
        }

        if (this.gameObject.transform.parent.name == "MinigameCanvas")
            Destroy(this.gameObject);
        else
            Destroy(transform.parent.gameObject);

    }
}

//Cloth Sprite[] 
//* [0] = head
//* [1] = chest
//* [2] = legs
//* [3] = foot 