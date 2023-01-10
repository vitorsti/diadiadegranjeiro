using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetColorsAndLogo : MonoBehaviour
{
    public RawImage logo;
    Texture2D loaded;
    public string colorsString;
    public List<string> colors = new List<string>();
    public Color primaryColor, secondaryColor;
    public GameObject[] primaryImages;
    public GameObject[] secondaryImages;

    // Start is called before the first frame update
    void Awake()
    {
        primaryImages = GameObject.FindGameObjectsWithTag("PrimaryColorImage");
        secondaryImages = GameObject.FindGameObjectsWithTag("SecondaryColorImage");
    }

    // Update is called once per frame
    void Start()
    {
        SetLogo();
        SetColors();
    }

    void SetLogo()
    {

        loaded = ImagesFromServerMethods.GetCourseLogo(loaded);
        logo.texture = loaded;
    }

    void SetColors()
    {
        colorsString = ImagesFromServerMethods.GetColors(colorsString);
        string[] splited = colorsString.Split('.');

        if (colors.Count > 0)
            colors.Clear();

        for (int i = 0; i < splited.Length; i++)
        {
            colors.Add(splited[i]);
        }

        string primary = colors[0].Replace("primaria", "");
        string secondary = colors[1].Replace("secundaria", "");

        ColorUtility.TryParseHtmlString(primary, out primaryColor);
        ColorUtility.TryParseHtmlString(secondary, out secondaryColor);

        foreach (GameObject i in primaryImages)
        {
            i.GetComponent<Image>().color = primaryColor;
        }
        foreach (GameObject i in secondaryImages)
        {
            i.GetComponent<Image>().color = secondaryColor;
        }
    }
}
