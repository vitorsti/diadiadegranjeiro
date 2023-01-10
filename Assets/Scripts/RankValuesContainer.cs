using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

[CreateAssetMenu(fileName = "RankData", menuName = "ScriptableObject/rankData")]
public class RankValuesContainer : ScriptableObject
{
    [Serializable]
    public struct rankData { public Sprite rankImage; public string rankName; public float r0; public float r1; public int id; }
    public float rank;
    public float reward;

    public float conPelnaty, subPenalty, megaPenalty;
    public rankData[] datas;
    
    void OnValidate()
    {
        for (int i = 0; i < datas.Length; i++)
        {

            datas[i].id = i;
        }

        if (rank <= 0)
            rank = 0;

    }

    void OnEnable()
    {
        
    }

    public void SetRank(float _value)
    {
        rank += _value;

        if (rank <= 0)
            rank = 0;
    }

    /*public float GetRank()
    {
        return rank;
    }*/

    /*public void SetValue(int _id, int _value)
    {
        int i = Array.FindIndex(datas, x => x.id == _id);

        datas[i].requiredRank = _value;
    }*/

    public float GetRangeA(int _id)
    {
        return datas.FirstOrDefault(x => x.id == _id).r0;
    }

    public float GetRangeB(int _id)
    {
        return datas.FirstOrDefault(x => x.id == _id).r1;
    }

    public string GetRankName(int _id)
    {
        return datas.FirstOrDefault(x => x.id == _id).rankName;
    }

    public Sprite GetSprite(int _id)
    {
        return datas.FirstOrDefault(x => x.id == _id).rankImage;
    }

    public float GetRank(){
        //rank = PlayGamesMethods.GetScore();
        rank = 10;
        return rank;
    }
}
