using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using CI.QuickSave;

[CreateAssetMenu(fileName = "TasksCounterData", menuName = "ScriptableObject/tasksCounterData")]
public class TasksCounterValuesContainer : ScriptableObject
{
    [Serializable]
    public struct tasksCounterData { public string name; public float value; }
    public tasksCounterData[] datas;

    [Serializable]
    public struct tasksCounterDataDefault { public string name; public float value; }
    public tasksCounterDataDefault[] datasDefault;
    public bool zerar = false;
    public bool setDefaultValues;

    private void OnValidate()
    {
        if (zerar)
        {
            for (int i = 0; i < datas.Length; i++)
            {
                datas[i].value = 0;
            }

            zerar = false;
        }

        if (setDefaultValues)
        {
            datasDefault = new tasksCounterDataDefault[datas.Length];

            for (int i = 0; i < datasDefault.Length; i++)
            {
                datasDefault[i].name = datas[i].name;
                datasDefault[i].value = datas[i].value;
            }

            setDefaultValues = false;
        }
    }

    public void SetValue(string name, float _value)
    {
        int i = Array.FindIndex(datas, x => x.name == name);

        datas[i].value = _value;
    }

    public float GetTask(string name)
    {
        return datas.FirstOrDefault(x => x.name == name).value;
    }

    public void AddQtty(string name)
    {
        int i = Array.FindIndex(datas, x => x.name == name);

        datas[i].value += 1;

    }

    public void SaveAll()
    {
        for (int i = 0; i < datas.Length; i++)
        {
            QuickSaveWriter.Create("SaveData")
                .Write<float>(datas[i].name + ".tcvc", datas[i].value)
                .Commit();

        }
    }

    public void LoadAll()
    {
        for (int i = 0; i < datas.Length; i++)
        {
            QuickSaveReader.Create("SaveData")
                .Read<float>(datas[i].name + ".tcvc", (r) => { datas[i].value = r; });
        }
    }

    public void ResetAll()
    {
        for (int i = 0; i < datas.Length; i++)
        {
            QuickSaveWriter.Create("SaveData")
                .Write<float>(datas[i].name + ".tcvc", datasDefault[i].value)
                .Commit();

        }
    }
}
