using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using EventProperties;

public class RegionInformationScreen : MonoBehaviour
{
    public Region region;
    RegionValuesContainer regionData;
    RegionDescriptionContainer regionInformationData;
    public TextMeshProUGUI taskName, estamina, time;
    public TextMeshProUGUI description;
    public Slider lifeSlider;
    public Button startTaskButton, closeWindow, teleportButton;
    public Scrollbar scrool;
    public int energy;


    // Start is called before the first frame update
    void Awake()
    {
        regionData = Resources.Load<RegionValuesContainer>("RegionData");
        regionInformationData = Resources.Load<RegionDescriptionContainer>("RegionDescriptionData");

    }

    void Start()
    {
        SetTexts();
        closeWindow.onClick.AddListener(Close);
        startTaskButton.onClick.AddListener(Close);
        teleportButton.onClick.AddListener(GoToMarker);

        scrool.value = 1;
    }

    void Update()
    {
        if (lifeSlider.value != regionData.GetHealth(region.ToString()))
            lifeSlider.value = regionData.GetHealth(region.ToString());
    }

    void SetTexts()
    {
        taskName.text = regionInformationData.GetName(region.ToString());
        description.text = regionInformationData.GetDescrition(region.ToString());
        estamina.text = "Estamina necessária: " + regionInformationData.GetEstamina(region.ToString());
        float t = regionInformationData.GetTimeToComplete(region.ToString())/60;
        time.text = time.text.Replace("99", t.ToString() + " h");
        lifeSlider.maxValue = regionData.GetMaxHealth(region.ToString());
        lifeSlider.value = regionData.GetHealth(region.ToString());
        energy = regionInformationData.GetEstamina(region.ToString());

        //energy = regionInformationData.GetEstamina(region.ToString());
    }

    public void GoToMarker()
    {
        TarefaTrigger.CallEnable3(true);
        GameObject marker = GameObject.Find(region.ToString() + "Marker");
        PlayerController playerController = FindObjectOfType<PlayerController>();
        Vector3 tp = marker.transform.position;
        playerController.enabled = true;
        playerController.Tp(tp);
        TarefaTrigger.CallEnable3(false);
    }

    public void Close()
    {
        TarefaTrigger.CallEnable3(true);
        Destroy(this.gameObject, 0.2f);
    }

    public void CloseWindow(){

        Destroy(this.gameObject);

    }

}
