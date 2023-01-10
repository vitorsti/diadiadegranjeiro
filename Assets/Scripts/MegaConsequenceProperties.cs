using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaConsequenceProperties : MonoBehaviour
{
    [Header("----- Controladores -----")]
    public bool spawned = false;

    [Header("----- Listas de Causas -----")]
    public List<GameObject> Causer;

    // Start is called before the first frame update
    void Start()
    {
        spawned = true;
    }

}
