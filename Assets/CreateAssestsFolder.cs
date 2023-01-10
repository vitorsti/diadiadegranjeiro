using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CreateAssestsFolder : MonoBehaviour
{
    private void Awake()
    {
        CreateFolder();
    }
    public void CreateFolder()
    {

        if (!Directory.Exists(Application.persistentDataPath + "/images"))
        {

            Directory.CreateDirectory(Application.persistentDataPath + "/images");
            Debug.LogError("Folder Created!!");
        }
        else
        {
            Debug.LogError("Folder Already Exists!!");
        }

    }

    public void Deletefolder()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/images"))
        {

            Directory.Delete(Application.persistentDataPath + "/images");
            Debug.LogError("Folder Deleted!!");
        }
        else
        {
            Debug.LogError("Folder does not Exists!!");
        }
    }
}
