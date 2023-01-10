using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetCustomImages : MonoBehaviour
{

    //public SpriteRenderer[] images;
    //public Texture2D[] textures;
    // public SpriteRenderer ;// caixaAguaAviario;
    public Texture2D siloTexture, geradorTexture, caixaAguaTexture;
    public RawImage silo1, silo2, gerador, caixaAguaRodo, caixaAguaAviario;
    public Texture2D defaultLogo;

    public bool load;

    private void OnValidate()
    {
        if (load)
        {
            LoadImages();
            load = false;
        }
    }
    // Start is called before the first frame update
    private void Awake()
    {



    }
    void Start()
    {
        LoadImages();
        //SetImages();
        SetSiloImages();
        SetGeradorImages();
        SetSiloImages();
        SetCaixaAguaImages();

    }

    void LoadImages()
    {
        /*for (int i = 0; i < textures.Length; i++)
        {
            textures[i] = ImagesFromServerMethods.GetTexture(textures[i], i);
        }*/

        siloTexture = ImagesFromServerMethods.GetSiloLogo(siloTexture);
        geradorTexture = ImagesFromServerMethods.GetGeradorLogo(geradorTexture);
        caixaAguaTexture = ImagesFromServerMethods.GetCaixaAguaLogo(caixaAguaTexture);
    }

   /* void SetImages()
    {
        for (int i = 0; i < images.Length; i++)
        {
            if (textures[i] != null)
            {
                Sprite loaded = Sprite.Create(textures[i], new Rect(0, 0, textures[i].width, textures[i].height), new Vector2(0.5f, 0.5f), 100);
                images[i].sprite = loaded;
                images[i].gameObject.SetActive(true);
            }
            else
            {
                images[i].sprite = defaultLogo;
            }

        }
    }*/

    void SetGeradorImages()
    {
        if (geradorTexture != null)
        {
            //Sprite loaded = Sprite.Create(geradorTexture, new Rect(0, 0, geradorTexture.width, geradorTexture.height), new Vector2(0.5f, 0.5f), 100);
            gerador.texture = geradorTexture;
            //gerador.gameObject.SetActive(true);
        }
        else
        {
            gerador.texture = defaultLogo;
            //gerador.gameObject.SetActive(true);
        }
    }

    void SetSiloImages()
    {
        if (siloTexture != null)
        {
            //Sprite loaded = Sprite.Create(siloTexture, new Rect(0, 0, siloTexture.width, siloTexture.height), new Vector2(0.5f, 0.5f), 100);
            silo1.texture = siloTexture;
            //silo1.gameObject.SetActive(true);
            silo2.texture = siloTexture;
            //silo2.gameObject.SetActive(true);
        }
        else
        {
            silo1.texture = defaultLogo;
            silo2.texture = defaultLogo;
            //silo1.gameObject.SetActive(true);
            //silo2.gameObject.SetActive(true);
        }
    }

    void SetCaixaAguaImages()
    {
        if (caixaAguaTexture != null)
        {
            //Sprite loaded = Sprite.Create(caixaAguaTexture, new Rect(0, 0, caixaAguaTexture.width, caixaAguaTexture.height), new Vector2(0.5f, 0.5f), 100);
            caixaAguaAviario.texture = caixaAguaTexture;
            //caixaAguaAviario.gameObject.SetActive(true);
            caixaAguaRodo.texture = caixaAguaTexture;
            //caixaAguaRodo.gameObject.SetActive(true);
        }
        else
        {
            caixaAguaAviario.texture = defaultLogo;
            //caixaAguaAviario.gameObject.SetActive(true);
            caixaAguaRodo.texture = defaultLogo;
        }
    }
}
