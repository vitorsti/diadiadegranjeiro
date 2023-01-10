using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimparCaixa : MonoBehaviour
{
    public sujeriraManager[] sujeiras;
    bool start;
    // Start is called before the first frame update
    void Start()
    {
        start = false;
        sujeiras = GameObject.FindObjectsOfType<sujeriraManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sujeiras.Length > 0)
        {
            start = true;
        }

        if (start)
        {
            sujeiras = GameObject.FindObjectsOfType<sujeriraManager>();
            if (sujeiras.Length == 0)
            {
                print("sujeiras limpas");
                FiscaliMedirPH fiscali = FindObjectOfType<FiscaliMedirPH>();
                fiscali.NextTarefa(fiscali.colocarCloroButton.gameObject, "- Coloque água e aplique cloro");
                fiscali.AddCompletedTarefa();
                start = false;
            }
        }
    }
}
