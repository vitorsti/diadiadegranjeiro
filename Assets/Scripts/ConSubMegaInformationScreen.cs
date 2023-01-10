using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConSubMegaInformationScreen : MonoBehaviour
{
    public RegionDescriptionContainer regionInformationData;
    public string getName;
    //public Text ;
    public TextMeshProUGUI description, taskName, estamina, time;
    public Button startTaskButton, closeWindow;
    public Scrollbar scrool;
    public int energy;

    // Start is called before the first frame update
    void Awake()
    {
        regionInformationData = Resources.Load<RegionDescriptionContainer>("OtherTasksDescriptionData");
    }
    void Start()
    {
        SetTexts();
        closeWindow.onClick.AddListener(Close);

        scrool.value = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetTexts()
    {
        taskName.text = regionInformationData.GetName(getName);
        description.text = regionInformationData.GetDescrition(getName);
        estamina.text = "Estamina necessária: " + regionInformationData.GetEstamina(getName);
        float t = regionInformationData.GetTimeToComplete(getName)/60;
        time.text = time.text.Replace("99", t.ToString() + " h");
        energy = regionInformationData.GetEstamina(getName);
        //lifeSlider.maxValue = regionData.GetMaxHealth(region.ToString());
        //lifeSlider.value = regionData.GetHealth(region.ToString());
    }

    public void SetName(string name)
    {
        getName = name;
    }

    public static void CallClose()
    {
        ConSubMegaInformationScreen othersTaksScrren;
        othersTaksScrren = FindObjectOfType<ConSubMegaInformationScreen>();
        if (othersTaksScrren != null)
            othersTaksScrren.Close();
    }

    public void Close()
    {
        TarefaTrigger.CallEnable(true);
        TarefaTrigger.CallEnable2(true);
        TarefaTrigger.CallEnable3(true);
        Destroy(this.gameObject, 0.2f);
    }
}
