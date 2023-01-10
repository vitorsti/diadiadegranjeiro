using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DontQuitTest : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponentInChildren<Text>().text = "Hello";
        GetComponent<Button>().onClick.AddListener(ClickButton);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Home) || Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Menu))
            {
                DontQuit();
            }
    }

    public void DontQuit()
    {

        Application.CancelQuit();
        this.gameObject.GetComponentInChildren<Text>().text = "you cant escape...";
        Color c = Random.ColorHSV();
        this.gameObject.GetComponentInChildren<Text>().color = c;
    }

    public void ClickButton(){
        this.gameObject.GetComponentInChildren<Text>().text = "Hello";
        Color c = Random.ColorHSV();
        this.gameObject.GetComponentInChildren<Text>().color = c;

    }
}
