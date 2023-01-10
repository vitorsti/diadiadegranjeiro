using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[CreateAssetMenu(fileName = "ShopContainer", menuName = "ScriptableObject/shopData")]
public class ShopItensContainer : ScriptableObject
{
    /*[Serializable]
    public struct itensData { public string name; public GameObject itenPrefab;}
    public itensData[] datas;*/

    public GameObject[] itens;
    

}
