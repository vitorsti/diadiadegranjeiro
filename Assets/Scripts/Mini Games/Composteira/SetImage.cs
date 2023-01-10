using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventProperties;

public class SetImage : MonoBehaviour
{
    public Image[] camaras;
    public Sprite cheio, vazio;
    public Slider slider;
    public float[] floatIds;
    public float previousRegionHealth, regionHealth, sliderValue, regioMaxHealth;
    public MinigamesValuesContainers composteiraData;
    public MinigamesValuesContainers sliderData;
    public RegionValuesContainer regionData;
    public Region region;

    void Awake()
    {
        regionData = Resources.Load<RegionValuesContainer>("RegionData");
        sliderValue = sliderData.GetData("SliderValue");
        previousRegionHealth = sliderData.GetData("PreviousRegionHelth");
        regionHealth = regionData.GetHealth(region.ToString());
        slider.maxValue = regionData.GetMaxHealth(region.ToString());
        regioMaxHealth = regionData.GetMaxHealth(region.ToString());
        floatIds = new float[composteiraData.datas.Length];

        for (int i = 0; i < floatIds.Length; i++)
        {
            floatIds[i] = composteiraData.datas[i].value;
        }

        setSliderValue();

    }
    void Start()
    {
        for (int i = 0; i < camaras.Length; i++)
        {
            if (floatIds[i] == 0)
            {
                //camaras[i].color = Color.green;
                camaras[i].GetComponent<Image>().sprite = vazio;
            }
            else if (floatIds[i] == 1)
            {
                //camaras[i].color = Color.red;
                camaras[i].GetComponent<Image>().sprite = cheio;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetImageColor()
    {

        for (int i = 0; i < floatIds.Length; i++)
        {
            floatIds[i] = composteiraData.datas[i].value;
        }

        for (int i = 0; i < camaras.Length; i++)
        {
            if (floatIds[i] == 0)
            {
                //camaras[i].color = Color.green;
                camaras[i].GetComponent<Image>().sprite = vazio;
            }
            else if (floatIds[i] == 1)
            {
                //camaras[i].color = Color.red;
                camaras[i].GetComponent<Image>().sprite = cheio;
            }
        }
    }

    public void setSliderValue()
    {
        if (previousRegionHealth > regionHealth)
        {
            float dif = previousRegionHealth - regionHealth;
            sliderValue += dif;

            slider.value = sliderValue;
            previousRegionHealth = regionHealth;

            sliderData.SetData("SliderValue", sliderValue);
            sliderData.SetData("PreviousRegionHelth", previousRegionHealth);

        }
        if (regionHealth > previousRegionHealth)
        {
            float dif = regionHealth - previousRegionHealth;
            sliderValue -= dif;

            slider.value = sliderValue;
            previousRegionHealth = regionHealth;

            sliderData.SetData("SliderValue", sliderValue);
            sliderData.SetData("PreviousRegionHelth", previousRegionHealth);
        }
        if (previousRegionHealth == regionHealth)
        {
            slider.value = sliderValue;
        }

    }

    public void Close()
    {
        TarefaTrigger.CallEnable(true);
        if (this.gameObject.transform.parent.name == "MinigameCanvas")
            Destroy(this.gameObject);
        else
            Destroy(this.gameObject.transform.parent);

        InstanatiateBoasPraticasPopUP.SpawBoasPraticas();
        //CallAds.ShowAdAfter();
    }
}
