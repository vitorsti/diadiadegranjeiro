using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using CI.QuickSave;

[CreateAssetMenu(fileName = "DayData", menuName = "ScriptableObject/dayData")]
public class DayValuesContainer : ScriptableObject
{
    [Serializable]
    public struct dayData { public string name; public int value; }
    public dayData[] datas;

    [Serializable]
    public struct dayDataDefault { public string name; public int value; }
    public dayDataDefault[] datasDefault;

    public bool reset, passDay, save, load, setDefaultValues;

    void OnValidate()
    {
        if (reset)
        {
            for (int i = 0; i < datas.Length - 1; i++)
            {
                datas[i].value = 0;
            }
            reset = false;
        }

        if (passDay)
        {
            PassDay();
            passDay = false;
        }

        if (save)
        {

            SaveAll();
            save = false;
        }

        if (load)
        {
            LoadAll();
            load = false;
        }

        if (setDefaultValues)
        {
            datasDefault = new dayDataDefault[datas.Length];

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
    }

    public int GetValue(string name)
    {
        return datas.FirstOrDefault(x => x.name == name).value;
    }

    /*public void PassDay()
    {
        for (int i = 0; i < datas.Length; i++){
            datas[i].value++;
        }


    }*/

    public void PassDay(/*string name*/)
    {
        /*int i = Array.FindIndex(datas, x => x.name == name);

        datas[i].value += 1;*/

        for (int i = 0; i < datas.Length - 1; i++)
        {
            datas[i].value += 1;
        }
    }

    public void SaveAll()
    {
        for (int i = 0; i < datas.Length - 1; i++)
        {
            QuickSaveWriter.Create("SaveData")
                .Write<int>(datas[i].name + ".day", datas[i].value)
                .Commit();
        }
    }

    public void LoadAll()
    {
        for (int i = 0; i < datas.Length - 1; i++)
        {
            QuickSaveReader.Create("SaveData")
            .Read<int>(datas[i].name + ".day", (r) => { datas[i].value = r; });
        }
    }

    public void ResetAll()
    {
        for (int i = 0; i < datas.Length - 1; i++)
        {
            QuickSaveWriter.Create("SaveData")
                .Write<int>(datas[i].name + ".day", datasDefault[i].value)
                .Commit();
        }
    }


}
