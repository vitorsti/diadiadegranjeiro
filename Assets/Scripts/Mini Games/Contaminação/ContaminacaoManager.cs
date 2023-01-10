using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ContaminacaoManager : MonoBehaviour
{
    public ContaminacaoBehavior[] contaminacaos;
    public GameObject eventSpawner;
    public Text text; 
    bool start;
    int lenght;
    // Start is called before the first frame update
    void Awake()
    {
        start = false;

        contaminacaos = FindObjectsOfType<ContaminacaoBehavior>();
        lenght = contaminacaos.Length;
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = contaminacaos.Length + " / " + lenght;
        
        if (contaminacaos.Length > 0)
        {
            start = true;
        }

        if (start)
        {
            contaminacaos = FindObjectsOfType<ContaminacaoBehavior>();

            if (contaminacaos.Length == 0)
            {
                start = false;
                Destroy(this.gameObject);

            }
        }


    }

    void OnDestroy()
    {
        Destroy(eventSpawner);
    }

    public void SetSpawner(GameObject obj)
    {
        eventSpawner = obj;
    }
}
