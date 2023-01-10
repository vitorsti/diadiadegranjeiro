using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;


public class ImageFromServerTest : MonoBehaviour
{
  public Texture2D[] textures;
    public RawImage[] rawImages;
    public Texture2D loading;
    string serverUrl;
    public string path;
    string courseName;
    // Start is called before the first frame update
    private void Awake()
    {
        //CreateFolder();
        courseName = PlayerPrefs.GetString("course_name", "");
        serverUrl = "https://yahp.prodb.com.br/";
        path = Path.Combine(Application.persistentDataPath, "images");
        TestScene();
       
    }
    void Start()
    {
        //GetImages();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.D))
        {
            GetImages();
        }
#endif
    }

    public void TestScene()
    {
        rawImages = new RawImage[textures.Length];
        rawImages = FindObjectsOfType<RawImage>();
        for (int i = 0; i < rawImages.Length; i++)
        {
            rawImages[i].gameObject.SetActive(false);
        }
    }

    public void GetImages()
    {
        /*if (File.Exists(path + "/image"))
        {
            for (int i = 0; i < textures.Length; i++)
            {
                LoadImages();
            }
        }*/


        courseName = PlayerPrefs.GetString("course_name");
        courseName = courseName.Replace(" ", "");
        for (int i = 0; i < textures.Length; i++)
        {

            StartCoroutine(DownloadImage(serverUrl + courseName + "/image", i));
        }
    }


    IEnumerator DownloadImage(string MediaUrl, int index)
    {
        Debug.LogError("Downloading image...");
        rawImages[index].gameObject.SetActive(true);
        rawImages[index].texture = loading;
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl + index + ".png");
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError(request.error);
            //GetImageFromDiskButtonClick();
        }
        else
        {
            textures[index] = ((DownloadHandlerTexture)request.downloadHandler).texture;
            rawImages[index].texture = textures[index];
            Debug.LogError("Image Downloaded!!");
            SaveImageToDisk(index);
        }
    }

    public void SaveImageToDisk(int index)
    {

        byte[] textureBytes = textures[index].EncodeToPNG();
        string fileName = "/image" + index;
        File.WriteAllBytes(path + fileName, textureBytes);

        Debug.LogError("Image Saved!!");
    }

    public void LoadImages()
    {

        for (int i = 0; i < textures.Length; i++)
        {
            string fileName = "/image" + i;
            Debug.Log(path + fileName);
            if (File.Exists(path + fileName))
            {
                Debug.Log(path + fileName);
                byte[] textureByte = File.ReadAllBytes(path + fileName);
                Texture2D loadedTexture = new Texture2D(0, 0);
                ImageConversion.LoadImage(loadedTexture, textureByte, false);
                textures[i] = loadedTexture;
                Debug.Log("Image " + i + " loaded!");
            }
            else
            {
                //StartCoroutine(DownloadImage(serverUrl + courseName + "/image", i));
                Debug.Log("image failed to load");
            }
        }

    }
}
