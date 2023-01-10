using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using EventProperties;

public class CaixaDAguaRodoManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Slider produtoSlider;
    public float produtoDisponivel;
    public MinigamesValuesContainers data;
    public RegionValuesContainer regionData;
    public Region region;
    public float fillSpeed;
    public Text produtoText;
    public GameObject panel;
    public GameObject[] test;
    bool enablePanel;
    bool enable, fiscalCompleted;
    string replace;
    // Start is called before the first frame update
    void Awake()
    {
        produtoDisponivel = data.GetData("Rodoluvio");
        regionData = Resources.Load<RegionValuesContainer>("RegionData");
        produtoSlider.maxValue = regionData.GetMaxHealth(region.ToString());
        produtoSlider.value = regionData.GetHealth(region.ToString());
        fiscalCompleted = false;

    }

    void Start()
    {
        AtualizarTexto(produtoDisponivel);
        enablePanel = false;


    }

    // Update is called once per frame
    void Update()
    {
        if (produtoDisponivel <= 0)
        {
            produtoDisponivel = 0;
            AtualizarTexto(produtoDisponivel);
        }

        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
        {

            if (enable)
            {
                if (produtoSlider.value < produtoSlider.maxValue)
                {
                    produtoDisponivel -= Time.deltaTime * fillSpeed;
                    produtoSlider.value += Time.deltaTime * fillSpeed;
                    regionData.SetHealth(region.ToString(), produtoSlider.value);
                    AtualizarTexto(produtoDisponivel);
                    fiscalCompleted = true;
                }
            }
        }

    }

    public void Fechar()
    {

    }

    public void Close()
    {
        data.SetData("Rodoluvio", produtoDisponivel);
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

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        enable = true;

    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        enable = false;
    }

    void AtualizarTexto(float produto)
    {

        produtoText.text = "Produto disponivel: " + (int)produto + "L";

    }

    public void ComprarProduto(float quantity)
    {
        produtoDisponivel += quantity;
        AtualizarTexto(produtoDisponivel);
        data.SetData("Rodoluvio", produtoDisponivel);
    }

    public void RemoveMoney(float _remove)
    {
        MoneyManager.RemoveMoney("gold", _remove);
    }

    public void EnablePanel()
    {
        if (enablePanel) { Enable(); } else { Disable(); }
        //SetButtonsText();
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

}
