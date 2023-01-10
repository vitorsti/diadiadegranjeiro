using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System;
using System.Linq;
using CI.QuickSave;

[CreateAssetMenu(fileName = "TutorialsData", menuName = "ScriptableObject/tutorialsData")]
public class TutorialsContainer : ScriptableObject
{
    public enum select { video, image, none }
    [Serializable]
    public struct TutorialsData { public string name; [TextArea] public string description; public Sprite image; public VideoClip video; public int id; public select videoOrImage; public bool enablePointer; public GameObject pointer; public Vector3 pointerPosition; public bool useCustomPosition; public Vector3 customPosition;}
    public TutorialsData[] datas;
    public bool firstTime;

    public bool resetFistTime;

    void OnValidate()
    {
        for (int i = 0; i < datas.Length; i++)
        {
            datas[i].id = i;

        }

        if (resetFistTime)
        {
            PlayerPrefs.SetInt(this.name, 1);
            PlayerPrefs.Save();
            firstTime = true;
            resetFistTime = false;
        }

    }

    public string GetDescrition(int _id)
    {

        return datas.FirstOrDefault(x => x.id == _id).description;
    }

    public Sprite GetImage(int _id)
    {
        return datas.FirstOrDefault(x => x.id == _id).image;
    }

    public VideoClip GetVideo(int _id)
    {
        return datas.FirstOrDefault(x => x.id == _id).video;
    }

    public select IsIamgeOrVideo(int _id)
    {

        return datas.FirstOrDefault(x => x.id == _id).videoOrImage;
    }

    public void SetFirstTime(bool set)
    {

        firstTime = set;

        if (firstTime == false)
            PlayerPrefs.SetInt(this.name, 0);
        if (firstTime == true)
            PlayerPrefs.SetInt(this.name, 1);
    }

    public bool GetFirstTime()
    {
        float f = PlayerPrefs.GetInt(this.name, 1);

        if (f == 0)
            firstTime = false;
        else
            firstTime = true;

        //Debug.Log(f);

        return firstTime;
    }

    public bool GetEnablePointer(int _id)
    {
        return datas.FirstOrDefault(x => x.id == _id).enablePointer;
    }

    public GameObject GetPointer(int _id)
    {
        return datas.FirstOrDefault(x => x.id == _id).pointer;
    }

    public Vector3 GetPointerPosition(int _id)
    {
        return datas.FirstOrDefault(x => x.id == _id).pointerPosition;
    }
    public bool GetUseCustomPosition(int _id)
    {
        return datas.FirstOrDefault(x => x.id == _id).useCustomPosition;
    }
    public Vector3 GetCustomPosition(int _id)
    {
        return datas.FirstOrDefault(x => x.id == _id).customPosition;
    }

    public void Load()
    {

    }


}
