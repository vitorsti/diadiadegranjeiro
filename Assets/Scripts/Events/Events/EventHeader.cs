using EventProperties;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHeader : MonoBehaviour
{
    public EventBaseProperties props;
    public Region regionSpawned;
    public GameObject regionObject;
    TarefaTrigger tarefa;
    TasksSpawnedContainer tasksSpawned;

    // Start is called before the first frame update
    void Start()
    {
        tasksSpawned = Resources.Load<TasksSpawnedContainer>("SpawnedTasksData");
        if (regionObject == null)
        {
            tarefa = FindObjectOfType<TarefaTrigger>();
            for (int i = 0; i < tarefa.tarefasTriggers.Length; i++)
            {

                if (tarefa.tarefasTriggers[i].GetComponent<RegionProperties>().region == regionSpawned)
                {
                    regionObject = tarefa.tarefasTriggers[i].gameObject;
                    break;
                }
            }
        }

        Debug.LogWarning("Spawned: " + gameObject.name + "Region: " + regionSpawned);
    }

    private void OnDestroy()
    {
        if (props.eventType == GameEventType.Consequence || props.eventType == GameEventType.Random || props.eventType == GameEventType.SubConsequence)
        {
            //regionObject.GetComponent<RegionProperties>().IncreaseHealth();
            regionObject.GetComponent<RegionProperties>().MaxHealth();
            regionObject.GetComponent<RegionManager>().RemoveEvent(gameObject);
            FindObjectOfType<GameEventManager>().RemoveEventFromManager(regionSpawned, gameObject);
            tasksSpawned.RemoveSpawnedEvent(gameObject.name);
            Debug.Log("Resolvido: " + gameObject.name);
        }
    }
}
