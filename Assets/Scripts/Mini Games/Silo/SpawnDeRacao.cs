using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventProperties;

public class SpawnDeRacao : MonoBehaviour
{
    public GameObject prefab, minigameScreen, eventSpawner;
    public Transform posiA, posiB;
    //public string eventSpawnerName;
    public List<GameObject> ob = new List<GameObject>();
    public Sprite[] racaoSprites;
    public Text vidasText;
    public Text timerText;
    public float timer;
    public int vidas;

    public int min, max;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRacao", 1f, 1f);
        minigameScreen = this.transform.parent.gameObject;

        #if UNITY_EDITOR
        timer = 10000;
        #endif


    }

    // Update is called once per frame
    void Update()
    {

        timer -= Time.deltaTime;
        timerText.text = "Tempo: " + (int)timer;

        //condição de vitoria
        if (timer <= 0)
        {
            timer = 0;
            CancelInvoke("SpawnRacao");
            foreach (GameObject i in ob)
            {
                Destroy(i);
            }
            print("Tarefa concluida!");
            RegionDescriptionContainer regionDescriptionData;
            regionDescriptionData = Resources.Load<RegionDescriptionContainer>("OtherTasksDescriptionData");
            EnergyManager.RemovEnergy("value", regionDescriptionData.GetEstamina(eventSpawner.GetComponent<SpawnTarefa>().missionName));
            TimeManager.CalculateDestination(regionDescriptionData.GetTimeToComplete(eventSpawner.GetComponent<SpawnTarefa>().missionName));
            InstantiateMinigameCompleteScreenManager.SpawnScreen();
            Destroy(eventSpawner);
            Close();
        }

        vidasText.text = "Vidas: " + vidas;

        //condição de derrota
        if (vidas <= 0)
        {
            vidas = 0;
            CancelInvoke("SpawnRacao");
            foreach (GameObject i in ob)
            {
                Destroy(i);
            }
            RegionDescriptionContainer regionDescriptionData;
            regionDescriptionData = Resources.Load<RegionDescriptionContainer>("OtherTasksDescriptionData");
            EnergyManager.RemovEnergy("value", regionDescriptionData.GetEstamina(eventSpawner.GetComponent<SpawnTarefa>().missionName));
            print("Perdeu, tente novamente!");
            Close();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "racao")
        {
            Destroy(other.gameObject);
            vidas--;
        }
    }
    public void SpawnRacao()
    {

        GameObject go = Instantiate(prefab, new Vector3(Random.Range(posiA.position.x, posiB.position.x), transform.position.y, transform.position.z), transform.rotation, transform);
        //go.transform.SetParent(this.gameObject.transform, true);
        go.GetComponent<Image>().sprite = racaoSprites[Random.Range(0, racaoSprites.Length)];
        go.name = "racao";
        ob.Add(go);

    }

    public void Close()
    {
        TarefaTrigger.CallEnable(true);
        if (this.gameObject.transform.parent.name == "MinigameCanvas")
            Destroy(this.gameObject);
        else
            Destroy(transform.parent.gameObject);

        InstanatiateBoasPraticasPopUP.SpawBoasPraticas();
        //CallAds.ShowAdAfter();
    }

    public void SetSpawner(GameObject obj)
    {
        Debug.Log("Funfo");
        eventSpawner = obj;
        //eventSpawner = GameObject.Find(eventSpawnerName);
    }
}
