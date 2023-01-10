using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using CI.QuickSave;

[CreateAssetMenu(fileName = "SpawnedTasksData", menuName = "ScriptableObject/spawnedTasksData")]
public class TasksSpawnedContainer : ScriptableObject
{
    public ListOfEvents[] listOfEvents;
    public List<string> notSpawned;
    public List<string> spawned;
    //public bool getDatas = false;
    public bool zerar = false;
    public bool load = false;
    public bool save = false;
    public bool resetSave;

    private void OnValidate()
    {
        if (zerar)
        {

            notSpawned.Clear();
            spawned.Clear();

            zerar = false;
        }

        if (save)
        {
            SaveAllEventsNotSpawned();
            SaveAllSpawned();
            Debug.Log("saved");
            Debug.Log(QuickSaveRaw.LoadString(this.name + "NS.json"));
            Debug.Log(QuickSaveRaw.LoadString(this.name + "S.json"));
            save = false;
        }

        if (load)
        {
            LoadAllNotSpawned();
            LoadAllSpawned();
            Debug.Log("loaded");
            //Debug.Log(QuickSaveRaw.LoadString(this.name + "NS.json"));
            load = false;
        }

        if (resetSave)
        {
            ResetAll();
            //Debug.Log(QuickSaveRaw.LoadString(this.name + "NS.json"));
            //Debug.Log(QuickSaveRaw.LoadString(this.name + "S.json"));

            resetSave = false;
        }

    }

    public void AddEvent(string _value)
    {
        notSpawned.Add(_value);
    }

    public void RemoveEvent(string _value)
    {
        notSpawned.Remove(_value);
    }

    public void AddSpawnedEvent(string _value)
    {

        spawned.Add(_value);
    }

    public void RemoveSpawnedEvent(string _value)
    {
        spawned.Remove(_value);
    }

    public void SaveAllEventsNotSpawned()
    {
        PlayerPrefs.SetInt("ns.count", notSpawned.Count);
        if (QuickSaveRaw.Exists(this.name + "NS.json") == true)
            QuickSaveRaw.Delete(this.name + "NS.json");
        for (int i = 0; i < notSpawned.Count; i++)
        {

            QuickSaveWriter.Create(this.name + "NS")
                           .Write<string>(i.ToString(), notSpawned[i])
                           .Commit();
        }
    }

    public void SaveAllSpawned()
    {
        PlayerPrefs.SetInt("s.count", spawned.Count);
        if (QuickSaveRaw.Exists(this.name + "S.json") == true)
            QuickSaveRaw.Delete(this.name + "S.json");
        for (int i = 0; i < spawned.Count; i++)
        {

            QuickSaveWriter.Create(this.name + "S")
                           .Write<string>(i.ToString(), spawned[i])
                           .Commit();
        }
    }

    public void LoadAllNotSpawned()
    {
        notSpawned.Clear();
        string[] t = new string[PlayerPrefs.GetInt("ns.count")];

        for (int i = 0; i < t.Length; i++)
        {
            QuickSaveReader.Create(this.name + "NS")
                            .Read<string>(i.ToString(), (r) => { t[i] = r; });

        }

        for (int i = 0; i < t.Length; i++)
        {
            notSpawned.Add(t[i]);
        }

        // RegionProperties[] regionProperties;

        // regionProperties = FindObjectsOfType<RegionProperties>();

        // for (int i = 0; i < notSpawned.Count; i++)
        // {
        //     for (int j = 0; j < listOfEvents.Length; j++)
        //     {
        //         for (int k = 0; k < listOfEvents[j].Events.Count; k++)
        //         {
        //             if (notSpawned[i] == listOfEvents[j].Events[k].name)
        //             {
        //                 GameObject go = /*Instantiate(*/listOfEvents[j].Events[k];/*, listOfEvents[j].Events[k].transform.position, Quaternion.identity);*/
        //                 for (int l = 0; l < regionProperties.Length; l++)
        //                 {
        //                     if (regionProperties[l].region == go.GetComponent<EventHeader>().regionSpawned)
        //                         regionProperties[l].GetComponent<RegionManager>().regionActiveEvents.Add(go);
        //                     else if (go.GetComponent<EventHeader>().regionSpawned == EventProperties.Region.Any)
        //                         regionProperties[0].GetComponent<RegionManager>().regionActiveEvents.Add(go);

        //                 }

        //             }

        //         }
        //     }
        // }


    }

    public void LoadAllSpawned()
    {
        spawned.Clear();
        string[] t = new string[PlayerPrefs.GetInt("s.count")];

        for (int i = 0; i < t.Length; i++)
        {
            QuickSaveReader.Create(this.name + "S")
                            .Read<string>(i.ToString(), (r) => { t[i] = r; });

        }

        for (int i = 0; i < t.Length; i++)
        {
            spawned.Add(t[i]);
        }

        // GameObject content;

        // content = FindObjectOfType<ContentOrganizer>().gameObject;

        // RegionProperties[] regionProperties;

        // regionProperties = FindObjectsOfType<RegionProperties>();

        // for (int i = 0; i < spawned.Count; i++)
        // {
        //     for (int j = 0; j < listOfEvents.Length; j++)
        //     {
        //         for (int k = 0; k < listOfEvents[j].Events.Count; k++)
        //         {
        //             if (spawned[i] == listOfEvents[j].Events[k].name)
        //             {
        //                 GameObject go = Instantiate(listOfEvents[j].Events[k], content.transform.position, content.transform.rotation, content.transform);
        //                 go.name = listOfEvents[j].Events[k].name;
        //                 go.GetComponent<EventHeader>().props.spawned = true;
        //                 for (int l = 0; l < regionProperties.Length; l++)
        //                 {
        //                     if (regionProperties[l].region == go.GetComponent<EventHeader>().regionSpawned)
        //                         regionProperties[l].GetComponent<RegionManager>().regionActiveEvents.Add(go);
        //                     else if (go.GetComponent<EventHeader>().regionSpawned == EventProperties.Region.Any)
        //                         regionProperties[0].GetComponent<RegionManager>().regionActiveEvents.Add(go);


        //                 }

        //             }

        //         }
        //     }
        // }
    }

    public void InstantiateAllNotSpawned()
    {
        RegionProperties[] regionProperties;

        regionProperties = FindObjectsOfType<RegionProperties>();
        //Debug.LogError(regionProperties.Length);

        for (int i = 0; i < notSpawned.Count; i++)
        {
            for (int j = 0; j < listOfEvents.Length; j++)
            {
                for (int k = 0; k < listOfEvents[j].Events.Count; k++)
                {
                    if (notSpawned[i] == listOfEvents[j].Events[k].name)
                    {
                        GameObject go = /*Instantiate(*/listOfEvents[j].Events[k];/*, listOfEvents[j].Events[k].transform.position, Quaternion.identity);*/
                        for (int l = 0; l < regionProperties.Length; l++)
                        {
                            if (regionProperties[l].region == go.GetComponent<EventHeader>().regionSpawned)
                                regionProperties[l].GetComponent<RegionManager>().regionActiveEvents.Add(go);

                            /* else*/
                            if (go.GetComponent<EventHeader>().regionSpawned == EventProperties.Region.Any)
                                regionProperties[0].GetComponent<RegionManager>().regionActiveEvents.Add(go);


                        }

                    }

                }
            }
        }
    }

    public void InstantiateAllSpawned()
    {
        GameObject content;

        content = FindObjectOfType<ContentOrganizer>().gameObject;

        RegionProperties[] regionProperties;

        regionProperties = FindObjectsOfType<RegionProperties>();

        for (int i = 0; i < spawned.Count; i++)
        {
            for (int j = 0; j < listOfEvents.Length; j++)
            {
                for (int k = 0; k < listOfEvents[j].Events.Count; k++)
                {
                    if (spawned[i] == listOfEvents[j].Events[k].name)
                    {
                        GameObject go = Instantiate(listOfEvents[j].Events[k], content.transform.position, content.transform.rotation, content.transform);
                        go.name = listOfEvents[j].Events[k].name;
                        go.GetComponent<EventHeader>().props.spawned = true;
                        for (int l = 0; l < regionProperties.Length; l++)
                        {
                            if (regionProperties[l].region == go.GetComponent<EventHeader>().regionSpawned)
                                regionProperties[l].GetComponent<RegionManager>().regionActiveEvents.Add(go);
                            /*else*/
                            if (go.GetComponent<EventHeader>().regionSpawned == EventProperties.Region.Any)
                                regionProperties[0].GetComponent<RegionManager>().regionActiveEvents.Add(go);


                        }

                    }

                }
            }
        }
    }

    public void ResetAll()
    {
        notSpawned.Clear();
        spawned.Clear();

        SaveAllEventsNotSpawned();
        SaveAllSpawned();
    }

}
