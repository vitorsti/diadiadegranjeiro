using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[CreateAssetMenu(fileName = "itenShopData", menuName = "ScriptableObject/itenShopData")]
public class ItenValuesContainer : ScriptableObject
{
     [Serializable]
    public struct valuesData { public string name; public float cost; [TextArea] public string description;}
    public valuesData[] datas;
    public string GetName(string _name)
    {
        return datas.FirstOrDefault(x => x.name == _name).name;
    }

    public string GetDescrition(string _name)
    {
        return datas.FirstOrDefault(x => x.name == _name).description;
    }

    public float GetCost(string _name)
    {
        return datas.FirstOrDefault(x => x.name == _name).cost;
    }
}
