using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using EventProperties;

public class FiscaliMedirPH : MonoBehaviour
{
    public Slider phSlider;
    public InputField inputField;
    public Text phText, tarefaTextDescription;
    public GameObject agua, aguaTarefa, sujeiras, medirPhButton, eventSpawner, tarefasText, position;
    public Button colocarCloroButton;
    public Region region;
    public RegionValuesContainer regionData;
    public bool isFiscal, enable;
    public int tarefas;
    bool fiscalCompleted;
    // Start is called before the first frame update
    void Awake()
    {
        // phText.text = " ";
        regionData = Resources.Load<RegionValuesContainer>("RegionData");
        fiscalCompleted = false;
        //phSlider.maxValue = regionData.GetMaxHealth(region.ToString());

    }

    void Start()
    {
        LoadGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadGame()
    {
        if (isFiscal)
        {
            agua.SetActive(true);
            medirPhButton.SetActive(true);

            aguaTarefa.SetActive(false);
            sujeiras.SetActive(false);
            tarefasText.SetActive(false);

            phText.text = " ";
            phSlider.maxValue = regionData.GetMaxHealth(region.ToString());

            colocarCloroButton.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(180, colocarCloroButton.gameObject.GetComponent<RectTransform>().sizeDelta.y);
            colocarCloroButton.GetComponentInChildren<Text>().text = "Colocar cloro.";

        }
        else
        {
            agua.SetActive(false);
            medirPhButton.SetActive(false);

            aguaTarefa.SetActive(false);
            sujeiras.SetActive(false);
            tarefasText.SetActive(true);

            phText.text = " ";
            phSlider.maxValue = regionData.GetMaxHealth(region.ToString());

            //colocarCloroButton.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, colocarCloroButton.gameObject.GetComponent<RectTransform>().position.y);
            colocarCloroButton.transform.position = position.transform.position;
            colocarCloroButton.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(200, colocarCloroButton.gameObject.GetComponent<RectTransform>().sizeDelta.y);
            colocarCloroButton.GetComponentInChildren<Text>().text = "Colocar água e aplicar cloro.";

            colocarCloroButton.gameObject.SetActive(false);

            NextTarefa(aguaTarefa, " - Retire a água suja");
        }
    }

    public void MedirPH()
    {

        phSlider.value = regionData.GetHealth(region.ToString());
        phText.text = phSlider.value.ToString("00");


    }

    public void Close()
    {
        TarefaTrigger.CallEnable(true);
        if (this.gameObject.transform.parent.name == "MinigameCanvas")
            Destroy(this.gameObject);
        else
            Destroy(transform.parent.gameObject);

        if (fiscalCompleted)
        {
            RegionDescriptionContainer regionDescriptionData;
            regionDescriptionData = Resources.Load<RegionDescriptionContainer>("RegionDescriptionData");
            EnergyManager.RemovEnergy("value", regionDescriptionData.GetEstamina(region.ToString()));
            TimeManager.CalculateDestination(regionDescriptionData.GetTimeToComplete(region.ToString()));
            InstantiateMinigameCompleteScreenManager.SpawnScreen();
        }

        InstanatiateBoasPraticasPopUP.SpawBoasPraticas();
        //CallAds.ShowAdAfter();
    }

    public void ColocarCloro()
    {
        if (isFiscal)
        {
            float soma = phSlider.value + float.Parse(inputField.text);
            phSlider.value = soma;
            if (soma >= phSlider.maxValue)
            {
                soma = phSlider.maxValue;
                regionData.SetHealth(region.ToString(), soma);
                phText.text = soma.ToString("00");
            }
            else
            {
                regionData.SetHealth(region.ToString(), soma);
                phText.text = soma.ToString("00");
            }

            fiscalCompleted = true;
        }
        else
        {
            float soma = phSlider.value + float.Parse(inputField.text);
            phSlider.value = soma;
            if (soma >= phSlider.maxValue)
            {
                soma = phSlider.maxValue;
                regionData.SetHealth(region.ToString(), soma);
                phText.text = soma.ToString("00");
            }
            else
            {
                regionData.SetHealth(region.ToString(), soma);
                phText.text = soma.ToString("00");
            }

            agua.SetActive(true);
            MoneyManager.RemoveMoney("gold", float.Parse(inputField.text));
            AddCompletedTarefa();

        }
    }

    public void AddCompletedTarefa()
    {
        tarefas++;

        if (tarefas >= 3)
        {
            RegionDescriptionContainer regionDescriptionData;
            regionDescriptionData = Resources.Load<RegionDescriptionContainer>("OtherTasksDescriptionData");
            EnergyManager.RemovEnergy("value", regionDescriptionData.GetEstamina("Água poluida"));
            TimeManager.CalculateDestination(regionDescriptionData.GetTimeToComplete("Água poluida"));
            InstantiateMinigameCompleteScreenManager.SpawnScreen();

            Destroy(eventSpawner);
            Destroy(this.gameObject);
        }
    }

    public void NextTarefa(GameObject obj, string tarefaDescription)
    {

        obj.SetActive(true);
        tarefaTextDescription.text = tarefaDescription;
    }

    public void SetSpawner(GameObject obj)
    {
        Debug.Log("Funfo");
        eventSpawner = obj;
        //eventSpawner = GameObject.Find(eventSpawnerName);
    }

    public void IsFiscal(bool _isFiscal)
    {
        isFiscal = _isFiscal;
    }


}
