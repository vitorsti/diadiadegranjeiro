using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
///<summary>
/// This script is used to spawn the minigames for the daily tasks
///</summary>

public class TarefaTrigger : MonoBehaviour
{
    [Header("Coloque o nome pasta e do minigame dentro da pasta 'Resources', exemplo: FolderName/MinigameName")]
    public string path;
    [SerializeField]
    private GameObject prefab, informationScreen;
    [SerializeField]
    private GameObject miniGamesCanvas, cam, player, mainCanvas, scenario;
    bool tarefaSpawned;
    public GameObject[] tarefasTriggers;
    public int _energy;
    public bool pass;
    public bool isPlayerClose;
    //public UnityEvent myEvent;
    // Start is called before the first frame update

    void Awake()
    {

        miniGamesCanvas = GameObject.FindGameObjectWithTag("TarefaCanvas");
        prefab = Resources.Load<GameObject>(path);
        informationScreen = Resources.Load<GameObject>("TarefaDescriptionFiscal");
        tarefasTriggers = GameObject.FindGameObjectsWithTag("TarefaTrigger");
        mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas");
        cam = Camera.main.gameObject;
        scenario = GameObject.Find("Scenario");
        player = GameObject.Find("Player");
        GetComponentInChildren<Transform>().gameObject.GetComponentInChildren<Button>().onClick.AddListener(SpawnTaskInformationScreen);
        GetComponentInChildren<Transform>().gameObject.GetComponentInChildren<Image>().color = new Color(0, 0, 0, 0);
        //tarefasTriggers = (GameObject[])GameObject.FindObjectsOfType(typeof(TarefaTrigger));
        pass = true;
        isPlayerClose = false;
#if UNITY_EDITOR
        //isPlayerClose = true;
#endif
    }

    void Update()
    {

        // if(mainCanvas.gameObject.activeInHierarchy == false){
        //     Enable(false);
        // }else{
        //     Enable(true);
        // }
    }


    public void SpawnTaskInformationScreen()
    {
        TutorialDescription tD = FindObjectOfType<TutorialDescription>();

        if (!pass || tD != null)
            return;

        RegionInformationScreen regionIn = FindObjectOfType<RegionInformationScreen>();
        if (regionIn != null)
        {
            Destroy(regionIn.gameObject);
        }

        GameObject go = GameObject.Instantiate(informationScreen, miniGamesCanvas.transform.position, transform.rotation * Quaternion.Euler(0, 0, 0), miniGamesCanvas.transform);
        go.name = informationScreen.name;

        RegionProperties _region = GetComponent<RegionProperties>();

        go.GetComponent<RegionInformationScreen>().region = _region.region;
        go.GetComponent<RegionInformationScreen>().startTaskButton.onClick.AddListener(Spawn);

        Enable3(false);

    }
    public void Spawn()
    {
#if UNITY_EDITOR
        isPlayerClose = true;
#endif
        if (!isPlayerClose)
        {
            GameObject noEnergyText = Instantiate(Resources.Load<GameObject>("FadeText"), miniGamesCanvas.transform.position, miniGamesCanvas.transform.rotation, miniGamesCanvas.transform);
            TextFade.SetText("Aproxime-se da propriedade para realizar a tarefa.");
            return;
        }
        EnergyManager energyManager = FindObjectOfType<EnergyManager>();
        RegionInformationScreen infoScreen = FindObjectOfType<RegionInformationScreen>();

        _energy = infoScreen.energy;

        if (energyManager.slider.value >= _energy)
        {
            GameObject go = GameObject.Instantiate(prefab, miniGamesCanvas.transform.position, transform.rotation * Quaternion.Euler(0, 0, 0), miniGamesCanvas.transform);
            go.name = prefab.name;

            CheckIfPrefabContainsScript(go);
            SendMessageToSCripts(go);

            Enable(false);
        }
        else
        {
            GameObject noEnergyText = Instantiate(Resources.Load<GameObject>("FadeText"), miniGamesCanvas.transform.position, miniGamesCanvas.transform.rotation, miniGamesCanvas.transform);
            TextFade.SetText("Sem energia para realizar a tarefa.");
            return;
        }

    }

    public void SendMessageToSCripts(GameObject go)
    {
        Transform[] allChildren = go.GetComponentsInChildren<Transform>();
        RegionDescriptionContainer regionDescriptionData = Resources.Load<RegionDescriptionContainer>("RegionDescriptionData");

        foreach (Transform child in allChildren)
        {
            child.SendMessage("SetEnergy", this.gameObject.GetComponent<RegionProperties>().region.ToString(), SendMessageOptions.DontRequireReceiver);
            //break;
        }
    }

    ///<summary>
    /// Coloque false para desabilitar os box collider e true para habilitar
    ///</summary>
    public void Enable(bool enable)
    {
        foreach (GameObject i in tarefasTriggers)
        {

            i.GetComponent<BoxCollider>().enabled = enable;
        }
        if (mainCanvas != null)
            //mainCanvas.GetComponent<Canvas>().enabled = enable;

            mainCanvas.GetComponentInChildren<TimeManager>().enabled = enable;
    }

    public void Enable2(bool enable)
    {

        if (SceneManager.GetActiveScene().name == "Game")
        {

            cam.SetActive(enable);
            //scenario.SetActive(enable);
            player.SetActive(enable);
            //mainCanvas.GetComponent<Canvas>().enabled = enable;
            miniGamesCanvas.GetComponent<Canvas>().enabled = enable;

            TarefaTrigger.CallEnable(enable);
            mainCanvas.GetComponentInChildren<TimeManager>().startTimer = enable;
        }
    }

    public void Enable3(bool enable)
    {

        if (SceneManager.GetActiveScene().name == "Game")
        {

            cam.GetComponent<CameraBehavior>().enabled = enable;
            player.GetComponent<PlayerController>().enabled = enable;
            //TarefaTrigger.CallEnable(enable);
            foreach (GameObject i in tarefasTriggers)
            {

                i.GetComponent<BoxCollider>().enabled = enable;
            }
            //mainCanvas.GetComponentInChildren<TimeManager>().startTimer = enable;
        }

    }

    ///<summary>
    /// Coloque false para desabilitar os box collider e true para habilitar
    ///</summary>
    public static void CallEnable(bool _enable)
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            TarefaTrigger tarefa;
            tarefa = FindObjectOfType<TarefaTrigger>();
            tarefa.Enable(_enable);
        }
        else
            return;
    }

    public static void CallEnable2(bool _enable)
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            TarefaTrigger tarefa;
            tarefa = FindObjectOfType<TarefaTrigger>();
            tarefa.Enable2(_enable);
        }
        else
            return;
    }

    public static void CallEnable3(bool _enable)
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            TarefaTrigger tarefa;
            tarefa = FindObjectOfType<TarefaTrigger>();
            tarefa.Enable3(_enable);

        }
        else
            return;
    }

    public void CheckIfPrefabContainsScript(GameObject obj)
    {
        if (obj.GetComponent<FiscalSiloManager>() != null)
        {

            RegionProperties region = GetComponent<RegionProperties>();
            obj.GetComponent<FiscalSiloManager>().region = region.region;

        }

        if (obj.GetComponent<MinigameGerador>() != null)
        {

            obj.GetComponent<MinigameGerador>().isFiscal = true;

        }

        if (obj.GetComponent<FiscaliMedirPH>() != null)
        {

            obj.GetComponent<FiscaliMedirPH>().isFiscal = true;

        }

        if (obj.GetComponent<MiniGameFilcalAquecedor>() != null)
        {

            obj.GetComponent<MiniGameFilcalAquecedor>().isFiscal = true;

        }
    }

    public static void CallPass(bool _pass)
    {
        TarefaTrigger tarefaTrigger = FindObjectOfType<TarefaTrigger>();
        tarefaTrigger.Pass(_pass);
    }

    public void Pass(bool _pass)
    {
        foreach (GameObject i in tarefasTriggers)
        {

            i.GetComponent<TarefaTrigger>().pass = _pass;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
            isPlayerClose = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
            isPlayerClose = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
            isPlayerClose = false;
    }

}
