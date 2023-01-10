using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColocarRação : MonoBehaviour
{
    public Slider progressBar, tubo, slider;
    public GameObject encerrarButton;

    // Start is called before the first frame update
    void Awake()
    {
        encerrarButton.SetActive(false);
        progressBar.gameObject.SetActive(false);
    }
    void Start()
    {
        tubo.value = tubo.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        //slider.value -= tubo.value;

        if (tubo.value == tubo.minValue)
        {
            progressBar.gameObject.SetActive(true);
            progressBar.value += Time.deltaTime;
        }

        if (progressBar.value == progressBar.maxValue)
        {
            tubo.interactable = false;
            encerrarButton.SetActive(true);
            encerrarButton.GetComponent<Button>().onClick.AddListener(Encerrar);
        }
    }

    public void RemoveMoney(float moneyToRemove)
    {
        MoneyManager.RemoveMoney("gold", moneyToRemove);
    }



    public void Encerrar()
    {
        GameObject obj = GameObject.Find("caminhao");
        //obj.GetComponent<MeshRenderer>().enabled = true;
        obj.GetComponent<Animator>().Play("caminhao_2");
        Destroy(this.gameObject);
    }
}
