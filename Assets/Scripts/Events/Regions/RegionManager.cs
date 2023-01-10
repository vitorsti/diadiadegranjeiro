using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class RegionManager : MonoBehaviour
{

    [Header("----- Eventos Gerais -----")]
    public List<GameObject> regionActiveEvents;
    public GameObject parent;
    public TasksCounterValuesContainer tasksData;
    public TasksSpawnedContainer tasksSpawnedData;
    void Awake()
    {
        tasksData = Resources.Load<TasksCounterValuesContainer>("TasksCounterData");
        tasksSpawnedData = Resources.Load<TasksSpawnedContainer>("SpawnedTasksData");
    }

    public void AddNewEvent(GameObject _event)
    {
        if (!CheckEventOnList(_event.name)){
            regionActiveEvents.Add(_event);
            tasksSpawnedData.AddEvent(_event.name);
        }
    }

    public void RemoveEvent(GameObject _event)
    {
        regionActiveEvents.Remove(_event);
    }

    public bool CheckEventOnList(string _event)
    {
        if (regionActiveEvents.Exists(x => x.name == _event))
            return true;
        else
            return false;
    }

    public void SpawnTask()
    {
        if (regionActiveEvents.Count != 0)
        {
            for (int i = 0; i < regionActiveEvents.Count; i++)
            {
                if (regionActiveEvents[i].GetComponent<EventHeader>().props.spawned == false)
                { 
                    
                    GameObject go = Instantiate(regionActiveEvents[i], transform.position, Quaternion.identity);
                    go.transform.SetParent(parent.transform, false);
                    go.GetComponent<EventHeader>().props.spawned = true;
                    go.GetComponent<EventHeader>().regionObject = this.gameObject;
                    go.name = regionActiveEvents[i].name;
                    tasksSpawnedData.RemoveEvent(go.name);
                    tasksSpawnedData.AddSpawnedEvent(go.name);
                    //print(go.GetComponent<EventHeader>().props.eventType.ToString());
                    regionActiveEvents[i] = go;
                    if (go.GetComponent<EventHeader>().props.eventType.ToString() != "Random")
                    {
                        tasksData.AddQtty(go.GetComponent<EventHeader>().props.eventType.ToString());
                    }
                   
                }
            }
        }
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.I)) // Teste, apagar depois
        {
            SpawnTask();
        }
#endif
    }

    #region DailyTask Logic 
    /*
        [Header("----- Eventos de Fiscalização -----")]
        public GameObject dailyTask;
        public bool dailyTaskComplete = false;
        [SerializeField] string filePath;
        //= "Events/DailyTask/...";

        public void LoadDailyTask()
        {
            dailyTask = Resources.Load<GameObject>(filePath);
        }

        public void LoadAllDailyTask()
        {
            if (filePath != string.Empty)
            {
                GameObject[] tasks = Resources.LoadAll<GameObject>(filePath);
                dailyTask = tasks[Random.Range(0, tasks.Length)];
            }
        }

        void CompleteTask()
        {
            Debug.Log(" CompleteTask: " + gameObject.name);
            gameObject.GetComponent<RegionProperties>().IncreaseHealth();
        }

        void IncompleteTask()
        {
            Debug.Log("IncompleteTask: " + gameObject.name);
            gameObject.GetComponent<RegionProperties>().DecreaseHealth();
        }

        public void CheckTask()
        {
            if (dailyTaskComplete)
                CompleteTask();
            else
                IncompleteTask();

            dailyTask = null;
            dailyTaskComplete = false;
        }
    */
    #endregion DailyTask Logic
}
