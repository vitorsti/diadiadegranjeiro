using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventProperties;

public class EntragaDeracaoManager : MonoBehaviour
{
    public Region region;
    public RegionValuesContainer regionData;
    public GameObject eventSpawner;
    public GameObject minigameColocarRacao, minigameRodoComDefeito, miniGameControleDeEntrada, minigameCanvas, tarefasButton, nextDayButton, missionPanel;
    Camera cam;
    CameraBehavior cameraBehavior;

    // Start is called before the first frame update
    void Awake()
    {
        minigameCanvas = GameObject.FindGameObjectWithTag("TarefaCanvas");
        regionData = Resources.Load<RegionValuesContainer>("RegionData");
        tarefasButton = GameObject.Find("TarefasButton");
        nextDayButton = GameObject.Find("NextDayyButton");
        missionPanel = GameObject.Find("MissionPanel");
        cam = Camera.main;
        cameraBehavior = cam.GetComponent<CameraBehavior>();

    }

    void Start()
    {
        cam.orthographicSize = cameraBehavior.zoomOutMax;
        SetActivate(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.name == "caminhao")
        {
            cam.transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y, transform.position.z);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        RegionManager regionManager = FindObjectOfType<RegionManager>();
        if (other.name == "caminhao")
        {
            if (regionData.GetHealthStatus(region.ToString()) == 0)
            {
                print("Rodo com defeito");
                if (minigameRodoComDefeito != null)
                {
                    GameObject go = GameObject.Instantiate(minigameRodoComDefeito, regionManager.parent.gameObject.transform.position, Quaternion.identity, regionManager.parent.gameObject.transform);
                    go.name = minigameRodoComDefeito.name;
                }
            }
            else
                print("Tudo certo");

            print(other.name + ": entrou");

        }
    }

    public void StartMinigame()
    {
        print("starting minigame...");
        if (this.gameObject.name == "caminhao")
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
        GameObject go = GameObject.Instantiate(minigameColocarRacao, minigameCanvas.transform.position, minigameCanvas.transform.rotation, minigameCanvas.transform);
        go.name = minigameColocarRacao.name;
        //SetActivate(false);
    }

    public void StartControleDeEntrada()
    {
        if (this.gameObject.name == "caminhao")
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Animator>().speed = 0;
        }
        GameObject t = GameObject.Instantiate(miniGameControleDeEntrada, minigameCanvas.transform.position, minigameCanvas.transform.rotation, minigameCanvas.transform);
        t.name = miniGameControleDeEntrada.name;
        //SetActivate(false);
    }

    public void Encerrar()
    {
        SetActivate(true);
        cam.transform.position = new Vector3(0, Camera.main.transform.position.y, 0);
        Destroy(eventSpawner);
        Destroy(transform.parent.gameObject);
        //CallAds.ShowAdAfter();

    }

    public void SetSpawner(GameObject _setSpawner)
    {
        eventSpawner = _setSpawner;
    }

    void SetActivate(bool enable)
    {
        tarefasButton.SetActive(enable);

        if (nextDayButton != null)
            nextDayButton.SetActive(enable);

        if (missionPanel != null)
            missionPanel.SetActive(enable);

        TarefaTrigger.CallPass(enable);
        cameraBehavior.enabled = enable;
    }

}
