using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventProperties;

public class ConsequenceCompostairaManager : MonoBehaviour
{
    public GalinhaMortaBehavior[] galinhas;
    bool start;
    public GameObject eventSpawner;

    // Start is called before the first frame update
    void Start()
    {
        galinhas = FindObjectsOfType<GalinhaMortaBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        galinhas = FindObjectsOfType<GalinhaMortaBehavior>();

        if (galinhas.Length > 0)
        {
            start = true;
        }

        if (start)
        {
            if (galinhas.Length == 0)
            {
                print("tarefa concluida");
                Destroy(eventSpawner);
                start = false;
                RegionDescriptionContainer regionDescriptionData;
                regionDescriptionData = Resources.Load<RegionDescriptionContainer>("OtherTasksDescriptionData");
                EnergyManager.RemovEnergy("value", regionDescriptionData.GetEstamina("Retirar carcaças"));
                InstantiateMinigameCompleteScreenManager.SpawnScreen();
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
            Destroy(transform.parent.gameObject);
        
        InstanatiateBoasPraticasPopUP.SpawBoasPraticas();
    }

    public void SetSpawner(GameObject obj)
    {
        Debug.Log("seting spawner...");
        eventSpawner = obj;
        Debug.Log("spawner seted: " + eventSpawner.name);
        //eventSpawner = GameObject.Find(eventSpawnerName);
    }
}
