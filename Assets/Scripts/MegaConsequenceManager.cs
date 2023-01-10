using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaConsequenceManager : MonoBehaviour
{
    public GameObject spawner, nextDayButton;
    LoteValuesContainer loteData;
    // Start is called before the first frame update
    void Awake()
    {
        loteData = Resources.Load<LoteValuesContainer>("LoteData");
        nextDayButton = GameObject.Find("NextDayyButton");
        spawner = this.gameObject;
    }

    void Start()
    {
        nextDayButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(RemoveQtty);
    }

    public void RemoveQtty()
    {
        loteData.RemoveQtty("lote0", loteData.GetHealthRemove("lote0"));
    }

    public void Resolve()
    {
        ConSubMegaInformationScreen informationScreen;
        informationScreen = FindObjectOfType<ConSubMegaInformationScreen>();

        MoneyManager.RemoveMoney("cash", loteData.GetCure("lote0"));
        EnergyManager.RemovEnergy("value", informationScreen.regionInformationData.GetEstamina(informationScreen.getName));
        nextDayButton.GetComponent<UnityEngine.UI.Button>().onClick.RemoveListener(RemoveQtty);
        loteData.SetValue("lote0", loteData.GetMaxValue("lote0"));
        Destroy(spawner);
    }
}
