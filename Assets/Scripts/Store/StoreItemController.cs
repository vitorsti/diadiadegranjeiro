using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreItemController : MonoBehaviour
{
    public StoreItem storeData;
    public Button healthButton, maxHealthButton, regionGapButton, prevButton, nxtButton;
    public Slider maxHealthSlider, regionGapSlider;
    public TextMeshProUGUI title, posiTxt;
    public GameObject descriptionScreen;

    int index;
    // Start is called before the first frame update
    void Awake()
    {
        index = 0;
        storeData = Resources.Load<StoreItem>("Store/ShopData");
        maxHealthSlider.interactable = false; regionGapSlider.interactable = false;
        prevButton.onClick.AddListener(PrevIem);
        nxtButton.onClick.AddListener(NextItem);
        SetThings();
    }
    public void ShowDeascription(string _text)
    {
        descriptionScreen.SetActive(true);
        descriptionScreen.GetComponentInChildren<TextMeshProUGUI>().text = _text;

    }

    public void SetThings()
    {
        //storeData.datas[index].regionData = Resources.Load<RegionValuesContainer>("RegionData");

        //setando o titulo
        title.text = storeData.datas[index].nome;

        //setando a posicao
        int posi = index + 1;
        posiTxt.text = posi + " / " + storeData.datas.Length;

        //colocando a funcionaliodade dos botes
        healthButton.onClick.AddListener(delegate { storeData.BuyHealth(index); });
        maxHealthButton.onClick.AddListener(delegate { storeData.BuyMaxHealthUpgrade(index); });
        regionGapButton.onClick.AddListener(delegate { storeData.BuyRegionGapUpgrade(index); });

        //setando o preço
        healthButton.GetComponentInChildren<TextMeshProUGUI>().text = "$ " + storeData.datas[index].healthPrice;

        if (storeData.datas[index].maxHealthStage == storeData.datas[index].maxHealthMax)
            maxHealthButton.GetComponentInChildren<TextMeshProUGUI>().text = "MAX";
        else
            maxHealthButton.GetComponentInChildren<TextMeshProUGUI>().text = "$ " + storeData.datas[index].maxHealthPrice;

        if (storeData.datas[index].regionGapStage == storeData.datas[index].regionGapMax)
            regionGapButton.GetComponentInChildren<TextMeshProUGUI>().text = "MAX";
        else
            regionGapButton.GetComponentInChildren<TextMeshProUGUI>().text = "$ " + storeData.datas[index].regionGapPrice;

        //setando o nivel dos upgrades

        maxHealthSlider.maxValue = storeData.datas[index].maxHealthMax;
        maxHealthSlider.value = storeData.datas[index].maxHealthStage;

        regionGapSlider.maxValue = storeData.datas[index].regionGapMax;
        regionGapSlider.value = storeData.datas[index].regionGapStage;

        maxHealthButton.onClick.AddListener(Atualizar);
        regionGapButton.onClick.AddListener(Atualizar);
    }

    public void Atualizar()
    {

        //setando o preço
        if (storeData.datas[index].maxHealthStage == storeData.datas[index].maxHealthMax)
            maxHealthButton.GetComponentInChildren<TextMeshProUGUI>().text = "MAX";
        else
            maxHealthButton.GetComponentInChildren<TextMeshProUGUI>().text = "$ " + storeData.datas[index].maxHealthPrice;

        if (storeData.datas[index].regionGapStage == storeData.datas[index].regionGapMax)
            regionGapButton.GetComponentInChildren<TextMeshProUGUI>().text = "MAX";
        else
            regionGapButton.GetComponentInChildren<TextMeshProUGUI>().text = "$ " + storeData.datas[index].regionGapPrice;

        //setando o nivel dos upgrades
        maxHealthSlider.maxValue = storeData.datas[index].maxHealthMax;
        maxHealthSlider.value = storeData.datas[index].maxHealthStage;

        regionGapSlider.maxValue = storeData.datas[index].regionGapMax;
        regionGapSlider.value = storeData.datas[index].regionGapStage;

    }

    public void RemoveListeners()
    {
        healthButton.onClick.RemoveAllListeners();
        maxHealthButton.onClick.RemoveAllListeners();
        regionGapButton.onClick.RemoveAllListeners();
    }


    public void PrevIem()
    {
        if (index != 0)
        {
            index--;
            RemoveListeners();
            SetThings();
        }
    }

    public void NextItem()
    {
        if (index != storeData.datas.Length - 1)
        {
            index++;
            RemoveListeners();
            SetThings();
        }
    }


}
