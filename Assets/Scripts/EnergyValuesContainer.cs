using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using CI.QuickSave;

[CreateAssetMenu(fileName = "EnergyData", menuName = "ScriptableObject/energyData")]
public class EnergyValuesContainer : ScriptableObject
{
    [Serializable]
    public struct moneyData { public string name; public int value; }
    public moneyData[] datas;

    [Serializable]
    public struct moneyDataDefault { public string name; public int value; }
    public moneyDataDefault[] datasDefault;

    public bool resetEnergy;
    public bool setDefaultValues;

    void OnValidate()
    {
        if (resetEnergy)
        {
            SetValue("value", GetValue("maxValue"));
            resetEnergy = false;
        }

        if (setDefaultValues)
        {
            datasDefault = new moneyDataDefault[datas.Length];
            for (int i = 0; i < datasDefault.Length; i++)
            {
                datasDefault[i].name = datas[i].name;
                datasDefault[i].value = datas[i].value;
            }
            setDefaultValues = false;
        }
    }

    public void SetValue(string name, int _value)
    {
        int i = Array.FindIndex(datas, x => x.name == name);

        datas[i].value = _value;

        Save(name, _value);

    }

    public int GetValue(string name)
    {
        return datas.FirstOrDefault(x => x.name == name).value;
    }

    public void AddQtty(string name, int _value)
    {
        int i = Array.FindIndex(datas, x => x.name == name);

        datas[i].value += _value;

        Save(name, _value);
    }

    public void RemoveQtty(string name, int _value)
    {
        int i = Array.FindIndex(datas, x => x.name == name);

        datas[i].value -= _value;

        if (datas[i].value <= 0)
            datas[i].value = 0;

        Save(name, _value);
    }

    void Save<T>(string name, T value)
    {
        QuickSaveWriter.Create("SaveData")
        .Write(name, value)
        .Commit();

        //Debug.Log(value);
    }

    public void SaveAll()
    {

        for (int i = 0; i < datas.Length; i++)
        {
            QuickSaveWriter.Create("SaveData")
                            .Write<int>(datas[i].name, datas[i].value)
                            .Commit();

        }

        Debug.Log(QuickSaveRaw.LoadString("SaveData.json"));

    }

    public void LoadAll()
    {
        for (int i = 0; i < datas.Length; i++)
        {
            QuickSaveReader.Create("SaveData")
                            .Read<int>(datas[i].name, (r) => { datas[i].value = r; });

        }

    }

    public void ResetAll()
    {
        for (int i = 0; i < datas.Length; i++)
        {
            QuickSaveWriter.Create("SaveData")
                            .Write<int>(datas[i].name, datasDefault[i].value)
                            .Commit();

        }
    }


}
