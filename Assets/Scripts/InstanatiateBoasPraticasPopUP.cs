using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanatiateBoasPraticasPopUP : MonoBehaviour
{
    public GameObject boasPraticasPouUp;
    public GameObject popUpPosition;
    // Start is called before the first frame update
    void Awake()
    {
        popUpPosition = GameObject.FindGameObjectWithTag("PopUp");
        boasPraticasPouUp = Resources.Load<GameObject>("BoasPraticasPopUp");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void SpawBoasPraticas()
    {
        InstanatiateBoasPraticasPopUP ibp;
        ibp = FindObjectOfType<InstanatiateBoasPraticasPopUP>();
        GameObject go = Instantiate(ibp.boasPraticasPouUp, ibp.popUpPosition.transform.position, ibp.popUpPosition.transform.rotation, ibp.popUpPosition.transform);
        go.name = ibp.boasPraticasPouUp.name;
    }
}
