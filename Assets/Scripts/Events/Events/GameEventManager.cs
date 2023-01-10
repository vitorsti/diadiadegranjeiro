using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using EventProperties;
using System;

public class GameEventManager : MonoBehaviour
{
    [Header("----- Listas de Eventos -----")]
    public ListOfEvents ConsequenceEvents;
    public ListOfEvents SubConsequenceEvents;
    public ListOfEvents MegaConsequenceEvents;
    public ListOfEvents RandomEvents;
    public List<KeyValuePair<Region, GameObject>> activeEvents = new List<KeyValuePair<Region, GameObject>>();

    [Header("----- Estrutura -----")]
    public Map[] Maps;
    Dictionary<Region, GameObject> RegionsList;
    GameEventType eventType;

    DayValuesContainer dayData;

    [Serializable]
    public struct Map
    {
        public Region regionName;
        public GameObject area;
    }

    public GameObject fadeScreen;

    // Start is called before the first frame update
    void Awake()
    {
        RegionsList = new Dictionary<Region, GameObject>() { };
        foreach (Map reg in Maps)
            RegionsList.Add(reg.regionName, reg.area);

        dayData = Resources.Load<DayValuesContainer>("DayData");
    }

    private List<Region> VerifyUnhealthRegions()
    {
        RegionProperties[] get = GameObject.FindObjectsOfType<RegionProperties>();
        List<Region> unhealthRegions = new List<Region>();

        foreach (RegionProperties reg in get)
            if (!reg.isHealthy)
                unhealthRegions.Add(reg.region);

        return unhealthRegions;
    }

    public void RemoveEventFromManager(Region _reg, GameObject _event)
    {
        GameObject eventRef = activeEvents.Find(x => x.Value.name == _event.name).Value;
        activeEvents.Remove(new KeyValuePair<Region, GameObject>(_reg, eventRef));
        DebugKey();
    }

    void DebugKey()
    {
        if (activeEvents.Count != 0)
            activeEvents.ForEach(x => Debug.LogWarning("[]EventosAtivos: " + x.Key + " - " + x.Value));
        else
            Debug.LogWarning("DebugKey - Lista vazia");
    }

    void ConsequenceEventsController()
    {
        List<GameObject> possibleEvents = new List<GameObject>();
        List<Region> possibleRegions = new List<Region>();
        possibleRegions = VerifyUnhealthRegions();

        //Pega a lista de todos os eventos, verifica se há alguma região que não está bem cuidada, se sim, pega os eventos que podem ser spawnadas nessas regiões,
        //Caso não tenha uma região com saude ruim, seleciona um dos eventos aleatórios.
        if (possibleRegions.Count > 0)
        {
            ConsequenceEvents.Events.ForEach(_event =>
            {
                if (possibleRegions.Intersect(_event.gameObject.GetComponent<EventHeader>().props.regions).Any() && !possibleEvents.Contains(_event))
                    possibleEvents.Add(_event);
                else if (_event.gameObject.GetComponent<EventHeader>().props.regions[0] == Region.Any)
                    possibleEvents.Add(_event);
            });

            eventType = GameEventType.Consequence;
            Debug.Log("Possible regions: " + possibleRegions.Count);
        }
        else
        {
            RandomEvents.Events.ForEach(_event =>
            {
                if (_event.GetComponent<EventHeader>().props.eventType == GameEventType.Random)
                    possibleEvents.Add(_event);
            });

            eventType = GameEventType.Random;
        }

        Debug.Log("Count: " + possibleEvents.Count);

        //Pega a lista de eventos que podem spawnar, sorteia um deles, pega as possíveis regiões desse evento e escolhe onde spawnar.
        /*if (possibleEvents != null)
        {
            List<Region> areas = new List<Region>();
            int rng = UnityEngine.Random.Range(0, possibleEvents.Count);

            GameObject region;
            GameObject spawnedEvent = possibleEvents[rng];

            if (eventType == GameEventType.Consequence)
            {
                //Verifica se o evento tem regiões especificas ou pode ser spawnado em qualquer um delas,
                if ((spawnedEvent.gameObject.GetComponent<EventHeader>().props.regions.Count >= 1) && spawnedEvent.gameObject.GetComponent<EventHeader>().props.regions[0] != Region.Any)
                    areas = possibleRegions.Intersect(spawnedEvent.gameObject.GetComponent<EventHeader>().props.regions).ToList();

                //Se a região do evento for any, o evento irá spawnar em qualquer região danificada.
                else if (spawnedEvent.gameObject.GetComponent<EventHeader>().props.regions[0] == Region.Any)
                    areas.AddRange(possibleRegions);
            }
            else if (eventType == GameEventType.Random)
            {
                //Verifica se o evento tem regiões especificas ou pode ser spawnado em qualquer um delas,
                if ((spawnedEvent.gameObject.GetComponent<EventHeader>().props.regions.Count >= 1) && spawnedEvent.gameObject.GetComponent<EventHeader>().props.regions[0] != Region.Any)
                    areas.AddRange(spawnedEvent.gameObject.GetComponent<EventHeader>().props.regions);

                //Any é removido da lista de regiões pois não é uma região em si.
                else if (spawnedEvent.gameObject.GetComponent<EventHeader>().props.regions[0] == Region.Any)
                {
                    areas.AddRange(Enum.GetValues(typeof(Region)).Cast<Region>());
                    areas.Remove(Region.Any);
                }
            }

            int i = UnityEngine.Random.Range(0, areas.Count);

            //Procura na estrutura Maps uma region equivalente a região do evento e retorna o GameObject dessa região. [Key = Region | Value = GameObject]
            region = RegionsList.FirstOrDefault(x => x.Key == areas[i]).Value;

            //Seta as informações do evento
            spawnedEvent.GetComponent<EventHeader>().regionSpawned = areas[i];
            spawnedEvent.GetComponent<EventHeader>().regionObject = region;

            Debug.Log("!!Consequence: " + spawnedEvent.name + " na Região: " + region.name);

            //Verifica se o evento já existe naquela região
            if (!region.GetComponent<RegionManager>().CheckEventOnList(spawnedEvent.name))
            {
                activeEvents.Add(new KeyValuePair<Region, GameObject>(areas[i], spawnedEvent));
                region.GetComponent<RegionManager>().AddNewEvent(spawnedEvent);
                Debug.Log("!!Consequence enviado para: " + region.name);
            }*/

        if (possibleEvents != null)
        {
            List<Region> areas = new List<Region>();
            //int rng = UnityEngine.Random.Range(0, possibleEvents.Count);

            possibleEvents.ForEach(_event =>
            {
                GameObject region;
                GameObject spawnedEvent = _event;

                if (eventType == GameEventType.Consequence)
                {
                    //Verifica se o evento tem regiões especificas ou pode ser spawnado em qualquer um delas,
                    if ((spawnedEvent.gameObject.GetComponent<EventHeader>().props.regions.Count >= 1) && spawnedEvent.gameObject.GetComponent<EventHeader>().props.regions[0] != Region.Any)
                        areas = possibleRegions.Intersect(spawnedEvent.gameObject.GetComponent<EventHeader>().props.regions).ToList();

                    //Se a região do evento for any, o evento irá spawnar em qualquer região danificada.
                    else if (spawnedEvent.gameObject.GetComponent<EventHeader>().props.regions[0] == Region.Any)
                        areas.AddRange(possibleRegions);
                }
                else if (eventType == GameEventType.Random)
                {
                    //Verifica se o evento tem regiões especificas ou pode ser spawnado em qualquer um delas,
                    if ((spawnedEvent.gameObject.GetComponent<EventHeader>().props.regions.Count >= 1) && spawnedEvent.gameObject.GetComponent<EventHeader>().props.regions[0] != Region.Any)
                        areas.AddRange(spawnedEvent.gameObject.GetComponent<EventHeader>().props.regions);

                    //Any é removido da lista de regiões pois não é uma região em si.
                    else if (spawnedEvent.gameObject.GetComponent<EventHeader>().props.regions[0] == Region.Any)
                    {
                        areas.AddRange(Enum.GetValues(typeof(Region)).Cast<Region>());
                        areas.Remove(Region.Any);
                    }
                }

                int i = UnityEngine.Random.Range(0, areas.Count);

                //Procura na estrutura Maps uma region equivalente a região do evento e retorna o GameObject dessa região. [Key = Region | Value = GameObject]
                region = RegionsList.FirstOrDefault(x => x.Key == areas[i]).Value;

                //Seta as informações do evento
                spawnedEvent.GetComponent<EventHeader>().regionSpawned = areas[i];
                spawnedEvent.GetComponent<EventHeader>().regionObject = region;

                Debug.Log("!!Consequence: " + spawnedEvent.name + " na Região: " + region.name);

                //Verifica se o evento já existe naquela região
                if (!region.GetComponent<RegionManager>().CheckEventOnList(spawnedEvent.name))
                {
                    activeEvents.Add(new KeyValuePair<Region, GameObject>(areas[i], spawnedEvent));
                    region.GetComponent<RegionManager>().AddNewEvent(spawnedEvent);
                    Debug.Log("!!Consequence enviado para: " + region.name);
                }
            });

            DebugKey();
        }
    }

    void SubConsequenceEventsController()
    {
        SubConsequenceEvents.Events.ForEach(_sub =>
        {
            //Para cada evento na lista de Eventos de SubConsequencia irá verificar se os eventos causadores já existem na lista de eventos ativos no jogo
            List<GameObject> listEvents = new List<GameObject>();

            listEvents = (from e in activeEvents select e.Value).Intersect(_sub.GetComponent<SubConsequenceProperties>().Causer).ToList();

            //Envia a quantidade de eventos causadores ativos para o SubConsequenceProperties que controla a chance de spawnar esse evento
            _sub.GetComponent<SubConsequenceProperties>().VerifyCauserQuantity(listEvents.Count);

            //Testa chance de spawnar esse evento
            int rng = UnityEngine.Random.Range(1, 101);

            if (rng < _sub.GetComponent<SubConsequenceProperties>().chanceToSpawn)
            {
                GameObject subEvent = _sub;
                int i = UnityEngine.Random.Range(0, subEvent.gameObject.GetComponent<EventHeader>().props.regions.Count);

                List<Region> areas = (Enum.GetValues(typeof(Region)).Cast<Region>().Intersect(subEvent.gameObject.GetComponent<EventHeader>().props.regions)).ToList();
                areas.Remove(Region.Any);
                GameObject region = RegionsList.FirstOrDefault(x => x.Key == areas[i]).Value;

                Debug.LogWarning("--SubConsequence Spawned: " + _sub.gameObject.name);

                if (!region.GetComponent<RegionManager>().CheckEventOnList(subEvent.name))
                {
                    subEvent.GetComponent<EventHeader>().regionSpawned = areas[i];
                    subEvent.GetComponent<EventHeader>().regionObject = region;

                    activeEvents.Add(new KeyValuePair<Region, GameObject>(areas[i], subEvent));
                    region.GetComponent<RegionManager>().AddNewEvent(subEvent);
                    Debug.Log(">>SubConsequence enviado para: " + region.name);
                }
            }
        });
    }

    void MegaConsequenceEventsController()
    {
        MegaConsequenceEvents.Events.ForEach(_mega =>
        {
            //Para cada Mega Consequencia, verifica a quantidade de causadores [Eventos de sub consequencia] ativos atualmente
            int activeCausers;
            activeCausers = (from e in activeEvents select e.Value).Intersect(_mega.GetComponent<MegaConsequenceProperties>().Causer).ToList().Count;
            Debug.Log("||Mega " + _mega.name + " causers: " + activeCausers);

            //Se a quantidade de causadores ativos for igual a quantidade de causadores que existem, significa que todos estão ativos, logo, spawna a Mega Consequencia
            if (activeCausers == _mega.GetComponent<MegaConsequenceProperties>().Causer.Count)
                Debug.LogWarning("||MegaConsequencia Spawned: " + _mega.name);
        });
    }

    void CheckUncompleteEvents()
    {
        if (activeEvents.Count > 0)
        {
            activeEvents.ForEach(_event =>
            {
                GameObject region = RegionsList.FirstOrDefault(x => x.Key == _event.Key).Value;
                region.GetComponent<RegionProperties>().DecreaseHealth();
                Debug.Log(region.name + " diminuiu devido a " + _event.Value.name);
            });
        }

    }

    void RemoveRegionHelath()
    {

        RegionProperties[] get = GameObject.FindObjectsOfType<RegionProperties>();

        foreach (RegionProperties reg in get)
        {

            reg.DecreaseHealth();
        }
    }

    void AddRegionHelath()
    {
        RegionProperties[] get = GameObject.FindObjectsOfType<RegionProperties>();
        print("All regions restored health to max");

        foreach (RegionProperties reg in get)
        {

            reg.MaxHealth();
        }
    }

    public void NextDay()
    {
        if (!EnergyManager.pass)
            EnergyManager.CallEnergyCheck();
        if (EnergyManager.pass)
        {
            DayManager dayManager = FindObjectOfType<DayManager>();
            /*if (dayManager.fiscalDay)
            {
                return;
            }*/
            if (fadeScreen != null)
            {
                fadeScreen.SetActive(true);
            }
            dayManager.Checkfiscal();
            RemoveRegionHelath();
            //CheckUncompleteEvents();
            ConsequenceEventsController();
            SubConsequenceEventsController();
            MegaConsequenceEventsController();
            dayData.PassDay(/*"day"*/);
            dayData.PassDay(/*"fiscal"*/);
            EnergyManager.ResetEnergy();
            TimeManager.ResetTime();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))  // Teste, apagar depois
        {
            ConsequenceEventsController();
        }
        if (Input.GetKeyDown(KeyCode.N))  // Teste, apagar depois
        {
            NextDay();
        }

        if (Input.GetKeyDown(KeyCode.A)) // Teste, apagar depois
        {
            AddRegionHelath();
        }
    }

    #region DailyController [Não usado]
    /*
    //Estrutura do Daily Events partindo do principio que cada evento diário só pode spawnar em um lugar especifico, ou pode spawnar em qualquer lugar, mas não em 2 lugares especificos
    public void DailyEventsController()
    {
        DailyEvents.ForEach(_event => 
        {
            if (_event.gameObject.GetComponent<EventHeade>().props.regions[0] == Region.Any)
            {
                Debug.Log("any ");

                int i = UnityEngine.Random.Range(1, RegionsList.Count);
                KeyValuePair<Region, GameObject> region = RegionsList.ElementAt(i);

                if(region.Value.gameObject.GetComponent<RegionManager>().regionActiveEvents != null)
                {
                    if (!region.Value.gameObject.GetComponent<RegionManager>().regionActiveEvents.Exists(x => x.name == _event.name))
                    {
                        Debug.Log("Daily add at '" + region.ToString() + "' the event '" + _event.name);
                        region.Value.gameObject.GetComponent<RegionManager>().AddNewEvent(_event);
                    }
                }
                else
                {
                    Debug.Log("Daily add at '" + region.ToString() + "' the event '" + _event.name);
                    region.Value.gameObject.GetComponent<RegionManager>().AddNewEvent(_event);
                }
 
            }
            else
            {
                Debug.Log("daily region: " + _event.name );

                GameObject region;
                region = RegionsList.FirstOrDefault(x => x.Key == _event.gameObject.GetComponent<EventHeade>().props.regions[0]).Value;

                if (!region.gameObject.GetComponent<RegionManager>().regionActiveEvents.Exists(x => x.name == _event.name)) //Se existe outro diario pode existir mais um?
                {
                    region.GetComponent<RegionManager>().AddNewEvent(_event);
                    Debug.Log("Daily add at '" + region.ToString() + "' the event '" + _event.name);

                }
            }
        });
    }*/
    #endregion
    #region DailyTaskController 
    /*
    public void DailyTaskController()
    {
        foreach (Map region in Maps)
        {
            if (region.area.GetComponent<RegionManager>().dailyTask != null)
            {
                Debug.Log(region.area.GetComponent<RegionManager>().name + " is Done: " + region.area.GetComponent<RegionManager>().dailyTaskComplete);
                region.area.GetComponent<RegionManager>().CheckTask();
            }
        }
    } */
    #endregion
}


