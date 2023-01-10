using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TxtMeshProChangePage : MonoBehaviour
{
    public TextMeshProUGUI txt, browserTxt;
    public TecnicalInformationContainer data;
    GameObject minigameCanvas, mainCanvas;
    public int totalPages;
    public int currentPage;
    int pageChange;

    public int articleId;

    // Start is called before the first frame update
    void Awake()
    {
        txt = GetComponent<TextMeshProUGUI>();
        data = Resources.Load<TecnicalInformationContainer>("TecnicalInformationData");
        minigameCanvas = GameObject.FindGameObjectWithTag("TarefaCanvas");
        //mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas");
    }

    void Start()
    {
        pageChange = 1;
        articleId = 0;
        if (browserTxt != null)
            browserTxt.text = data.GetBrowserTxt(articleId);
        txt.text = data.GetDescrition(articleId);
        currentPage = 1;
        if (minigameCanvas != null)
            minigameCanvas.SetActive(false);
        //mainCanvas.SetActive(false);
        Time.timeScale = 0;
    }

    void Update()
    {
        totalPages = txt.textInfo.pageCount;
    }

    public void SelectArticle(int _id)
    {
        if (browserTxt != null)
            browserTxt.text = data.GetBrowserTxt(_id);
        txt.text = data.GetDescrition(_id);
        txt.pageToDisplay = 1;
        pageChange = 1;
        articleId = _id;
    }

    public void NextPage()
    {
        if (pageChange > txt.textInfo.pageCount)
        {
            if (articleId == data.datas.Length - 1)
                pageChange = txt.textInfo.pageCount;
            else
                NextArticle();
        }
        else
        {
            pageChange++;
            currentPage = pageChange;
            txt.pageToDisplay = pageChange;
        }
    }

    public void PrevPage()
    {

        if (pageChange < 1)
        {
            if (articleId == 0)
            {
                pageChange = 1;
                return;
            }
            else
                PrevArticle();
        }
        else
        {
            pageChange--;
            currentPage = pageChange;
            txt.pageToDisplay = pageChange;
        }
    }

    public void NextArticle()
    {
        if (articleId >= data.datas.Length - 1)
            articleId = data.datas.Length - 1;
        else
            articleId++;

        pageChange = 1;

        if (browserTxt != null)
            browserTxt.text = data.GetBrowserTxt(articleId);

        txt.text = data.GetDescrition(articleId);
        txt.pageToDisplay = 1;

    }

    public void PrevArticle()
    {
        if (articleId <= 0)
            articleId = 0;
        else
            articleId--;

        //pageChange = 1;
        if (browserTxt != null)
            browserTxt.text = data.GetBrowserTxt(articleId);

        txt.text = data.GetDescrition(articleId);
        txt.pageToDisplay = txt.textInfo.pageCount;
        pageChange = txt.textInfo.pageCount;

    }

    public void Close(GameObject go)
    {
        if (minigameCanvas != null)
            minigameCanvas.SetActive(true);
        //mainCanvas.SetActive(false);
        Time.timeScale = 1;
        Destroy(go);
    }



}
