using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameLigarPontosController : MonoBehaviour
{
    public RectTransform[] leftPoints;
    public RectTransform[] rightPoints;

    public GameObject[] spots;
    public GameObject[] lines;

    public int corrects;

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < lines.Length; i++)
        {
            int rnd = Random.Range(0, lines.Length);
            GameObject tempGO = lines[rnd];
            lines[rnd] = lines[i];
            lines[i] = tempGO;

            int rng = Random.Range(0, spots.Length);
            GameObject slotGO = spots[rng];
            spots[rng] = spots[i];
            spots[i] = slotGO;
        }

        for (int i = 0; i < lines.Length; i++)
        {
            lines[i].GetComponent<RectTransform>().anchoredPosition = leftPoints[i].anchoredPosition;
            lines[i].GetComponent<MiniGameDragDrop>().initialpos = leftPoints[i];

            spots[i].GetComponent<RectTransform>().anchoredPosition = rightPoints[i].anchoredPosition;
        }
    }

    void Update()
    {

#if UNITY_EDITOR
        if (Input.anyKeyDown)
        {
            // corrects = 4;
            // AddCorrects();

        }
#endif

    }

    public void AddCorrects()
    {
        corrects++;

        if (corrects >= 4)
        {
            Debug.Log("Ganhou");

            MinigameGerador go = FindObjectOfType<MinigameGerador>();

            if (go != null)
            {
                go.AddCompletedeConsequenceObjectives();
                go.DisableButton(go.fiaçãoButton.name);
                Destroy(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }

        }
    }

    public void Close()
    {
        Destroy(this.gameObject);
    }
}
