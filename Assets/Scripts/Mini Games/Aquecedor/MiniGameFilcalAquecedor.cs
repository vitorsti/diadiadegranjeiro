using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using EventProperties;

public class MiniGameFilcalAquecedor : MonoBehaviour
{
    public Sprite bkgF, bkgC;
    public Text tarefaTextDescription;
    public Slider medidorDeLenhaSlider, matchSlider;
    bool enablePanel, sliderDirection;
    public bool isFiscal;
    public float matchSliderMaxValue, sliderFillMultiplier, tarefas;
    public GameObject panel, eventSpawner, strikeMatch, fire, compraMadeiraButton, guia;
    public Region region;
    public RegionValuesContainer regionData;
    bool fiscalCompleted;
    // Start is called before the first frame update
    void Awake()
    {
        fiscalCompleted = false;
        regionData = Resources.Load<RegionValuesContainer>("RegionData");
        medidorDeLenhaSlider.maxValue = regionData.GetMaxHealth(region.ToString());
        medidorDeLenhaSlider.value = regionData.GetHealth(region.ToString());

    }

    void Start()
    {
        LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFiscal)
        {
            if (sliderDirection)
            {
                matchSlider.value += Time.deltaTime * sliderFillMultiplier;
                if (matchSlider.value == matchSlider.maxValue)
                    sliderDirection = false;
            }

            if (!sliderDirection)
            {
                matchSlider.value -= Time.deltaTime * sliderFillMultiplier;
                if (matchSlider.value == matchSlider.minValue)
                    sliderDirection = true;
            }
        }

    }

    public void ComprarLenha(int moneyToRemove)
    {
        if (medidorDeLenhaSlider.value == medidorDeLenhaSlider.maxValue)
            return;

        if (isFiscal)
        {
            medidorDeLenhaSlider.value = medidorDeLenhaSlider.maxValue;
            regionData.SetHealth(region.ToString(), medidorDeLenhaSlider.value);
            fiscalCompleted = true;

        }
        else
        {

            NextTarefa(strikeMatch, "  - Acenda o fogo.");
            compraMadeiraButton.GetComponent<Button>().enabled = false;
            AddCompletedTarefa();
            medidorDeLenhaSlider.value = medidorDeLenhaSlider.maxValue;
            Enable();

        }

        MoneyManager.RemoveMoney("gold", moneyToRemove);

    }

    public void LenhaToRemove(int lenhaToRemove)
    {
        medidorDeLenhaSlider.value -= lenhaToRemove;
        regionData.SetHealth(region.ToString(), medidorDeLenhaSlider.value);
    }

    public void setLenha(int id)
    {
        regionData.SetHealthModfy(region.ToString(), (float)id);
    }

    public void EnablePanel()
    {
        if (enablePanel) { Enable(); } else { Disable(); }
    }

    public void Disable()
    {
        panel.SetActive(true);
        enablePanel = true;
    }

    public void Enable()
    {
        panel.SetActive(false);
        enablePanel = false;
    }

    public void SetSpawner(GameObject obj)
    {
        Debug.Log("Funfo");
        eventSpawner = obj;
        //eventSpawner = GameObject.Find(eventSpawnerName);
    }

    public void AddCompletedTarefa()
    {
        tarefas++;

        if (tarefas >= 2)
        {
            Destroy(eventSpawner);
            RegionDescriptionContainer regionDescriptionData;
            regionDescriptionData = Resources.Load<RegionDescriptionContainer>("OtherTasksDescriptionData");
            EnergyManager.RemovEnergy("value", regionDescriptionData.GetEstamina("Falta de lenha"));
            TimeManager.CalculateDestination(regionDescriptionData.GetTimeToComplete("Falta de lenha"));
            InstantiateMinigameCompleteScreenManager.SpawnScreen();
            Close();
        }
    }

    public void LoadGame()
    {
        if (isFiscal)
        {
            GetComponent<Image>().sprite = bkgF;
            strikeMatch.SetActive(false);
            guia.SetActive(false);
            //tarefaDescription.GetComponent<Text>().text = " ";
            fire.SetActive(true);
            return;
        }
        else
        {
            GetComponent<Image>().sprite = bkgC;
            strikeMatch.SetActive(false);
            guia.SetActive(true);
            fire.SetActive(false);
            NextTarefa(this.gameObject, "  - Compre madeira.");
            matchSlider.maxValue = matchSliderMaxValue;

            float r = UnityEngine.Random.Range(matchSlider.minValue, matchSlider.maxValue + 1.0f);

            matchSlider.value = r;

            int random = UnityEngine.Random.Range(0, 2);

            if (random == 0)
                sliderDirection = true;
            else
                sliderDirection = false;
        }
    }

    public void IsFiscal(bool _isFiscal)
    {
        isFiscal = _isFiscal;
    }

    public void NextTarefa(GameObject obj, string tarefaDescription)
    {

        obj.SetActive(true);
        tarefaTextDescription.text = tarefaDescription;

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
            regionData.SetHealth(region.ToString(), regionData.GetHealth(region.ToString()) + regionData.GetHealthModfy(region.ToString()));
            InstantiateMinigameCompleteScreenManager.SpawnScreen();
            //InstantiateMinigameCompleteScreenManager.SpawnScreen();

        }

        InstanatiateBoasPraticasPopUP.SpawBoasPraticas();
        //CallAds.ShowAdAfter();

        //regionDescription = Resources.Load<RegionDescriptionContainer>("RegionDescriptionData");
    }
}
