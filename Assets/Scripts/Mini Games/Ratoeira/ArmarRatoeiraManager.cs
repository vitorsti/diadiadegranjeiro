using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventProperties;

public class ArmarRatoeiraManager : MonoBehaviour
{
    [SerializeField]
    float moneyToRemove;
    // Assign in the inspector
    public GameObject objectToRotate;
    public Slider slider;
    public Text ratoreirasText;
    public Region region;
    public RegionValuesContainer regionData;
    RegionDescriptionContainer regionInformationData;
    public Sprite[] fundos;
    public float regioMaxHealth, regionHealth, gap, div;
    public int ratoeirasDesarmadas;
    public int energyToRemove;
    public float timer, resetTimer;
    bool test;
    public bool enable;
    [Header("Coloque uma quantidade a mais da quantidade total")]
    //public int ratoeirasTotais;

    // Preserve the original and current orientation
    private float previousValue;

    void Awake()
    {
        GetComponent<Image>().sprite = fundos[Random.Range(0, fundos.Length)];

        regionData = Resources.Load<RegionValuesContainer>("RegionData");
        regionInformationData = Resources.Load<RegionDescriptionContainer>("RegionDescriptionData");
        regioMaxHealth = regionData.GetMaxHealth(region.ToString());
        regionHealth = regionData.GetHealth(region.ToString());
        gap = regionData.GetHealthModfy(region.ToString());

        timer = 1f;
        // Assign a callback for when  slider changes
        slider.onValueChanged.AddListener(OnSliderChanged);

        // And current value
        previousValue = slider.value;

        //ratoeirasDesarmadas = Random.Range(0, ratoeirasTotais);
        CheckHealth();

        resetTimer = timer;
    }

    void Start()
    {
        string i = ratoeirasDesarmadas.ToString();

        ratoreirasText.text = ratoreirasText.text.Replace("int", i);


    }
    void Update()
    {

        if (regionHealth != regioMaxHealth)
        {

            if (ratoeirasDesarmadas <= 0)
            {
                ratoeirasDesarmadas = 0;
                regionData.SetHealth(region.ToString(), regioMaxHealth);
                EnergyManager.RemovEnergy("value", energyToRemove);
                print("Todas as ratoeiras foram armadas");
                InstantiateMinigameCompleteScreenManager.SpawnScreen();
                Close();
            }


            if (slider.value == slider.maxValue)
            {
                slider.interactable = false;
                slider.value = slider.minValue;
                int i = ratoeirasDesarmadas;
                ratoeirasDesarmadas -= 1;
                ratoreirasText.text = ratoreirasText.text.Replace(i.ToString(), ratoeirasDesarmadas.ToString());
                test = true;
                GetComponent<Image>().sprite = fundos[Random.Range(0, fundos.Length)];
                MoneyManager.RemoveMoney("gold", moneyToRemove);
                TimeManager.CalculateDestination(regionInformationData.GetEstamina(region.ToString()));
            }

            if (test == true)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    timer = resetTimer;
                    test = false;
                    slider.interactable = true;
                }
            }

            if (enable)
            {
                slider.value -= Time.deltaTime * 10;
            }
        }
        else
        {
            slider.interactable = false;
        }

    }

    void OnSliderChanged(float value)
    {
        // How much we've changed
        float delta = value - previousValue;
        objectToRotate.transform.Rotate(new Vector3(0, 0, 1) * delta * 180);

        // Set our previous value for the next change
        previousValue = value;

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

    public void CheckHealth()
    {
        if (regioMaxHealth != regionHealth)
        {
            float dif = regioMaxHealth - regionHealth;
            print(dif);
            div = dif / gap;
            print(div);

            ratoeirasDesarmadas = (int)div;
        }
    }

    public void SetEnergy(string value)
    {
        energyToRemove = regionInformationData.GetEstamina(value);
        print("gar");
    }

}
