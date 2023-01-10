using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubConsequenceProperties : MonoBehaviour
{
    [Header("----- Controladores -----")]
    public int chanceToSpawn;
    [SerializeField] int addChance = 10;
    [SerializeField] int maxChancePerQty = 50;

    [Header("----- Listas de Causas -----")]
    public List<GameObject> Causer;

    public void VerifyCauserQuantity(int _qtdCausers)
    {
        Debug.Log(gameObject.name + " Chance to spawn QTD: " + _qtdCausers);

        if (_qtdCausers == 0)
            ResetChance();
        else
        {
            IncrementChance(_qtdCausers);

            //Verifica se estourou o limite de chance baseado na quantidade ativa recebida
            if (_qtdCausers == 1 && chanceToSpawn > 50)
                chanceToSpawn = maxChancePerQty;
            else if (_qtdCausers >= 2 && chanceToSpawn > 100)
                chanceToSpawn = 100;
        }

        Debug.Log(gameObject.name + "Chance = " + chanceToSpawn);
    }

    void IncrementChance(int _qtdCausers)
    {
        chanceToSpawn = chanceToSpawn + (addChance * _qtdCausers);
    }

    void ResetChance()
    {
        chanceToSpawn = 0;
    }
}
