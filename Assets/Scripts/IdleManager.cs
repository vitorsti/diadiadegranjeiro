using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleManager : MonoBehaviour
{
    public float howMuchMoney;

    public float time;
    int moneyToAdd;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AddMoney", time, time);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddMoney()
    {
        MoneyManager.AddMoney("gold", (howMuchMoney));
    }

}
