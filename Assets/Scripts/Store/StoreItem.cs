using UnityEngine;
using EventProperties;
using System;
using System.Linq;
using CI.QuickSave;

[CreateAssetMenu(fileName = "StoreItem", menuName = "ScriptableObject/StoreItem")]
public class StoreItem : ScriptableObject
{
    [Serializable]
    public struct shopData
    {
        public int id;
        public Region region;
        public string nome;

        public RegionValuesContainer regionData;

        [Header("Preço")]
        public float healthPrice;
        public float maxHealthPrice;
        public float regionGapPrice;

        /* [Header("Estagio vida")]
         public int healthStage;
         public int healthMax;*/

        [Header("Estagio vida máxima")]
        public int maxHealthStage;
        public int maxHealthMax;

        [Header("Estagio gap")]
        public int regionGapStage;
        public int regionGapMax;

        [Header("Aumento")]
        //public float healthUp;
        public float maxHealthUp;
        public float regionGapUp;

    }

    [Serializable]
    public struct shopDataDefault
    {
        public int id;
        public Region region;
        public string nome;

        public RegionValuesContainer regionData;

        [Header("Preço")]
        public float healthPrice;
        public float maxHealthPrice;
        public float regionGapPrice;

        /* [Header("Estagio vida")]
         public int healthStage;
         public int healthMax;*/

        [Header("Estagio vida máxima")]
        public int maxHealthStage;
        public int maxHealthMax;

        [Header("Estagio gap")]
        public int regionGapStage;
        public int regionGapMax;

        [Header("Aumento")]
        //public float healthUp;
        public float maxHealthUp;
        public float regionGapUp;

    }

    [Header("----------")]
    public string index;
    public string number;
    public enum things { pVida, pMaxVida, pGap, maxVidaStage, maxVidaMax, gapStage, gapMax, maxUp, gapUp }
    public things chooseWhatToCahnge;
    public bool apply;
    [Header("----------")]


    public shopData[] datas;
    public shopDataDefault[] datasDefault;

    public bool setDefaultValues;

    private void OnValidate()
    {

        for (int i = 0; i < datas.Length; i++)
        {

            datas[i].id = i;
            datas[i].regionData = Resources.Load<RegionValuesContainer>("RegionData");
            datas[i].healthPrice = 100;

            if (datas[i].maxHealthUp == 0)
                datas[i].maxHealthUp = 1;

            if (datas[i].regionGapUp == 0)
                datas[i].regionGapUp = 1;

        }

        if (apply)
            Set();
        
        if (setDefaultValues)
        {
            datasDefault = new shopDataDefault[datas.Length];
            for (int i = 0; i < datasDefault.Length; i++)
            {
                datasDefault[i].id = datas[i].id;
                datasDefault[i].region = datas[i].region;
                datasDefault[i].nome = datas[i].nome;
                datasDefault[i].regionData = datas[i].regionData;
                datasDefault[i].healthPrice = datas[i].healthPrice;
                datasDefault[i].maxHealthPrice = datas[i].maxHealthPrice;
                datasDefault[i].regionGapPrice = datas[i].regionGapPrice;
                datasDefault[i].maxHealthStage = datas[i].maxHealthStage;
                datasDefault[i].maxHealthMax = datas[i].maxHealthMax;
                datasDefault[i].regionGapStage = datas[i].regionGapStage;
                datasDefault[i].regionGapMax = datas[i].regionGapMax;
                datasDefault[i].maxHealthUp = datas[i].maxHealthUp;
                datasDefault[i].regionGapUp = datas[i].regionGapUp;
            }
            setDefaultValues = false;
        }
    }

    void Set()
    {
        int i = int.Parse(index);
        int n = int.Parse(number);
        switch (chooseWhatToCahnge)
        {
            case things.pVida:
                datas[i].healthPrice = n;
                break;
            case things.pMaxVida:
                datas[i].maxHealthPrice = n;
                break;
            case things.pGap:
                datas[i].regionGapPrice = n;
                break;
            case things.maxVidaStage:
                datas[i].maxHealthStage = n;
                break;
            case things.maxVidaMax:
                datas[i].maxHealthMax = n;
                break;
            case things.gapStage:
                datas[i].regionGapStage = n;
                break;
            case things.gapMax:
                datas[i].regionGapMax = n;
                break;
            case things.maxUp:
                datas[i].maxHealthUp = n;
                break;
            case things.gapUp:
                datas[i].regionGapUp = n;
                break;

        }

        apply = false;
    }

    public void BuyHealth(int _id)
    {
        int i = Array.FindIndex(datas, x => x.id == _id);

        if (MoneyManager.GetMoney("cash") >= datas[i].healthPrice)
        {
            MoneyManager.RemoveMoney("cash", datas[i].healthPrice);
            datas[i].regionData.SetHealth(datas[i].region.ToString(), datas[i].regionData.GetMaxHealth(datas[i].region.ToString()));
        }
        else
            return;
    }

    public void BuyMaxHealthUpgrade(int _id)
    {
        int j = Array.FindIndex(datas, x => x.id == _id);

        if (datas[j].maxHealthStage == datas[j].maxHealthMax || MoneyManager.GetMoney("cash") <= datas[j].maxHealthPrice)
        {
            return;
        }
        else
        {
            MoneyManager.RemoveMoney("cash", datas[j].maxHealthPrice);

            datas[j].maxHealthPrice *= datas[j].maxHealthUp;
            datas[j].maxHealthStage++;

            float i = datas[j].regionData.GetMaxHealth(datas[j].region.ToString());
            i *= datas[j].maxHealthUp;
            datas[j].regionData.SetMaxHealth(datas[j].region.ToString(), i);
        }
    }

    public void BuyRegionGapUpgrade(int _id)
    {
        int j = Array.FindIndex(datas, x => x.id == _id);

        if (datas[j].regionGapStage == datas[j].regionGapMax || MoneyManager.GetMoney("cash") <= datas[j].regionGapPrice)
        {
            return;
        }
        else
        {
            MoneyManager.RemoveMoney("cash", datas[j].regionGapPrice);

            datas[j].regionGapPrice *= datas[j].regionGapUp;
            datas[j].regionGapStage++;

            float i = datas[j].regionData.GetRegionGap(datas[j].region.ToString());
            i -= datas[j].regionGapUp;
            datas[j].regionData.SetRegionGap(datas[j].region.ToString(), i);
        }
    }

    public void SaveAll()
    {
        for (int i = 0; i < datas.Length; i++)
        {
            QuickSaveWriter.Create(this.name)
                .Write<float>(datas[i].id + datas[i].region.ToString() +".hp", datas[i].healthPrice)
                .Write<float>(datas[i].id + datas[i].region.ToString() +".mhp", datas[i].maxHealthPrice)
                .Write<float>(datas[i].id + datas[i].region.ToString() +".rgp", datas[i].regionGapPrice)
                .Write<int>(datas[i].id + datas[i].region.ToString() +".mhs", datas[i].maxHealthStage)
                .Write<int>(datas[i].id + datas[i].region.ToString() +".mhm", datas[i].maxHealthMax)
                .Write<int>(datas[i].id + datas[i].region.ToString() +".rgs", datas[i].regionGapStage)
                .Write<int>(datas[i].id + datas[i].region.ToString() +".rgm", datas[i].regionGapMax)
                .Write<float>(datas[i].id + datas[i].region.ToString() +".mhu", datas[i].maxHealthUp)
                .Write<float>(datas[i].id + datas[i].region.ToString() +".rgu", datas[i].regionGapUp)
                .Commit();
        }
    }

    public void LoadAll()
    {
        for (int i = 0; i < datas.Length; i++)
        {
            QuickSaveReader.Create(this.name)
                .Read<float>(datas[i].id + datas[i].region.ToString() +".hp", (r) => {datas[i].healthPrice= r;})
                .Read<float>(datas[i].id + datas[i].region.ToString() +".mhp", (r) => {datas[i].maxHealthPrice= r;})
                .Read<float>(datas[i].id + datas[i].region.ToString() +".rgp", (r) => {datas[i].regionGapPrice= r;})
                .Read<int>(datas[i].id + datas[i].region.ToString() +".mhs", (r) => {datas[i].maxHealthStage= r;})
                .Read<int>(datas[i].id + datas[i].region.ToString() +".mhm", (r) => {datas[i].maxHealthMax= r;})
                .Read<int>(datas[i].id + datas[i].region.ToString() +".rgs", (r) => {datas[i].regionGapStage= r;})
                .Read<int>(datas[i].id + datas[i].region.ToString() +".rgm", (r) => {datas[i].regionGapMax= r;})
                .Read<float>(datas[i].id + datas[i].region.ToString() +".mhu", (r) => {datas[i].maxHealthUp= r;})
                .Read<float>(datas[i].id + datas[i].region.ToString() +".rgu", (r) => {datas[i].regionGapUp = r;});
        }
    }

    public void ResetAll(){
        for (int i = 0; i < datas.Length; i++)
        {
            QuickSaveWriter.Create(this.name)
                .Write<float>(datas[i].id + datas[i].region.ToString() +".hp", datasDefault[i].healthPrice)
                .Write<float>(datas[i].id + datas[i].region.ToString() +".mhp", datasDefault[i].maxHealthPrice)
                .Write<float>(datas[i].id + datas[i].region.ToString() +".rgp", datasDefault[i].regionGapPrice)
                .Write<int>(datas[i].id + datas[i].region.ToString() +".mhs", datasDefault[i].maxHealthStage)
                .Write<int>(datas[i].id + datas[i].region.ToString() +".mhm", datasDefault[i].maxHealthMax)
                .Write<int>(datas[i].id + datas[i].region.ToString() +".rgs", datasDefault[i].regionGapStage)
                .Write<int>(datas[i].id + datas[i].region.ToString() +".rgm", datasDefault[i].regionGapMax)
                .Write<float>(datas[i].id + datas[i].region.ToString() +".mhu", datasDefault[i].maxHealthUp)
                .Write<float>(datas[i].id + datas[i].region.ToString() +".rgu", datasDefault[i].regionGapUp)
                .Commit();
        }
    }

}
