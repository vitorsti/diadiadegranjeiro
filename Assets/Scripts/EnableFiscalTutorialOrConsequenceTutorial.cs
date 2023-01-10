using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class EnableFiscalTutorialOrConsequenceTutorial : MonoBehaviour
{
    public Object script;
    //public System.Reflection.PropertyInfo[] rProps;
    public GameObject fiscalTutorial, consequenceTutorial;
    BindingFlags bindingFlags = BindingFlags.Public |
                            BindingFlags.NonPublic |
                            BindingFlags.Instance |
                            BindingFlags.Static;
    //public string[] variablesNames;
    private void Start() 

    {
        // if(script != null && obj != null){
        //    if(script.GetType(typeof(bool).GetProperty(""))){
        //    }
        // }

        //rProps = script.GetType().GetProperties();
        //  foreach(System.Reflection.PropertyInfo rp in rProps )
        //      Debug.Log(rp.Name);

        //variablesNames = new string[script.GetType().GetFields(bindingFlags).Length];

       /* foreach (FieldInfo field in script.GetType().GetFields(bindingFlags))
        {
            Debug.Log(field.Name);
            if (field.Name.Contains("Fiscal"))
            {
                Debug.LogError(field.Name + " = " + field.GetValue(script));
                if (field.GetValue(script).Equals(false))
                {
                    consequenceTutorial.SetActive(true);
                    Debug.LogError("é falso garai");
                }
                else
                {
                    fiscalTutorial.SetActive(true);
                    Debug.LogError("num é falso garai");
                }
            }

        }*/

        StartCoroutine(StartTutorial());


    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator StartTutorial(){
        Debug.Log("satarting tutorial check");
        yield return new WaitForSeconds(0.25f);
        foreach (FieldInfo field in script.GetType().GetFields(bindingFlags))
        {
            Debug.Log(field.Name);
            if (field.Name.Contains("Fiscal"))
            {
                Debug.LogError(field.Name + " = " + field.GetValue(script));
                if (field.GetValue(script).Equals(false))
                {
                    consequenceTutorial.SetActive(true);
                    Debug.LogError("é falso garai");
                }
                else
                {
                    fiscalTutorial.SetActive(true);
                    Debug.LogError("num é falso garai");
                }
            }

        }

    }
}
