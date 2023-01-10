using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using CI.QuickSave;

[CreateAssetMenu(fileName = "LoteData", menuName = "ScriptableObject/loteData")]
public class LoteValuesContainer : ScriptableObject
{
    [Serializable]
    public struct loteData { public string name; public int value, maxValue, cureDesieaseCost, healthToRemove; }
    public loteData[] datas;

    [Serializable]
    public struct loteDataDefault { public string name; public int value, maxValue, cureDesieaseCost, healthToRemove; }
    public loteDataDefault[] datasDefault;

    public bool setDefaultValues;

    private void OnValidate()
    {
        if (setDefaultValues)
        {
            datasDefault = new loteDataDefault[datas.Length];

            for (int i = 0; i < datasDefault.Length; i++)
            {
                datasDefault[i].name = datas[i].name;
                datasDefault[i].value = datas[i].value;
                datasDefault[i].maxValue = datas[i].maxValue;
                datasDefault[i].cureDesieaseCost = datas[i].cureDesieaseCost;
                datasDefault[i].healthToRemove = datas[i].healthToRemove;
            }

            setDefaultValues = false;
        }
    }

    public void SetValue(string name, int _value)
    {
        int i = Array.FindIndex(datas, x => x.name == name);

        datas[i].value = _value;
    }

    public int GetValue(string name)
    {
        return datas.FirstOrDefault(x => x.name == name).value;
    }

    public int GetHealthRemove(string name)
    {
        return datas.FirstOrDefault(x => x.name == name).healthToRemove;
    }

    public int GetMaxValue(string name)
    {
        return datas.FirstOrDefault(x => x.name == name).maxValue;
    }

    public int GetCure(string name)
    {
        return datas.FirstOrDefault(x => x.name == name).cureDesieaseCost;
    }

    public void AddQtty(string name, int _value)
    {
        int i = Array.FindIndex(datas, x => x.name == name);

        datas[i].value += _value;


    }

    public void RemoveQtty(string name, int _value)
    {
        int i = Array.FindIndex(datas, x => x.name == name);

        datas[i].value -= _value;

        if (datas[i].value <= 0)
            datas[i].value = 0;
    }

    public void SaveAll()
    {
        for (int i = 0; i < datas.Length; i++)
        {
            QuickSaveWriter.Create("SaveData")
                .Write<int>(datas[i].name + ".v", datas[i].value)
                .Write<int>(datas[i].name + ".mv", datas[i].maxValue)
                //.Write<int>(datas[i].name + ".cdc", datas[i].cureDesieaseCost)
                .Write<int>(datas[i].name + ".htr", datas[i].healthToRemove)
                .Commit();

        }
    }

    public void LoadAll()
    {
        for (int i = 0; i < datas.Length; i++)
        {
            QuickSaveReader.Create("SaveData")
                .Read<int>(datas[i].name + ".v", (r) => { datas[i].value = r; })
                .Read<int>(datas[i].name + ".mv", (r) => { datas[i].maxValue = r; })
                //.Read<int>(datas[i].name + ".cdc", (r) => { datas[i].cureDesieaseCost = r;})
                .Read<int>(datas[i].name + ".htr", (r) => { datas[i].healthToRemove = r; });
        }
    }

    public void ResetAll()
    {
        for (int i = 0; i < datas.Length; i++)
        {
            QuickSaveWriter.Create("SaveData")
                .Write<int>(datas[i].name + ".v", datasDefault[i].value)
                .Write<int>(datas[i].name + ".mv", datasDefault[i].maxValue)
                //.Write<int>(datas[i].name + ".cdc", datasDefault[i].cureDesieaseCost)
                .Write<int>(datas[i].name + ".htr", datasDefault[i].healthToRemove)
                .Commit();

        }
    }
}
