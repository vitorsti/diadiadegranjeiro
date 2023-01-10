using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnTarefa : MonoBehaviour
{
    public GameObject prefab, informationScreen;
    public GameObject minigameCanvas, missionPanel, infoTaskPosition;
    public enum selectprefabChildren { canvas, worldSpace, worldSpaceMap, fiscal };
    public selectprefabChildren isPrefabCanvasOrWorldSpace;
    public string missionName;
    public Object script;
    bool tarefaSpawned;
    public bool mega;
    public int energy;
    // Start is called before the first frame update
    void Awake()
    {
        //informationScreen = Resources.Load<GameObject>("OtherTasksDescription");
        tarefaSpawned = false;
    }
    void Start()
    {
        if (mega)
            informationScreen = Resources.Load<GameObject>("MegaTaskDescription");
        else
            informationScreen = Resources.Load<GameObject>("OtherTasksDescription");

        missionPanel = GameObject.FindGameObjectWithTag("MissionPanel");
        minigameCanvas = GameObject.FindGameObjectWithTag("TarefaCanvas");
        infoTaskPosition = GameObject.Find("taskInfoPosition");
        this.gameObject.GetComponent<Button>().onClick.AddListener(SpawnDescription);
        this.gameObject.GetComponentInChildren<Text>().text = missionName;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnDescription()
    {
        TarefaTrigger.CallEnable3(false);

        ConSubMegaInformationScreen screen = FindObjectOfType<ConSubMegaInformationScreen>();
        if (screen != null)
            Destroy(screen.gameObject);

        GameObject go = GameObject.Instantiate(informationScreen, infoTaskPosition.transform.position, transform.rotation * Quaternion.Euler(0, 0, 0), minigameCanvas.transform);
        go.name = informationScreen.name;
        go.GetComponent<ConSubMegaInformationScreen>().SetName(missionName);

        if (!mega)
            go.GetComponent<ConSubMegaInformationScreen>().startTaskButton.onClick.AddListener(Spawn);
        else
        {
            go.GetComponent<ConSubMegaInformationScreen>().startTaskButton.onClick.AddListener(ConSubMegaInformationScreen.CallClose);
            go.GetComponent<ConSubMegaInformationScreen>().startTaskButton.onClick.AddListener(this.gameObject.GetComponent<MegaConsequenceManager>().Resolve);
        }
    }

    public void Spawn()
    {
        EnergyManager energyManager = FindObjectOfType<EnergyManager>();
        ConSubMegaInformationScreen infoScreen = FindObjectOfType<ConSubMegaInformationScreen>();

        energy = infoScreen.energy;
        if (energyManager.slider.value >= energy)
        {
            if (tarefaSpawned)
            {
                return;
            }
            if (isPrefabCanvasOrWorldSpace == selectprefabChildren.canvas)
            {
                GameObject go = GameObject.Instantiate(prefab, minigameCanvas.transform.position, transform.rotation * Quaternion.Euler(0, 0, 0), minigameCanvas.transform);
                go.name = prefab.name;

                CheckIfPrefabContainsScript(go);

                SendMessageToSCripts(go);

                TarefaTrigger.CallEnable(false);
                //tarefaSpawned = true;
            }

            if (isPrefabCanvasOrWorldSpace == selectprefabChildren.worldSpace)
            {
                GameObject go = GameObject.Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
                go.name = prefab.name;

                CheckIfPrefabContainsScript(go);

                SendMessageToSCripts(go);

                ConSubMegaInformationScreen otherTasksDescription;
                otherTasksDescription = FindObjectOfType<ConSubMegaInformationScreen>();

                ConSubMegaInformationScreen.CallClose();

                Destroy(otherTasksDescription.gameObject);


                TarefaTrigger.CallEnable2(false);
                //tarefaSpawned = true;
            }

            if (isPrefabCanvasOrWorldSpace == selectprefabChildren.worldSpaceMap)
            {
                GameObject go = GameObject.Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
                go.name = prefab.name;

                CheckIfPrefabContainsScript(go);

                SendMessageToSCripts(go);
                tarefaSpawned = true;
                TarefaTrigger.CallPass(false);

            }

            if (isPrefabCanvasOrWorldSpace == selectprefabChildren.fiscal)
            {
                GameObject go = GameObject.Instantiate(prefab, minigameCanvas.transform.position, transform.rotation * Quaternion.Euler(0, 0, 0), minigameCanvas.transform);
                go.name = prefab.name;

                CheckIfPrefabContainsScript(go);

                SendMessageToSCripts(go);

                TarefaTrigger.CallEnable3(false);
            }

            //tarefaSpawned = true;
            ConSubMegaInformationScreen.CallClose();
        }
        else
        {
            GameObject noEnergyText = Instantiate(Resources.Load<GameObject>("NoEnergyText"), minigameCanvas.transform.position, minigameCanvas.transform.rotation, minigameCanvas.transform);
            return;
        }
    }

    public void SendMessageToSCripts(GameObject go)
    {
        Transform[] allChildren = go.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            child.SendMessage("SetSpawner", this.gameObject, SendMessageOptions.DontRequireReceiver);
            //break;
        }
    }

    public void CheckIfPrefabContainsScript(GameObject obj)
    {
        if (script != null)
        {
            if (obj.gameObject.GetComponent(script.ToString()) != null)
            {
                obj.GetComponent(script.ToString()).SendMessage("IsFiscal", false, SendMessageOptions.DontRequireReceiver);
            }
        }

    }
}
