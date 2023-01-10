using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FiscalManager : MonoBehaviour
{
    public Text possibleReward, conText, subText, megaText, finalRewardText;
    public Button closeButton;
    public TasksCounterValuesContainer tasksData;
    [Header("Monsta quantas tarefas de  cada tipo teve")]
    [SerializeField]
    private float con;
    [SerializeField]
    float sub;
    [SerializeField]
    float mega;

    [Header("Coloque a penalidade para cada tipo de tarefa")]
    public float conPenalty;
    public float subPenalty;
    public float megaPenalty;

    [Header("Coloque a recompensa")]
    [SerializeField]
    private float reward;

    public GameObject spawner;
    // Start is called before the first frame update
    void Awake()
    {
        TarefaTrigger.CallEnable3(false);

        closeButton = this.gameObject.GetComponentInChildren<Button>();
        closeButton.onClick.AddListener(Close);
        //closeButton.gameObject.SetActive(false);

        tasksData = Resources.Load<TasksCounterValuesContainer>("TasksCounterData");

        con = tasksData.GetTask("Consequence");
        sub = tasksData.GetTask("SubConsequence");
        mega = tasksData.GetTask("MegaConsequence");
        TimeManager timeManager = FindObjectOfType<TimeManager>();
        timeManager.startTimer = false;
    }

    void Start()
    {
        //TarefaTrigger.CallEnable3(false);
        possibleReward.text += reward;

        conText.text = "";
        subText.text = "";
        megaText.text = "";
        finalRewardText.text = reward.ToString();

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
    }

    // Update is called once per frame
    public void CalculateReward()
    {

        float penaltyA = conPenalty * con;
        conText.text = "consequencia: " + con + " * " + conPenalty + " = " + penaltyA;
        print("con: " + penaltyA);

        float penaltyB = subPenalty * sub;
        subText.text = "sub-consequencia: " + sub + " * " + subPenalty + " = " + penaltyB;
        print("sub: " + penaltyB);

        float penaltyC = megaPenalty * mega;
        megaText.text = "mega-consequencia: " + mega + " * " + megaPenalty + " = " + penaltyC;
        print("mega: " + penaltyC);

        float penalty = penaltyA + penaltyB + penaltyC;
        print("penalidade: " + "- " + penalty);

        float finalReward = reward - penalty;
        finalRewardText.text = reward + " - " + penalty + " = " + finalReward;
        print("Recompensa final: " + finalReward);

        MoneyManager.AddMoney("cash", finalReward);
    }

    public void Close()
    {
        DayValuesContainer dayData;
        dayData = Resources.Load<DayValuesContainer>("DayData");
        dayData.SetValue("fiscal", 0);

        LoteValuesContainer loteData;
        loteData = Resources.Load<LoteValuesContainer>("LoteData");
        loteData.SetValue("lote0", loteData.GetMaxValue("lote0"));

        DayManager dayManager;
        dayManager = FindObjectOfType<DayManager>();
        dayManager.fiscalDay = false;

        TarefaTrigger.CallEnable3(true);

        GameObject rankScree = Resources.Load<GameObject>("RankScreen");
        GameObject go = Instantiate(rankScree, this.gameObject.transform.position, this.gameObject.transform.rotation, this.gameObject.transform.parent);
        go.name = rankScree.name;

        Destroy(spawner);
        Destroy(this.gameObject);


    }

    public void SetSpawner(GameObject go)
    {
        spawner = go;
    }


}
