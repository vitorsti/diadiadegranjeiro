using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RankManager : MonoBehaviour
{
    public TextMeshProUGUI possibleReward, conText, subText, megaText, finalRewardText, actualRank, nextRank, totalRankText;
    public Slider progressBar;
    public Button closeButton;
    public Image actualRankImage, nextRankImage;
    TasksCounterValuesContainer tasksData;
    RankValuesContainer rankData;

    [Header("Monsta quantas tarefas de  cada tipo teve")]
    [SerializeField]
    private float con;
    [SerializeField]
    float sub;
    [SerializeField]
    float mega;
    float conPenalty;
    float subPenalty;
    float megaPenalty;
    private float reward;

    public GameObject spawner;
    public bool sliderAnimin, continueAnimin;
    float playerRank;
    float finalReward;
    float dif, temp;
    // Start is called before the first frame update
    void Awake()
    {
        sliderAnimin = false;
        continueAnimin = false;
        TarefaTrigger.CallEnable3(false);

        /*closeButton = this.gameObject.GetComponentInChildren<Button>();
        closeButton.onClick.AddListener(Close);*/

        progressBar = this.gameObject.GetComponentInChildren<Slider>();
        //closeButton.gameObject.SetActive(false);

        tasksData = Resources.Load<TasksCounterValuesContainer>("TasksCounterData");
        rankData = Resources.Load<RankValuesContainer>("RankData");

        reward = rankData.reward;
        con = tasksData.GetTask("Consequence");
        sub = tasksData.GetTask("SubConsequence");
        mega = tasksData.GetTask("MegaConsequence");

        conPenalty = rankData.conPelnaty;
        subPenalty = rankData.subPenalty;
        megaPenalty = rankData.megaPenalty;

    }

    void Start()
    {
        //TarefaTrigger.CallEnable3(false);
        /*possibleReward.text += reward;

        conText.text = "";
        subText.text = "";
        megaText.text = "";
        finalRewardText.text = reward.ToString();*/

        //CalculateReward();

        SetRank();
        CalculateReward();
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            CalculateReward();
        }
#endif

        if (sliderAnimin)
        {
            if (finalReward > 0)
            {
                progressBar.value += finalReward * Time.deltaTime;

                if (progressBar.value >= rankData.rank)
                {

                    progressBar.value = rankData.rank;
                    sliderAnimin = false;

                }
                else if (progressBar.value == progressBar.maxValue && rankData.rank > progressBar.maxValue)
                {
                    PassRank();
                    sliderAnimin = false;
                }
            }

            if (finalReward < 0)
            {
                progressBar.value += finalReward * Time.deltaTime;

                if (progressBar.value <= rankData.rank)
                {

                    progressBar.value = rankData.rank;
                    sliderAnimin = false;

                }
                else if (progressBar.value == progressBar.minValue && rankData.rank < progressBar.minValue)
                {
                    DecreaseRank();
                    sliderAnimin = false;
                }
            }
        }

        if (continueAnimin)
        {
            if (dif > 0)
            {
                progressBar.value += dif * Time.deltaTime;
                if (progressBar.value >= rankData.rank)
                {

                    progressBar.value = rankData.rank;
                    continueAnimin = false;

                }
            }

            if (dif < 0)
            {
                progressBar.value += dif * Time.deltaTime;
                if (progressBar.value <= rankData.rank)
                {

                    progressBar.value = rankData.rank;
                    continueAnimin = false;

                }
            }
        }

    }

    public void SetRank()
    {

        playerRank = rankData.rank;

        //seta o rank atual
        for (int i = 0; i < rankData.datas.Length; i++)
        {
            if (playerRank >= rankData.GetRangeA(i) && playerRank <= rankData.GetRangeB(i))
            {
                actualRankImage.sprite = rankData.GetSprite(i);
                actualRank.text = rankData.GetRankName(i);
                progressBar.minValue = rankData.GetRangeA(i);
                progressBar.maxValue = rankData.GetRangeB(i);
                break;
            }

        }

        progressBar.value = playerRank;
        //sliderAnimin = true;

        //seta o proximo rank
        for (int i = 0; i < rankData.datas.Length; i++)
        {

            if (rankData.GetRangeA(i) > playerRank)
            {
                nextRankImage.sprite = rankData.GetSprite(i);
                nextRank.text = rankData.GetRankName(i);
                break;
            }
        }

        //sliderAnimin = true;

    }

    public void PassRank()
    {

        playerRank = rankData.rank;

        //seta o rank atual
        for (int i = 0; i < rankData.datas.Length; i++)
        {
            if (playerRank >= rankData.GetRangeA(i) && playerRank <= rankData.GetRangeB(i))
            {
                actualRankImage.sprite = rankData.GetSprite(i);
                actualRank.text = rankData.GetRankName(i);
                progressBar.minValue = rankData.GetRangeA(i);
                progressBar.maxValue = rankData.GetRangeB(i);
                break;
            }

        }

        //seta o proximo rank
        for (int i = 0; i < rankData.datas.Length; i++)
        {

            if (rankData.GetRangeA(i) > playerRank)
            {
                nextRankImage.sprite = rankData.GetSprite(i);
                nextRank.text = rankData.GetRankName(i);
                break;
            }
        }

        for (int i = 0; i < rankData.datas.Length; i++)
        {

            if (rankData.GetRangeB(i) < playerRank)
            {
                temp = rankData.GetRangeB(i);
                break;
            }
        }

        dif = playerRank - temp;
        continueAnimin = true;
    }

    public void DecreaseRank()
    {
        playerRank = rankData.rank;

        //seta o rank atual
        for (int i = 0; i < rankData.datas.Length; i++)
        {
            if (playerRank >= rankData.GetRangeA(i) && playerRank <= rankData.GetRangeB(i))
            {
                actualRankImage.sprite = rankData.GetSprite(i);
                actualRank.text = rankData.GetRankName(i);
                progressBar.minValue = rankData.GetRangeA(i);
                progressBar.maxValue = rankData.GetRangeB(i);
                break;
            }

        }

        //seta o proximo rank
        for (int i = 0; i < rankData.datas.Length; i++)
        {

            if (rankData.GetRangeA(i) > playerRank)
            {
                nextRankImage.sprite = rankData.GetSprite(i);
                nextRank.text = rankData.GetRankName(i);
                break;
            }
        }

        for (int i = 0; i < rankData.datas.Length; i++)
        {

            if (rankData.GetRangeB(i) > playerRank)
            {
                temp = rankData.GetRangeB(i);
                break;
            }
        }

        dif = playerRank - temp;
        continueAnimin = true;
    }



    public void CalculateReward()
    {

        float penaltyA = conPenalty * con;
        //conText.text = "consequencia: " + con + " * " + conPenalty + " = " + penaltyA;
        print("con: " + penaltyA);

        float penaltyB = subPenalty * sub;
        //subText.text = "sub-consequencia: " + sub + " * " + subPenalty + " = " + penaltyB;
        print("sub: " + penaltyB);

        float penaltyC = megaPenalty * mega;
        //megaText.text = "mega-consequencia: " + mega + " * " + megaPenalty + " = " + penaltyC;
        print("mega: " + penaltyC);

        float penalty = penaltyA + penaltyB + penaltyC;
        print("penalidade: " + "- " + penalty);

        finalReward = reward - penalty;
        //finalRewardText.text = reward + " - " + penalty + " = " + finalReward;
        print("Recompensa final: " + finalReward);

        //playerRank = rankData.rank;

        sliderAnimin = true;

        rankData.SetRank(finalReward);

        if (finalReward < 0)
            totalRankText.text = "Rank total: " + playerRank + " " + finalReward + " = " + rankData.rank;
        else
            totalRankText.text = "Rank total: " + playerRank + " + " + finalReward + " = " + rankData.rank;

        /*if (playerRank >= progressBar.maxValue || playerRank <= progressBar.minValue)
            SetRank();*/

    }

    public void Close()
    {
        TarefaTrigger.CallEnable3(true);
        TimeManager.ResetTime();

        float score = rankData.rank;
        int s = (int) score;
        //PlayGamesMethods.AddScoreToLeaderboard(s);

        Destroy(spawner);
        Destroy(this.gameObject);
    }

    public void SetSpawner(GameObject go)
    {
        spawner = go;
    }
}
