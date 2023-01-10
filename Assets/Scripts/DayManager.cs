using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    [SerializeField]
    private int day, fiscal, fiscalArriveEvery;
    
    [SerializeField]
    public bool fiscalDay;
    DayValuesContainer dayData;
    GameObject fiscalScrren;
    public GameObject content;
    // Start is called before the first frame update
    void Awake()
    {
        dayData = Resources.Load<DayValuesContainer>("DayData");
        fiscalScrren = Resources.Load<GameObject>("FiscalScreen");
        content = GameObject.FindGameObjectWithTag("TarefaCanvas");
    }

    void Start()
    {
        day = dayData.GetValue("day");
        fiscal = dayData.GetValue("fiscal");
        fiscalArriveEvery = dayData.GetValue("fiscal arrivel");

        if (fiscal == fiscalArriveEvery)
        {
            dayData.SetValue("fiscal", 0);
            fiscalDay = true;

        }
        else
            fiscalDay = false;
    }

    // Update is called once per frame
    void Update()
    {
        day = dayData.GetValue("day");
        fiscal = dayData.GetValue("fiscal");

        if (fiscal == fiscalArriveEvery)
        {
            fiscalDay = true;

        }
        else
            fiscalDay = false;
    }

    public void Fiscal()
    {
        print("Dia do fiscal");
        GameObject go = GameObject.Instantiate(fiscalScrren, content.transform.position, content.transform.rotation, content.transform);
        go.name = fiscalScrren.name;
        
    }

    public void Checkfiscal()
    {
        float f = fiscal + 1;
        if (f == fiscalArriveEvery)
        {
            fiscalDay = true;
            Fiscal();

        }
        else
            fiscalDay = false;
    }
}
