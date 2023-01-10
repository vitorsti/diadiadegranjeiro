using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnergyManager : MonoBehaviour
{
    public Slider slider;
    
    TextMeshProUGUI text;
    public Text energyCheckText;
    public static bool pass;
    public GameObject checkScreen;
    EnergyValuesContainer energyData;
    GameEventManager gameEvent;
    public string s;

    void Awake()
    {
        energyData = Resources.Load<EnergyValuesContainer>("EnergyData");
        slider = GetComponent<Slider>();
        text = slider.GetComponentInChildren<TextMeshProUGUI>();
        gameEvent = FindObjectOfType<GameEventManager>();
        pass = false;
        checkScreen.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = energyData.GetValue("maxValue");

        if (energyData.GetValue("value") != 0)
        {
            slider.value = energyData.GetValue("value");
        }
        else
            slider.value = slider.maxValue;

        SetText();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddEnergy("value", 10);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RemovEnergy("value", 10);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            ResetEnergy();
        }

#endif
        slider.maxValue = energyData.GetValue("maxValue");
        slider.value = energyData.GetValue("value");
        SetText();
    }

    public static void AddEnergy(string name, int amount)
    {
        EnergyValuesContainer energyData;
        energyData = Resources.Load<EnergyValuesContainer>("EnergyData");

        if (name == "value")
        {
            if (energyData.GetValue(name) + amount > energyData.GetValue("maxValue"))
            {
                print("excedeu o limite");
                return;
            }
            else
                energyData.AddQtty(name, amount);
        }
        else
            energyData.AddQtty(name, amount);

    }

    public static void RemovEnergy(string name, int amount)
    {
        EnergyValuesContainer energyData;
        energyData = Resources.Load<EnergyValuesContainer>("EnergyData");


        energyData.RemoveQtty(name, amount);

    }

    public static void ResetEnergy()
    {
        EnergyValuesContainer energyData;
        energyData = Resources.Load<EnergyValuesContainer>("EnergyData");

        energyData.SetValue("value", energyData.GetValue("maxValue"));
    }

    public void EnergyCheck()
    {

        EnergyValuesContainer energyData;
        energyData = Resources.Load<EnergyValuesContainer>("EnergyData");

        if (energyData.GetValue("value") <= 0)
        {
            pass = true;
        }
        else if (energyData.GetValue("value") > 0)
        {
            checkScreen.SetActive(true);
            energyCheckText.text = s.Replace("!value", energyData.GetValue("value").ToString());
        }

    }

    public void Yes()
    {
        pass = true;
        gameEvent.NextDay();
        checkScreen.SetActive(false);
        pass = false;
        //energyCheckText.text = energyCheckText.text.Replace(energyData.GetValue("value").ToString(), "!value");
    }

    public void No()
    {
        pass = false;
        checkScreen.SetActive(false);
        //energyCheckText.text = energyCheckText.text.Replace(energyData.GetValue("value").ToString(), "!value");
    }

    public static void CallEnergyCheck()
    {
        EnergyManager energyManager;
        energyManager = FindObjectOfType<EnergyManager>();
        energyManager.EnergyCheck();
    }

    public void SetText()
    {
        text.text = slider.value.ToString() + " / " + slider.maxValue.ToString();
    }
}
