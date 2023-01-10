using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using EventProperties;
using CI.QuickSave;

[CreateAssetMenu(fileName = "RegionData", menuName = "ScriptableObject/regionData")]
public class RegionValuesContainer : ScriptableObject
{
    [Serializable]
    public struct regionData { public string name; public float health, maxHealth, regionGap, healthModify; public int isHealthy; }
    public regionData[] datas;

    [Serializable]
    public struct regionDataDefault { public string name; public float health, maxHealth, regionGap, healthModify; public int isHealthy; }
    public regionDataDefault[] datasDefault;

    public bool setDefaultValues;

    private void OnValidate()
    {

        if (setDefaultValues)
        {
            datasDefault = new regionDataDefault[datas.Length];

            for (int i = 0; i < datasDefault.Length; i++)
            {
                datasDefault[i].name = datas[i].name;
                datasDefault[i].health = datas[i].health;
                datasDefault[i].maxHealth = datas[i].maxHealth;
                datasDefault[i].regionGap = datas[i].regionGap;
                datasDefault[i].healthModify = datas[i].healthModify;
                datasDefault[i].isHealthy = datas[i].isHealthy;
            }
            setDefaultValues = false;
        }
    }

    public void SetHealth(string name, float _health)
    {
        int i = Array.FindIndex(datas, x => x.name == name);

        datas[i].health = _health;

        //Save(name, _health);
    }

    public void SetMaxHealth(string name, float _maxHealth)
    {
        int i = Array.FindIndex(datas, x => x.name == name);

        datas[i].maxHealth = _maxHealth;

        //Save(name, _maxHealth);
    }

    public void SetRegionGap(string name, float _regionGap)
    {
        int i = Array.FindIndex(datas, x => x.name == name);

        datas[i].regionGap = _regionGap;

        //Save(name, _regionGap);
    }

    public void SetHealthModfy(string name, float _healthModfy)
    {
        int i = Array.FindIndex(datas, x => x.name == name);

        datas[i].healthModify = _healthModfy;

        //Save(name, _healthModfy);
    }

    public void SetHealthStatus(string name, int _isHealthy)
    {
        int i = Array.FindIndex(datas, x => x.name == name);

        datas[i].isHealthy = _isHealthy;

        //Save(name, _isHealthy);
    }

    public float GetRegionGap(string name)
    {
        return datas.FirstOrDefault(x => x.name == name).regionGap;
    }

    public float GetHealthModfy(string name)
    {
        return datas.FirstOrDefault(x => x.name == name).healthModify;
    }

    public float GetHealth(string name)
    {
        return datas.FirstOrDefault(x => x.name == name).health;

    }

    public float GetMaxHealth(string name)
    {
        return datas.FirstOrDefault(x => x.name == name).maxHealth;

    }

    public int GetHealthStatus(string name)
    {
        return datas.FirstOrDefault(x => x.name == name).isHealthy;
    }

    void Save<T>(string name, T value)
    {
        QuickSaveWriter.Create(this.name)
        .Write(name, value)
        .Commit();

        //Debug.Log(value);
    }

    public void SaveAll()
    {

        for (int i = 0; i < datas.Length; i++)
        {
            QuickSaveWriter.Create(this.name)
                            .Write<float>(datas[i].name + ".h", datas[i].health)
                            .Write<float>(datas[i].name + ".mH", datas[i].maxHealth)
                            .Write<float>(datas[i].name + ".rG", datas[i].regionGap)
                            .Write<float>(datas[i].name + ".hM", datas[i].healthModify)
                            .Write<int>(datas[i].name + ".iH", datas[i].isHealthy)
                            .Commit();

        }

        Debug.Log(QuickSaveRaw.LoadString(this.name + ".json"));

    }

    public void LoadAll()
    {
        for (int i = 0; i < datas.Length; i++)
        {
            QuickSaveReader.Create(this.name)
                            .Read<float>(datas[i].name + ".h", (r) => { datas[i].health = r; })
                            .Read<float>(datas[i].name + ".mH", (r) => { datas[i].maxHealth = r; })
                            .Read<float>(datas[i].name + ".rG", (r) => { datas[i].regionGap = r; })
                            .Read<float>(datas[i].name + ".hM", (r) => { datas[i].healthModify = r; })
                            .Read<int>(datas[i].name + ".iH", (r) => { datas[i].isHealthy = r; });


        }

    }

    public void ResetAll()
    {

        for (int i = 0; i < datas.Length; i++)
        {
            QuickSaveWriter.Create(this.name)
                            .Write<float>(datas[i].name + ".h", datasDefault[i].health)
                            .Write<float>(datas[i].name + ".mH", datasDefault[i].maxHealth)
                            .Write<float>(datas[i].name + ".rG", datasDefault[i].regionGap)
                            .Write<float>(datas[i].name + ".hM", datasDefault[i].healthModify)
                            .Write<int>(datas[i].name + ".iH", datasDefault[i].isHealthy)
                            .Commit();

        }
    }


}
