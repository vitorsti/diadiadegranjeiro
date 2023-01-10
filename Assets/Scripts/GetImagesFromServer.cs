using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public static class ImagesFromServerMethods
{
    public static void DownloadImages()
    {
        GameObject.FindObjectOfType<GetImagesFromServer>().GetImages();
    }

    public static void LoadImages()
    {
        GameObject.FindObjectOfType<GetImagesFromServer>().LoadImages();
    }

    public static Texture2D GetTexture(Texture2D texture, int i)
    {
        Debug.Log("Geting images...");
        if (GameObject.FindObjectOfType<GetImagesFromServer>().textures.Length == 0)
            LoadImages();

        return texture = GameObject.FindObjectOfType<GetImagesFromServer>().textures[i];
    }

    public static Texture2D GetCourseLogo(Texture2D texture)
    {
        if (GameObject.FindObjectOfType<GetImagesFromServer>().logo == null)
            GameObject.FindObjectOfType<GetImagesFromServer>().LoadCourseLogo();

        return texture = GameObject.FindObjectOfType<GetImagesFromServer>().logo;
    }

    public static Texture2D GetSiloLogo(Texture2D texture)
    {
        if (GameObject.FindObjectOfType<GetImagesFromServer>().silo == null)
            GameObject.FindObjectOfType<GetImagesFromServer>().LoadSiloLogo();

        return texture = GameObject.FindObjectOfType<GetImagesFromServer>().silo;
    }

    public static Texture2D GetGeradorLogo(Texture2D texture)
    {
        if (GameObject.FindObjectOfType<GetImagesFromServer>().gerador == null)
            GameObject.FindObjectOfType<GetImagesFromServer>().LoadGeradorLogo();

        return texture = GameObject.FindObjectOfType<GetImagesFromServer>().gerador;
    }

    public static Texture2D GetCaixaAguaLogo(Texture2D texture)
    {
        if (GameObject.FindObjectOfType<GetImagesFromServer>().caixaAgua == null)
            GameObject.FindObjectOfType<GetImagesFromServer>().LoadCaixaAguaLogo();

        return texture = GameObject.FindObjectOfType<GetImagesFromServer>().caixaAgua;
    }

    public static string GetColors(string text)
    {
        if (GameObject.FindObjectOfType<GetImagesFromServer>().colorsServer == "" || GameObject.FindObjectOfType<GetImagesFromServer>().colorsServer == null)
            GameObject.FindObjectOfType<GetImagesFromServer>().LoadColors();
        return text = GameObject.FindObjectOfType<GetImagesFromServer>().colorsServer;
    }

    public static string GetLogoTimers(string text)
    {
        if (GameObject.FindObjectOfType<GetImagesFromServer>().logoTimers == "" || GameObject.FindObjectOfType<GetImagesFromServer>().logoTimers == null)
            GameObject.FindObjectOfType<GetImagesFromServer>().LoadLogoTimers();
        return text = GameObject.FindObjectOfType<GetImagesFromServer>().logoTimers;
    }

    public static string GetLogoUrls(string text)
    {
        if (GameObject.FindObjectOfType<GetImagesFromServer>().logoUrl == "" || GameObject.FindObjectOfType<GetImagesFromServer>().logoUrl == null)
            GameObject.FindObjectOfType<GetImagesFromServer>().LoadLogoUrl();
        return text = GameObject.FindObjectOfType<GetImagesFromServer>().logoUrl;
    }



}
public class GetImagesFromServer : MonoBehaviour
{
    public Texture2D[] textures;
    //public RawImage[] rawImages;
    public Texture2D loading;
    string serverUrl;
    public string path;
    string courseName;
    public string colorsServer;
    public string logoTimers;
    public string logoUrl;
    //public List<string> colors = new List<string>();
    //public Texture2D defaultLogo;
    public Texture2D logo;
    public Texture2D caixaAgua, silo, gerador;

    [SerializeField]
    Texture2D stored;
    [SerializeField]
    Texture2D update;

    [SerializeField]
    private bool checkImagesUpdate;
    // Start is called before the first frame update
    private void OnValidate()
    {
        if (checkImagesUpdate)
        {
            CheckImageUpdate(logo, "logo", serverUrl + courseName + "/");
            checkImagesUpdate = false;
        }
    }
    private void Awake()
    {
        //PlayerPrefs.SetString("course_name", "Yahp");
        GetImagesFromServer[] objs = GameObject.FindObjectsOfType<GetImagesFromServer>();

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        courseName = PlayerPrefs.GetString("course_name", "");
        Debug.Log(courseName);
        serverUrl = "https://yahp.prodb.com.br/";
        path = Path.Combine(Application.persistentDataPath, "images");

        if (PlayerPrefs.GetInt("FirstTimeRuning", 0) == 1)
        {

            LoadImages();
            LoadCourseLogo();
            LoadGeradorLogo();
            LoadSiloLogo();
            LoadCaixaAguaLogo();
            LoadColors();
            LoadLogoTimers();
            LoadLogoUrl();
            LoadColors();

            StartCoroutine(UpdateImages());
            StartCoroutine(UpdateTexts());
        }
        else
        {
            GetImages();
        }
    }
    void Start()
    {
        //GetImages();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.A))
        {
            GetImages();
        }
#endif
    }



    public void GetImages()
    {

        courseName = PlayerPrefs.GetString("course_name", "Yahp");

        courseName = courseName.Replace(" ", "");

        /*for (int i = 0; i < textures.Length; i++)
        {

            StartCoroutine(DownloadImage(serverUrl + courseName + "/image", i));
        }*/

        StartCoroutine(DownloadColors(serverUrl + courseName));

        //string logoname = "logo";
        StartCoroutine(DownloadLogo(serverUrl + courseName));

        //string caixaAguaName = "caixaAgua";
        StartCoroutine(DownloadCaixaAguaLogo(serverUrl));

        //string siloName = "silo";
        StartCoroutine(DownloadSiloLogo(serverUrl));

        //string geradorName = "gerador";
        StartCoroutine(DownloadGeradorLogo(serverUrl));


    }

    IEnumerator UpdateImages()
    {
        CheckImageUpdate(logo, "logo", serverUrl + courseName + "/");
        yield return new WaitForSeconds(0.1f);
        CheckImageUpdate(silo, "silo", serverUrl);
        yield return new WaitForSeconds(0.1f);
        CheckImageUpdate(gerador, "gerador", serverUrl);
        yield return new WaitForSeconds(0.1f);
        CheckImageUpdate(caixaAgua, "caixaAgua", serverUrl);

    }

    IEnumerator UpdateTexts()
    {
        CheckTextUpdate(logoTimers, "LogoTimers", serverUrl);
        yield return new WaitForSeconds(0.1f);
        CheckTextUpdate(logoUrl, "LogoUrl", serverUrl);
        yield return new WaitForSeconds(0.1f);
        CheckTextUpdate(colorsServer, "Cores", serverUrl + courseName);
    }

    IEnumerator DownloadLogo(string MediaUrl)
    {
        Debug.LogError("Downloading image...");
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl + "/logo.png");
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            logo = ((DownloadHandlerTexture)request.downloadHandler).texture;
            SaveLogoToDisk(logo, "logo");
        }
    }

    IEnumerator DownloadCaixaAguaLogo(string MediaUrl)
    {
        Debug.LogError("Downloading image...");
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl + "caixaAgua.png");
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            caixaAgua = ((DownloadHandlerTexture)request.downloadHandler).texture;
            SaveLogoToDisk(logo, "caixaAgua");
        }
    }

    IEnumerator DownloadSiloLogo(string MediaUrl)
    {
        Debug.LogError("Downloading image...");
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl + "silo.png");
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            caixaAgua = ((DownloadHandlerTexture)request.downloadHandler).texture;
            SaveLogoToDisk(silo, "silo");
        }
    }

    IEnumerator DownloadGeradorLogo(string MediaUrl)
    {
        Debug.LogError("Downloading image...");
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl + "gerador.png");
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            caixaAgua = ((DownloadHandlerTexture)request.downloadHandler).texture;
            SaveLogoToDisk(gerador, "gerador");
        }
    }

    IEnumerator DownloadColors(string MediaUrl)
    {
        Debug.LogError("Downloading image...");
        UnityWebRequest request = UnityWebRequest.Get(MediaUrl + "/Cores.txt");
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError(request.error);

        }
        else
        {

            colorsServer = request.downloadHandler.text;
            yield return new WaitForSeconds(0.2f);
            SaveColors();

        }
    }

    IEnumerator DownloadLogoTimers(string MediaUrl)
    {
        Debug.LogError("Downloading image...");
        UnityWebRequest request = UnityWebRequest.Get(MediaUrl + "/LogoTimers.txt");
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError(request.error);

        }
        else
        {

            logoTimers = request.downloadHandler.text;
            yield return new WaitForSeconds(0.2f);
            SaveLogoTimers();

        }
    }

    IEnumerator DownloadLogoUrl(string MediaUrl)
    {
        Debug.LogError("Downloading image...");
        UnityWebRequest request = UnityWebRequest.Get(MediaUrl + "/LogoUrl.txt");
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError(request.error);

        }
        else
        {

            logoUrl = request.downloadHandler.text;
            yield return new WaitForSeconds(0.2f);
            SaveLogoUrl();

        }
    }

    IEnumerator DownloadImage(string MediaUrl, int index)
    {
        Debug.LogError("Downloading image...");
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl + index + ".png");
        Debug.Log(MediaUrl + index + ".png");
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError(request.error);

        }
        else
        {
            textures[index] = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Debug.LogError("Image Downloaded!!");
            SaveImageToDisk(index);
        }
    }

    public void SaveColors()
    {
        string fileName = "/colors";
        File.WriteAllText(path + fileName, colorsServer);

    }

    public void SaveLogoTimers()
    {
        string fileName = "/logoTimers";
        File.WriteAllText(path + fileName, logoTimers);

    }
    public void SaveLogoUrl()
    {
        string fileName = "/logoUrl";
        File.WriteAllText(path + fileName, logoUrl);

    }

    public void SaveImageToDisk(int index)
    {
        byte[] textureBytes = textures[index].EncodeToPNG();
        string fileName = "/image" + index;
        File.WriteAllBytes(path + fileName, textureBytes);

        Debug.LogError("Image Saved!!");
    }

    public void SaveLogoToDisk(Texture2D texture, string name)
    {
        byte[] textureBytes = texture.EncodeToPNG();
        string fileName = "/" + name;
        File.WriteAllBytes(path + fileName, textureBytes);

        Debug.LogError("logo Saved!!");
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
                StartCoroutine(DownloadImage(serverUrl + courseName + "/image", i));
                Debug.Log("image failed to load");
            }
        }

    }

    public void LoadCourseLogo()
    {
        string fileName = "/logo";
        Debug.Log(path + fileName);
        if (File.Exists(path + fileName))
        {
            Debug.Log(path + fileName);
            byte[] textureByte = File.ReadAllBytes(path + fileName);
            Texture2D loadedTexture = new Texture2D(0, 0);
            ImageConversion.LoadImage(loadedTexture, textureByte, false);
            logo = loadedTexture;
            Debug.Log("Logo loaded!");
        }
        else
        {
            StartCoroutine(DownloadLogo(serverUrl + courseName));
            Debug.Log("image failed to load");
        }
    }

    public void LoadSiloLogo()
    {
        string fileName = "/silo";
        Debug.Log(path + fileName);
        if (File.Exists(path + fileName))
        {
            Debug.Log(path + fileName);
            byte[] textureByte = File.ReadAllBytes(path + fileName);
            Texture2D loadedTexture = new Texture2D(0, 0);
            ImageConversion.LoadImage(loadedTexture, textureByte, false);
            silo = loadedTexture;
            Debug.Log("Logo loaded!");
        }
        else
        {
            StartCoroutine(DownloadSiloLogo(serverUrl));
            Debug.Log("image failed to load");
        }
    }

    public void LoadGeradorLogo()
    {
        string fileName = "/gerador";
        Debug.Log(path + fileName);
        if (File.Exists(path + fileName))
        {
            Debug.Log(path + fileName);
            byte[] textureByte = File.ReadAllBytes(path + fileName);
            Texture2D loadedTexture = new Texture2D(0, 0);
            ImageConversion.LoadImage(loadedTexture, textureByte, false);
            gerador = loadedTexture;
            Debug.Log("Logo loaded!");
        }
        else
        {
            StartCoroutine(DownloadGeradorLogo(serverUrl));
            Debug.Log("image failed to load");
        }
    }

    public void LoadCaixaAguaLogo()
    {
        string fileName = "/caixaAgua";
        Debug.Log(path + fileName);
        if (File.Exists(path + fileName))
        {
            Debug.Log(path + fileName);
            byte[] textureByte = File.ReadAllBytes(path + fileName);
            Texture2D loadedTexture = new Texture2D(0, 0);
            ImageConversion.LoadImage(loadedTexture, textureByte, false);
            caixaAgua = loadedTexture;
            Debug.Log("Logo loaded!");
        }
        else
        {
            StartCoroutine(DownloadCaixaAguaLogo(serverUrl));
            Debug.Log("image failed to load");
        }
    }

    public void LoadColors()
    {
        string fileName = "/colors";
        if (File.Exists(path + fileName))
        {
            string text = File.ReadAllText(path + fileName);
            colorsServer = text;
            Debug.Log(text);

        }
        else
        {
            StartCoroutine(DownloadColors(serverUrl + courseName));
        }

    }

    public void LoadLogoTimers()
    {
        string fileName = "/logoTimers";
        if (File.Exists(path + fileName))
        {
            string text = File.ReadAllText(path + fileName);
            logoTimers = text;
            Debug.Log(text);

        }
        else
        {
            StartCoroutine(DownloadLogoTimers(serverUrl));
        }

    }

    public void LoadLogoUrl()
    {
        string fileName = "/logoUrl";
        if (File.Exists(path + fileName))
        {
            string text = File.ReadAllText(path + fileName);
            logoUrl = text;
            Debug.Log(text);

        }
        else
        {
            StartCoroutine(DownloadLogoUrl(serverUrl));
        }

    }

    public void CheckTextUpdate(string textStored, string fileName, string url)
    {
        string current = textStored;
        string update;
        StartCoroutine(DownloadText(url));

        IEnumerator DownloadText(string MediaUrl)
        {
            Debug.LogError("Downloading image...");
            UnityWebRequest request = UnityWebRequest.Get(MediaUrl + "/" + fileName + ".txt");
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError(request.error);

            }
            else
            {

                update = request.downloadHandler.text;
                if (!CompareText(current, update))
                {
                    switch (fileName)
                    {
                        case "LogoTimers":
                            logoTimers = update;
                            DeleteFile("/logoTimers");
                            SaveLogoTimers();
                            break;
                        case "LogoUrl":
                            logoUrl = update;
                            DeleteFile("/logoUrl");
                            SaveLogoUrl();
                            break;
                        case "Cores":
                            colorsServer = update;
                            DeleteFile("/colors");
                            SaveColors();
                            break;
                    }
                   
                }
                yield return new WaitForSeconds(0.2f);
                //SaveLogoUrl();

            }
        }
    }

    public void CheckImageUpdate(Texture2D texture, string fileName, string url)
    {
        update = null;
        stored = null;

        stored = texture;
        //Download Updated Texture
        StartCoroutine(DownloadUpdate(url));

        IEnumerator DownloadUpdate(string MediaUrl)
        {
            Debug.LogError("Downloading image...");
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl + fileName + ".png");
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError(request.error);
            }
            else
            {


                update = ((DownloadHandlerTexture)request.downloadHandler).texture;

                //Compare Textures
                if (!CompareTexture(stored, update))
                {
                    DeleteFile("/" + fileName);
                    SaveLogoToDisk(update, fileName);

                }
            }
        }
    }
    private bool CompareTexture(Texture2D first, Texture2D second)
    {
        Color[] firstPix = first.GetPixels();
        Color[] secondPix = second.GetPixels();
        if (firstPix.Length != secondPix.Length)
        {
            return false;
        }
        for (int i = 0; i < firstPix.Length; i++)
        {
            if (firstPix[i] != secondPix[i])
            {
                return false;
            }
        }

        return true;
    }

    private bool CompareText(string text1, string text2)
    {
        if (text1 != text2)
            return false;
        else
            return true;
    }
    public void DeleteFile(string fileName)
    {
        if (File.Exists(path + fileName))
        {
            File.Delete(path + fileName);
            Debug.Log("File deleted");
        }
    }
    // public void CreateFolder()
    // {

    //     if (!Directory.Exists(Application.persistentDataPath + "/images"))
    //     {

    //         Directory.CreateDirectory(Application.persistentDataPath + "/images");
    //         Debug.LogError("Folder Created!!");
    //     }
    //     else
    //     {
    //         Debug.LogError("Folder Already Exists!!");
    //     }

    // }

    // Update is called once per frame

}
