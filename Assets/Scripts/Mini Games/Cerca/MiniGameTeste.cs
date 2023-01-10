using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using EventProperties;

public class MiniGameTeste : MonoBehaviour
{
    public Region region;
    public GameObject tarefa, matchScreen, textEverythinIsOk;
    [SerializeField]
    float moneyToRemove;
    public Sprite cerca0, cerca1, cerca2;
    public Slider regionHealthSlider;
    public List<Image> leftImages = new List<Image>();
    public List<Image> rightImages = new List<Image>();
    public int[] intsLeft;
    public int[] intsRigth;
    /*public int n;
    public int quantasVezesApssouPeloArray;*/
    public int[,] m = new int[6, 3];
    int venceu;
    public float h, maxH;
    public RegionValuesContainer regionData;
    public GameObject lineRenderer;

    void Awake()
    {
        regionData = Resources.Load<RegionValuesContainer>("RegionData");

        h = regionData.GetHealth(region.ToString());
        maxH = regionData.GetMaxHealth(region.ToString());

        regionHealthSlider.maxValue = maxH;
        regionHealthSlider.value = h;

        if (maxH > h)
        {
            textEverythinIsOk.SetActive(false);
            matchScreen.SetActive(true);
            FillArays();
        }
        else if (h == maxH)
        {
            textEverythinIsOk.SetActive(true);
            matchScreen.SetActive(false);
        }

        SetBgImage();
    }

    public void MoneyToRemove(float _moneyToRemove)
    {
        MoneyManager.RemoveMoney("gold", _moneyToRemove);
    }

    private bool IsAllGree()
    {
        for (int i = 0; i < leftImages.Count; ++i)
        {
            if (leftImages[i].color != Color.green)
            {
                return false;
            }
        }

        return true;
    }

    public void MatchLeft(int index)
    {
        leftImages[index].color = Color.yellow;

    }

    public void MatchRigth(int index)
    {
        rightImages[index].color = Color.yellow;
    }

    public void Apply()
    {
        for (int i = 0; i < intsLeft.Length; i++)
        {

            for (int j = 0; j < intsRigth.Length; j++)
            {
                if (intsLeft[i] == intsRigth[j] && leftImages[i].color == Color.yellow && rightImages[j].color == Color.yellow)
                {
                    leftImages[i].color = Color.green;
                    rightImages[j].color = Color.green;

                    //Draw Line
                    GameObject line = Instantiate(lineRenderer, leftImages[i].transform.position, Quaternion.identity, transform);
                    Vector3 diretion = line.transform.position - rightImages[j].transform.position;
                    line.transform.right = -diretion;
                    line.transform.localScale = new Vector3(2.8f, 0.1f, 1);
                    float y = line.gameObject.GetComponent<RectTransform>().pivot.y;
                    line.gameObject.GetComponent<RectTransform>().pivot = new Vector2(0, y);
                    line.transform.Rotate(-90, 0, 0);
                }
                else if (intsLeft[i] != intsRigth[j] && leftImages[i].color == Color.yellow && rightImages[j].color == Color.yellow)
                {
                    leftImages[i].color = Color.red;
                    rightImages[j].color = Color.red;

                    StartCoroutine(ResetColor());
                }
            }
        }

        if (IsAllGree().Equals(true))
        {
            print("Venceu");
            if (SceneManager.GetActiveScene().name == "TutorialScene")
            {
                TutorialManager tutorialManager;
                tutorialManager = (TutorialManager)FindObjectOfType(typeof(TutorialManager));
                tarefa.SetActive(false);
                tutorialManager.NextTutorial();
                this.gameObject.SetActive(false);
            }
            else
            {
                AddHealth();
                RegionDescriptionContainer regionDescriptionData;
                regionDescriptionData = Resources.Load<RegionDescriptionContainer>("RegionDescriptionData");
                EnergyManager.RemovEnergy("value", regionDescriptionData.GetEstamina(region.ToString()));
                TimeManager.CalculateDestination(regionDescriptionData.GetTimeToComplete(region.ToString()));
               
                Close2();
                MoneyToRemove(moneyToRemove);
                InstantiateMinigameCompleteScreenManager.SpawnScreen();
            }

            return;
        }

    }

    public void Reset()
    {
        for (int i = 0; i < leftImages.Count; i++)
        {
            leftImages[i].color = Color.red;
            rightImages[i].color = Color.red;

        }
    }

    public void FillArays()
    {

        /*Coluna1*/     /*Coluna2*/     /*Coluna3*/
        /*Linha1*/
        m[0, 0] = 1; m[0, 1] = 2; m[0, 2] = 3;
        /*Linha2*/
        m[1, 0] = 1; m[1, 1] = 3; m[1, 2] = 2;
        /*Linha3*/
        m[2, 0] = 2; m[2, 1] = 1; m[2, 2] = 3;
        /*Linha4*/
        m[3, 0] = 2; m[3, 1] = 3; m[3, 2] = 1;
        /*Linha5*/
        m[4, 0] = 3; m[4, 1] = 1; m[4, 2] = 2;
        /*Linha6*/
        m[5, 0] = 3; m[5, 1] = 2; m[5, 2] = 1;

        Random.InitState((int)System.DateTime.Now.Ticks);
        int a = Random.Range(0, 6);
        print("left = linha " + a);
        for (int i = 0; i < intsLeft.Length; i++)
        {
            intsLeft[i] = m[a, i];
        }

        int b = Random.Range(0, 6);
        print("Right = linha " + b);
        for (int i = 0; i < intsRigth.Length; i++)
        {
            intsRigth[i] = m[b, i];

        }

    }
    public void Close()
    {
        TarefaTrigger.CallEnable(true);
        if (this.gameObject.transform.parent.name == "MinigameCanvas")
            Destroy(this.gameObject);
        else
            Destroy(transform.parent.gameObject);

        InstanatiateBoasPraticasPopUP.SpawBoasPraticas();
        //CallAds.ShowAdAfter();
    }
    
    public void Close2()
    {
        TarefaTrigger.CallEnable(true);
        if (this.gameObject.transform.parent.name == "MinigameCanvas")
            Destroy(this.gameObject, 1f);
        else
            Destroy(transform.parent.gameObject, 1f);
       
        InstanatiateBoasPraticasPopUP.SpawBoasPraticas();
        //CallAds.ShowAdAfter();
    }

    public void AddHealth()
    {
        RegionProperties[] regions = FindObjectsOfType<RegionProperties>();
        for (int i = 0; i < regions.Length; i++)
        {
            if (regions[i].region == region)
            {
                regions[i].IncreaseHealth();
            }
        }
    }

    public void SetBgImage()
    {
        float mid = maxH / 2;

        if (h == maxH)
        {
            GetComponent<Image>().sprite = cerca0;
        }

        else if (h < maxH && h >= mid)
        {
            GetComponent<Image>().sprite = cerca1;
        }

        else if (h < mid && h > 0 || h == 0)
        {
            GetComponent<Image>().sprite = cerca2;
        }
    }

    IEnumerator ResetColor()
    {
        yield return new WaitForSeconds(1.5f);
        
        foreach (Image i in leftImages)
        {
            if (i.color == Color.red)
                i.color = Color.white;
        }

        foreach (Image i in rightImages)
        {
            if (i.color == Color.red)
                i.color = Color.white;
        }
    }

    

    /*public void CheckArrayA()
    {

        if (quantasVezesApssouPeloArray == intsLeft.Length)
        {
            print("Não existe numeros repitidos");
            quantasVezesApssouPeloArray = 0; // apenas para teste, excluir depois ou não, pois assim uso só uma variavel
            n = 0; // apenas para teste, excluir depois, o mesmo acima
            return;
        }

        n++;
        int contador = 0;
        for (int i = 0; i < intsLeft.Length; i++)
        {
            if (intsLeft[i] == n)
            {
                contador++;
            }
        }

        if (contador >= 2)
        {
            print("existem numeros repitidos");
            for (int i = 0; i < intsLeft.Length; i++)
            {
                if (intsLeft[i] == n)
                {
                    intsLeft[i] = Random.Range(1, 4);
                    break;
                }
            }
            quantasVezesApssouPeloArray = 0;
            n = 0;
            CheckArrayA();
        }
        else
        {
            quantasVezesApssouPeloArray++;
            CheckArrayA();
        }


    }
    */
}
