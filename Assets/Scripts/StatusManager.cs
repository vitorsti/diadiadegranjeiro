using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour
{
    public Text conTxt, subTxt, megaTxt, fiscalTxt;
    public Slider chickenHealthBar;
    public TasksCounterValuesContainer tasksData;
    public LoteValuesContainer loteData;
    public DayValuesContainer dayData;
    //public DayManager dayManager;

    void Awake()
    {
        tasksData = Resources.Load<TasksCounterValuesContainer>("TasksCounterData");
        loteData = Resources.Load<LoteValuesContainer>("LoteData");
        dayData = Resources.Load<DayValuesContainer>("DayData");
        //dayManager = FindObjectOfType<DayManager>();

        chickenHealthBar.wholeNumbers = true;
        chickenHealthBar.interactable = false;

    }

    void Start()
    {
        SetTexts();
    }

    // Update is called once per frame
    void Update()
    {
        Reset();
    }

    public void SetTexts()
    {
        
        conTxt.text = conTxt.text.Replace("0", tasksData.GetTask("Consequence").ToString("00"));
        subTxt.text = subTxt.text.Replace("0", tasksData.GetTask("SubConsequence").ToString("00"));
        megaTxt.text = megaTxt.text.Replace("0", tasksData.GetTask("MegaConsequence").ToString("00"));

        chickenHealthBar.maxValue = loteData.GetMaxValue("lote0");
        chickenHealthBar.value = loteData.GetValue("lote0");

        float f = dayData.GetValue("fiscal arrivel") - dayData.GetValue("fiscal");
        fiscalTxt.text = fiscalTxt.text.Replace("0", f.ToString("00"));

    }

    public void Reset()
    {
        conTxt.text = "Tarefas de consequencia feitas: " + tasksData.GetTask("Consequence").ToString("00");
        subTxt.text = "Tarefas de sub consequencia feitas: " + tasksData.GetTask("SubConsequence").ToString("00");
        megaTxt.text = "Tarefas de mega consequencia feitas: " + tasksData.GetTask("MegaConsequence").ToString("00");

        float f = dayData.GetValue("fiscal arrivel") - dayData.GetValue("fiscal");
        fiscalTxt.text = "Fiscal chega em: " + f.ToString() + " dias.";

        chickenHealthBar.value = loteData.GetValue("lote0");

    }
}
