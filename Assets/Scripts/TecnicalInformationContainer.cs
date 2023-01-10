using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[CreateAssetMenu(fileName = "TecnicalInformationData", menuName = "ScriptableObject/tecnicalInformationData")]
public class TecnicalInformationContainer : ScriptableObject
{
    [Serializable]
    public struct tecnicalInformationData { public string name; public int id;[TextArea] public string description;public string href; public string browserTxt;}
    public tecnicalInformationData[] datas;

    private void OnValidate() {

        for (int i = 0; i < datas.Length; i++)
        {

            datas[i].id = i;
            datas[i].href = datas[i].name.Replace(" ", "_");
            datas[i].browserTxt = "https://www.embrapa.br/artigo_" + datas[i].href;
        }
    }

    public string GetName(string _name)
    {
        return datas.FirstOrDefault(x => x.name == _name).name;
    }


    public string GetDescrition(int _id)
    {
        
        return datas.FirstOrDefault(x => x.id == _id).description;
    }

    public string GetBrowserTxt(int _id)
    {
        
        return datas.FirstOrDefault(x => x.id == _id).browserTxt;
    }

}
