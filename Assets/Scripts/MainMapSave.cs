using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;
using UnityEngine.SceneManagement;
using System.Linq;

public class MainMapSave : MonoBehaviour
{
    public bool enableInEditor;
    TimeManager timeManager;
    PlayerController player;
    public Vector3 sunPosition;
    public Vector3 playerPosition;
    public float time;
    public float t;
    public GameObject content;

    //public RegionProperties[] regionProperties;
    RegionValuesContainer regionData;
    EnergyValuesContainer energyData;
    MoneyValuesContainer moneyData;
    TasksSpawnedContainer tasksSpawnedData;
    DayValuesContainer dayData;
    LoteValuesContainer loteData;
    TasksCounterValuesContainer tasksCounterData;
    StoreItem storeData;
    public MinigamesValuesContainers[] minigamesData;

    public string[] saveFiles;

    private void Awake()
    {
        
#if UNITY_EDITOR
        this.enabled = enableInEditor;
        this.gameObject.SetActive(enableInEditor);
        
#endif
        player = FindObjectOfType<PlayerController>();
        timeManager = FindObjectOfType<TimeManager>();
        //regionProperties = FindObjectsOfType<RegionProperties>();
        regionData = Resources.Load<RegionValuesContainer>("RegionData");
        energyData = Resources.Load<EnergyValuesContainer>("EnergyData");
        moneyData = Resources.Load<MoneyValuesContainer>("MoneyData");
        tasksSpawnedData = Resources.Load<TasksSpawnedContainer>("SpawnedTasksData");
        dayData = Resources.Load<DayValuesContainer>("DayData");
        loteData = Resources.Load<LoteValuesContainer>("LoteData");
        tasksCounterData = Resources.Load<TasksCounterValuesContainer>("TasksCounterData");
        storeData = Resources.Load<StoreItem>("Store/ShopData");
    }

    private void OnGUI() {
        #if UNITY_EDITOR
        
        if (GUI.Button(new Rect(10, 250, 150, 50), "DELETE SAVE FILES"))
            DeleteSave();

        #endif
    }

    void Start()
    {
        //#if UNITY_EDITOR
        //    load2();
        //#endif
        Load();

    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }

        if(Input.GetKeyDown(KeyCode.D)){
           saveFiles =  QuickSaveRaw.GetAllFiles().ToArray();
        }
#endif

    }
    private void OnApplicationQuit()
    {
        Save();

    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus) Save();
    }


    public void Save()
    {
        playerPosition = player.gameObject.transform.position;
        sunPosition = timeManager.sun.transform.position;
        time = timeManager.timer;
        t += Time.deltaTime;

        // saves all scriptable objects data
        regionData.SaveAll(); // saves to : RegionData.json //
        energyData.SaveAll(); // saves to: SaveData.json //
        moneyData.SaveAll(); // saves to: SaveData.json //
        tasksSpawnedData.SaveAllEventsNotSpawned(); // saves to: TasksSpawnedDataNS.json //
        tasksSpawnedData.SaveAllSpawned(); // saves to: TasksSpawnedDataS.json //
        loteData.SaveAll(); // saves to: SaveData.json //
        tasksCounterData.SaveAll(); // saves to: SaveData.json //
        storeData.SaveAll(); // saves to: ShopData.json or StoreData.json i dont remember :P // 
        //

        // saves all minigames data
        foreach (MinigamesValuesContainers i in minigamesData)
        {
            i.SaveAll();
        }
        //

        QuickSaveWriter.Create("SaveData")
            .Write("playerPosi", playerPosition)
            .Write("clockTime", time)
            .Write("sunPosi", sunPosition)
            .Write("T", t)
            .Commit();
        dayData.SaveAll(); // saves to: SaveData.json //

        PlayerPrefs.Save();

        Debug.Log(QuickSaveRaw.LoadString("SaveData.json"));
    }
    public void Load()
    {
        if (SceneManager.GetActiveScene().name == "TitleScreen")
        {
            //loads all scriptable objects data
            regionData.LoadAll(); // //
            energyData.LoadAll(); // //
            moneyData.LoadAll(); // //
            tasksSpawnedData.LoadAllNotSpawned(); // //
            tasksSpawnedData.LoadAllSpawned(); // //
            loteData.LoadAll(); // //
            tasksCounterData.LoadAll(); // //
            storeData.LoadAll(); // //
                                 //

            // loads all minigames data
            foreach (MinigamesValuesContainers i in minigamesData)
            {
                i.LoadAll();
            }
            //

            Debug.Log("All assets loaded");
        }

        if (SceneManager.GetActiveScene().name == "Game")
        {
            Debug.Log("loading time...");

            QuickSaveReader.Create("SaveData")
                .Read<float>("clockTime", (r) => { time = r; })
                .Read<float>("T", (r) => { t = r; })
                .Read<Vector3>("sunPosi", (r) => { sunPosition = r; })
                .Read<Vector3>("playerPosi", (r) => { playerPosition = r; });

            dayData.LoadAll();

            player.gameObject.transform.position = playerPosition;
            player.target.gameObject.transform.position = playerPosition;
            timeManager.timer = time;
            timeManager.sun.transform.position = sunPosition;

            Camera.main.GetComponent<CameraBehavior>().GoToPlayer();

            tasksSpawnedData.InstantiateAllNotSpawned(); // //
            tasksSpawnedData.InstantiateAllSpawned(); // //

            Debug.Log("loaded");
        }
    }

    //Load2 is only used at editor
    void load2(){
        // if (SceneManager.GetActiveScene().name == "TitleScreen")
        // {
            //loads all scriptable objects data
            regionData.LoadAll(); // //
            energyData.LoadAll(); // //
            moneyData.LoadAll(); // //
            tasksSpawnedData.LoadAllNotSpawned(); // //
            tasksSpawnedData.LoadAllSpawned(); // //
            loteData.LoadAll(); // //
            tasksCounterData.LoadAll(); // //
            storeData.LoadAll(); // //
                                 //

            // loads all minigames data
            foreach (MinigamesValuesContainers i in minigamesData)
            {
                i.LoadAll();
            }
            //

            Debug.Log("All assets loaded");
        //}

        // if (SceneManager.GetActiveScene().name == "Game")
        // {
            Debug.Log("loading time...");

            QuickSaveReader.Create("SaveData")
                .Read<float>("clockTime", (r) => { time = r; })
                .Read<float>("T", (r) => { t = r; })
                .Read<Vector3>("sunPosi", (r) => { sunPosition = r; })
                .Read<Vector3>("playerPosi", (r) => { playerPosition = r; });

            dayData.LoadAll();

            player.gameObject.transform.position = playerPosition;
            player.target.gameObject.transform.position = playerPosition;
            timeManager.timer = time;
            timeManager.sun.transform.position = sunPosition;

            Camera.main.GetComponent<CameraBehavior>().GoToPlayer();

            tasksSpawnedData.InstantiateAllNotSpawned(); // //
            tasksSpawnedData.InstantiateAllSpawned(); // //

            Debug.Log("loaded");
        //}
    }

    public static void DeleteSave()
    {
        // string[] saveFiles = QuickSaveRaw.GetAllFiles().ToArray();

        // for (int i = 0; i < saveFiles.Length; i++){
        
        //     saveFiles[i] += ".json";

        //     Debug.Log("deleting: " + saveFiles[i]);
            
        //     QuickSaveRaw.Delete(saveFiles[i]);
        // }

        QuickSaveRaw.Delete("SaveData.json");

        MainMapSave main = FindObjectOfType<MainMapSave>();

        main.regionData.ResetAll(); 
        main.energyData.ResetAll(); 
        main.moneyData.ResetAll(); 
        main.tasksSpawnedData.ResetAll();   
        main.loteData.ResetAll(); 
        main.tasksCounterData.ResetAll();   
        main.storeData.ResetAll(); 
        main.dayData.ResetAll();  

        foreach (MinigamesValuesContainers i in main.minigamesData)
        {
            i.ResetAll();
        }

        PlayerPrefs.DeleteAll();

        Debug.Log("All save files were deleted");

        #if UNITY_EDITOR
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //SceneManager.LoadScene("TitleScreen");
        #endif
        SceneManager.LoadScene("TitleScreen");

    }
}
