using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoasPraticasPopUpManager : MonoBehaviour
{
    public GameObject boasPraticasScreen, mainCanvas;
    // Start is called before the first frame update
    void Start()
    {
        mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas");
        GetComponent<Button>().onClick.AddListener(OpenScreen);
        boasPraticasScreen = Resources.Load<GameObject>("TecnicalInformationScreen");

    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, 5f);
    }

    public void OpenScreen()
    {
        GameObject go = Instantiate(boasPraticasScreen, mainCanvas.transform.position, mainCanvas.transform.rotation, mainCanvas.transform);
        go.name = boasPraticasScreen.name;

        Destroy(this.gameObject);
    }
}
