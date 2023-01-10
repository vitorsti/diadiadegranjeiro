using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarefasMenu : MonoBehaviour
{
    public bool pause;
    public GameObject tarefaPanel;
    ConSubMegaInformationScreen otherTaskDescription;
    // Start is called before the first frame update
    void Start()
    {
        pause = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MainMenu()
    {
        if (pause) { Resume(); } else { Pause(); }
        //TarefaTrigger.CallEnable3(true);

    }

    public void Pause()
    {
        otherTaskDescription = FindObjectOfType<ConSubMegaInformationScreen>();
        if (otherTaskDescription != null)
            otherTaskDescription.gameObject.SetActive(true);

        tarefaPanel.GetComponent<RectTransform>().localPosition = Vector3.zero;
        //tarefaPanel.SetActive(true);
        pause = true;
        TarefaTrigger.CallEnable3(false);
    }

    public void Resume()
    {
        //otherTaskDescription = FindObjectOfType<ConSubMegaInformationScreen>();
        if (otherTaskDescription != null)
            otherTaskDescription.gameObject.SetActive(false);

        tarefaPanel.GetComponent<RectTransform>().localPosition = new Vector3(800, 0, 0);
        //tarefaPanel.SetActive(false);
        pause = false;
        TarefaTrigger.CallEnable3(true);
    }
}
