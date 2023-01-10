using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using CI.QuickSave;

[CreateAssetMenu(fileName = "MinigameData", menuName = "ScriptableObject/minigameData")]
public class MinigamesValuesContainers : ScriptableObject
{
    [Serializable]
    public struct minigameData { public string name; public float value; }
    public minigameData[] datas;

    [Serializable]
    public struct minigameDataDefault { public string name; public float value; }
    public minigameDataDefault[] datasDefault;

    public bool setDefaultValues;

    private void OnValidate()
    {
        if (setDefaultValues)
        {
            datasDefault = new minigameDataDefault[datas.Length];
            for (int i = 0; i < datasDefault.Length; i++)
            {
                datasDefault[i].name = datas[i].name;
                datasDefault[i].value = datas[i].value;
            }
            setDefaultValues = false;
        }
    }

    public void SetData(string name, float _value)
    {
        int i = Array.FindIndex(datas, x => x.name == name);

        datas[i].value = _value;

    }

    public float GetData(string name)
    {

        return datas.FirstOrDefault(x => x.name == name).value;

    }

    public void SaveAll()
    {
        for (int i = 0; i < datas.Length; i++)
        {
            QuickSaveWriter.Create("MinigamesData")
                .Write<float>(this.name + "." + datas[i].name, datas[i].value)
                .Commit();
        }
    }

    public void LoadAll()
    {
        for (int i = 0; i < datas.Length; i++)
        {
            QuickSaveReader.Create("MinigamesData")
                .Read<float>(this.name + "." + datas[i].name, (r) => { datas[i].value = r; });
        }

    }

    public void ResetAll()
    {
        for (int i = 0; i < datas.Length; i++)
        {
            QuickSaveWriter.Create("MinigamesData")
                .Write<float>(this.name + "." + datas[i].name, datasDefault[i].value)
                .Commit();
        }
    }


}
