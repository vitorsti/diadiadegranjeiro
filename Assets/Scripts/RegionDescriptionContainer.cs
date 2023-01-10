using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[CreateAssetMenu(fileName = "RegionDescriptionData", menuName = "ScriptableObject/regionDescriptionData")]
public class RegionDescriptionContainer : ScriptableObject
{
    [Serializable]
    public struct regionDescritionData { public string name; [TextArea] public string description; public string taskName; public int estamina; public float timeToComplete; }
    public regionDescritionData[] datas;
    
    public string GetName(string _name)
    {
        return datas.FirstOrDefault(x => x.name == _name).taskName;
    }

    public string GetDescrition(string _name)
    {
        return datas.FirstOrDefault(x => x.name == _name).description;
    }

    public int GetEstamina(string _name)
    {
        return datas.FirstOrDefault(x => x.name == _name).estamina;
    }

     public float GetTimeToComplete(string _name)
    {
        return datas.FirstOrDefault(x => x.name == _name).timeToComplete;
    }

}
