using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvesNaPropriedadeManager : MonoBehaviour
{
    public GameObject objectToRotate, eventSpawner;
    public Slider slider;
    public RectTransform rT;
    private float previousValue;
    bool start;

    // Start is called before the first frame update
    void Start()
    {
        start = false;
        slider.maxValue = slider.GetComponent<RectTransform>().sizeDelta.x;
        slider.onValueChanged.AddListener(OnSliderChanged);

        slider.value = slider.minValue;

        // And current value
        previousValue = slider.value;
        rT = objectToRotate.GetComponent<RectTransform>();
        rT.sizeDelta = objectToRotate.GetComponent<RectTransform>().sizeDelta;
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value == slider.maxValue)
        {
            start = true;
        }else{
            start = false;
        }

        if (start)
        {
            RegionDescriptionContainer regionDescriptionData;
            regionDescriptionData = Resources.Load<RegionDescriptionContainer>("OtherTasksDescriptionData");
            EnergyManager.RemovEnergy("value", regionDescriptionData.GetEstamina("Aves na propriedade"));
            Destroy(eventSpawner);
            Close();
        }
    }

    void OnSliderChanged(float value)
    {
        // How much we've changed
        float delta = value - previousValue;
        //print(delta);

        rT.sizeDelta = new Vector2(value, rT.sizeDelta.y);
        objectToRotate.GetComponent<RectTransform>().sizeDelta = rT.sizeDelta;

        // Set our previous value for the next change
        previousValue = value;

    }

    public void SetSpawner(GameObject obj)
    {
        Debug.Log("Funfo");
        eventSpawner = obj;
        //eventSpawner = GameObject.Find(eventSpawnerName);
    }

    public void Close()
    {
        TarefaTrigger.CallEnable(true);
        if (this.gameObject.transform.parent.name == "MinigameCanvas")
            Destroy(this.gameObject);
        else
            Destroy(this.gameObject.transform.parent);
        
        //CallAds.ShowAdAfter();
    }
}
