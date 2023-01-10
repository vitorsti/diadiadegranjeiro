using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    [SerializeField]
    private float gold, cash;
    [SerializeField]
    private bool remove;
    public TextMeshProUGUI goldText, cashText;
    [SerializeField]
    private MoneyValuesContainer moneyData;
    // Start is called before the first frame update

    void Awake()
    {
        moneyData = Resources.Load<MoneyValuesContainer>("MoneyData");
    }
    void Start()
    {
        gold = moneyData.GetMoney("gold");
        cash = moneyData.GetMoney("cash");

        AtualizarText();

    }

    void Update()
    {

        AtualizarText();

#if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.C))
        {
            MoneyManager.AddMoney("cash", 10);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            MoneyManager.AddMoney("gold", 10);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            MoneyManager.RemoveMoney("cash", 10);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            MoneyManager.RemoveMoney("gold", 10);
        }
#endif

    }

    public static void AddMoney(string name, float amount)
    {
        MoneyValuesContainer moneyData;
        moneyData = Resources.Load<MoneyValuesContainer>("MoneyData");

        moneyData.AddQtty(name, amount);

    }

    public static void RemoveMoney(string name, float amount)
    {
        MoneyValuesContainer moneyData;
        moneyData = Resources.Load<MoneyValuesContainer>("MoneyData");

        moneyData.RemoveQtty(name, amount);

    }

    public static float GetMoney(string name)
    {
        MoneyValuesContainer moneyData;
        moneyData = Resources.Load<MoneyValuesContainer>("MoneyData");

        float i = moneyData.GetMoney(name);

        return i;
    }

    public void AtualizarText()
    {
        gold = moneyData.GetMoney("gold");
        cash = moneyData.GetMoney("cash");

        int g = (int)gold;
        int c = (int)cash;

        goldText.text = g.ToString();
        cashText.text = c.ToString();
    }

}
