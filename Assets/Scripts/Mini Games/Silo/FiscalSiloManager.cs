using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventProperties;
using UnityEngine.UI;

public class FiscalSiloManager : MonoBehaviour
{
    public Region region;
    public RegionValuesContainer regionData;
    public float regioMaxHealth, regionHealth, gap, div;
    public Slider healthBarSlider;
    public GameObject healthBar;
    public List<GameObject> racaoAcomulados;
    public GameObject racaoPrefab;
    public RectTransform tA, tB, tC, panel;
    public bool startGame;
    public Sprite[] racaoSprites;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GameObject.Find("healthBarSlider");
        healthBarSlider = healthBar.GetComponent<Slider>();
        healthBarSlider.interactable = false;

        startGame = false;

        regionData = Resources.Load<RegionValuesContainer>("RegionData");
        regioMaxHealth = regionData.GetMaxHealth(region.ToString());
        regionHealth = regionData.GetHealth(region.ToString());
        gap = regionData.GetHealthModfy(region.ToString());

        healthBarSlider.maxValue = regioMaxHealth;
        healthBarSlider.value = regionHealth;

        CheckHealth();
    }

    // Update is called once per frame
    void Update()
    {

        if (startGame)
        {
            if (racaoAcomulados.Count > 0)
            {
                for (int i = 0; i < racaoAcomulados.Count; i++)
                {
                    if (racaoAcomulados[i] == null)
                    {
                        racaoAcomulados.RemoveAt(i);
                        regionHealth += gap;
                        healthBarSlider.value = regionHealth;

                    }
                }
            }
            else
            {
                RegionDescriptionContainer regionDescriptionData;
                regionDescriptionData = Resources.Load<RegionDescriptionContainer>("RegionDescriptionData");
                EnergyManager.RemovEnergy("value", regionDescriptionData.GetEstamina(region.ToString()));
                TimeManager.CalculateDestination(regionDescriptionData.GetTimeToComplete(region.ToString()));
                InstantiateMinigameCompleteScreenManager.SpawnScreen();
                print("Tarefa Concluida");
                Close();
            }
        }
    }

    public void Close()
    {
        TarefaTrigger.CallEnable(true);
        if (this.gameObject.transform.parent.name == "MinigameCanvas")
            Destroy(this.gameObject);
        else
            Destroy(this.gameObject.transform.parent);

        regionData.SetHealth(region.ToString(), regionHealth);

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

            if (div > 0)
            {
                do
                {
                    SpawnRacaoPresa();
                } while (div != 0);
            }
        }
    }

    public void SpawnRacaoPresa()
    {
        Vector3 position = new Vector3(Random.Range(tA.position.x, tB.position.x), 0,Random.Range(tC.position.z, tA.position.z));
        GameObject go = Instantiate(racaoPrefab, position, panel.rotation, panel.transform);
        go.GetComponent<Image>().sprite = racaoSprites[Random.Range(0, racaoSprites.Length)];
        go.name += " " + Random.Range(0, 100);

        racaoAcomulados.Add(go);

        // if (Physics2D.OverlapCircle((Vector2)position, go.GetComponent<CircleCollider2D>().radius).name == go.name)
        // {
        //     print(go.name);
        // }

        startGame = true;

        div--;

    }

}
