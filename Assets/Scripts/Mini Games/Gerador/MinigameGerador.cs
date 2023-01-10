using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using EventProperties;

public class MinigameGerador : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    float moneyToRemove;
    int oilOrGas;
    bool enable, oilOk, gasOk;
    public float regionMaxHealth, regionHealth, fillSpeed, objectives;
    //public string eventSpawnerName;
    public Slider oilSlider;
    public Slider gasSlider;
    public Texture2D oilSprite, gasSprite;
    public RegionValuesContainer regionData;
    public MinigamesValuesContainers geradorData;
    public GameObject oilGasButtons, fiaçãoButton, fiosAmongUs, oilButton, gasButton, eventSpawner;
    public bool isFiscal;
    bool fiscalCompleted;
    public Region region;
    // Start is called before the first frame update
    void Start()
    {
        moneyToRemove = 0;

        LoadGame();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
        {
            if (isFiscal)
                Fiscal();
            else
                Consequence();

        }

    }

    public void LoadGame()
    {
        if (isFiscal == true)
        {
            oilGasButtons.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, oilGasButtons.GetComponent<RectTransform>().anchoredPosition.y);
            fiaçãoButton.SetActive(false);

            regionData = Resources.Load<RegionValuesContainer>("RegionData");

            regionMaxHealth = regionData.GetMaxHealth(region.ToString());
            regionHealth = regionData.GetHealth(region.ToString());
            oilSlider.maxValue = regionMaxHealth / 2;
            gasSlider.maxValue = regionMaxHealth / 2;

            RemoveValueSlider();
        }
        else
        {
            gasOk = false;
            oilOk = false;

            oilGasButtons.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, oilGasButtons.GetComponent<RectTransform>().anchoredPosition.y);
            fiaçãoButton.SetActive(true);
            fiaçãoButton.GetComponent<Button>().onClick.AddListener(CallFiosAmongUs);

            regionData = Resources.Load<RegionValuesContainer>("RegionData");

            regionMaxHealth = regionData.GetMaxHealth(region.ToString());
            regionHealth = regionData.GetHealth(region.ToString());
        }
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        enable = true;

    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        enable = false;
    }

    public void Oil()
    {
        if (oilSlider.value == oilSlider.maxValue)
        {
            print("Oleo cheio");
            return;
        }

        oilOrGas = 1;
        Cursor.SetCursor(oilSprite, Vector2.zero, CursorMode.Auto);
    }
    public void Gas()
    {
        if (gasSlider.value == gasSlider.maxValue)
        {
            print("Gasolina cheio");
            return;
        }

        oilOrGas = 2;
        Cursor.SetCursor(gasSprite, Vector2.zero, CursorMode.Auto);
    }
    public void Cancel()
    {
        oilOrGas = 0;
        enable = false;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
    public void Close()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        int total = (int)oilSlider.value + (int)gasSlider.value;
        regionData.SetHealth(region.ToString(), total);
        TarefaTrigger.CallEnable(true);

        if (fiscalCompleted)
        {
            RegionDescriptionContainer regionDescriptionData;
            regionDescriptionData = Resources.Load<RegionDescriptionContainer>("RegionDescriptionData");
            EnergyManager.RemovEnergy("value", regionDescriptionData.GetEstamina(region.ToString()));
            TimeManager.CalculateDestination(regionDescriptionData.GetTimeToComplete(region.ToString()));
            InstantiateMinigameCompleteScreenManager.SpawnScreen();
        }

        MoneyManager.RemoveMoney("gold", moneyToRemove);

        if (this.gameObject.transform.parent.name == "MinigameCanvas")
            Destroy(this.gameObject);
        else
            Destroy(transform.parent.gameObject);


        InstanatiateBoasPraticasPopUP.SpawBoasPraticas();
        
        //CallAds.ShowAdAfter();
    }
    public void RemoveValueSlider()
    {
        float a = geradorData.GetData("oil");
        float b = geradorData.GetData("gas");

        float soma = a + b;

        if (regionHealth < soma)
        {

            float remove = soma - regionHealth;
            oilSlider.value = a - remove / 2;
            gasSlider.value = b - remove / 2;

            geradorData.SetData("oil", oilSlider.value);
            geradorData.SetData("gas", gasSlider.value);


        }
        else
        {
            oilSlider.value = a;
            gasSlider.value = b;

            geradorData.SetData("oil", oilSlider.value);
            geradorData.SetData("gas", gasSlider.value);
        }


    }
    public void Fiscal()
    {
        if (enable)
        {
            if (oilOrGas == 1)
            {
                if (oilSlider.value != oilSlider.maxValue)
                {
                    oilSlider.value += Time.deltaTime * fillSpeed;
                    geradorData.SetData("oil", oilSlider.value);
                    float soma = oilSlider.value + gasSlider.value;
                    regionData.SetHealth(region.ToString(), soma);
                    fiscalCompleted = true;
                    moneyToRemove += Time.deltaTime;
                }
                else
                {
                    print("oleo cheio");
                }
            }

            if (oilOrGas == 2)
            {
                if (gasSlider.value != gasSlider.maxValue)
                {
                    gasSlider.value += Time.deltaTime * fillSpeed;
                    geradorData.SetData("gas", gasSlider.value);
                    float soma = oilSlider.value + gasSlider.value;
                    regionData.SetHealth(region.ToString(), soma);
                    fiscalCompleted = true;
                    moneyToRemove += Time.deltaTime;
                }
                else
                {
                    print("gasolina cheia");
                }
            }
        }
    }

    public void Consequence()
    {

        if (enable)
        {
            if (oilOrGas == 1)
            {
                if (!oilOk)
                {
                    if (oilSlider.value != oilSlider.maxValue && !oilOk)
                    {
                        oilSlider.value += Time.deltaTime * fillSpeed;
                        moneyToRemove += Time.deltaTime;

                    }
                    else
                    {
                        oilOk = true;
                        AddCompletedeConsequenceObjectives();
                        DisableButton(oilButton.name);
                        print("Oleo cheio");
                        return;
                    }
                }

            }

            if (oilOrGas == 2)
            {
                if (!gasOk)
                {
                    if (gasSlider.value != gasSlider.maxValue)
                    {
                        gasSlider.value += Time.deltaTime * fillSpeed;
                        moneyToRemove += Time.deltaTime;

                    }
                    else
                    {
                        gasOk = true;
                        AddCompletedeConsequenceObjectives();
                        DisableButton(gasButton.name);
                        print("Gasolina cheio");
                        return;
                    }
                }

            }

        }
    }

    public void CallFiosAmongUs()
    {
        print("Called");
        GameObject go = Resources.Load<GameObject>("MinigamesConsequence/Gerador/MinigameFiosAmongUs");
        Instantiate(go, transform.position, transform.rotation * Quaternion.Euler(0, 0, 0), transform.parent);
        print(go.transform.position);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    // Função de vitoria do minigame consequencia
    public void AddCompletedeConsequenceObjectives()
    {
        objectives++;

        if (objectives == 3)
        {
            RegionDescriptionContainer regionDescriptionData;
            regionDescriptionData = Resources.Load<RegionDescriptionContainer>("OtherTasksDescriptionData");
            EnergyManager.RemovEnergy("value", regionDescriptionData.GetEstamina("Queda de energia"));
            TimeManager.CalculateDestination(regionDescriptionData.GetTimeToComplete("Queda de energia"));
            InstantiateMinigameCompleteScreenManager.SpawnScreen();
            Debug.Log("Gerador restaurado");
            Destroy(eventSpawner);
            Destroy(this.gameObject);
        }
    }

    public void DisableButton(string objName)
    {
        GameObject go = GameObject.Find(objName);
        go.GetComponent<Button>().interactable = false;
        go.GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
    }

    public void IsFiscal(bool _isFiscal)
    {
        isFiscal = _isFiscal;
    }
    public void SetSpawner(GameObject obj)
    {
        Debug.Log("Funfo");
        eventSpawner = obj;
        //eventSpawner = GameObject.Find(eventSpawnerName);
    }
}