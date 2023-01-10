using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstantiateMinigameCompleteScreenManager : MonoBehaviour
{
    public GameObject prefab, go, minigameCanvas;
    public TextMeshProUGUI[] texts;
    [SerializeField]
    private float moneyToAdd;
    [SerializeField]
    private bool spawnScreen;

    private void OnValidate()
    {
        if (spawnScreen)
        {
            SpawnScreen();
            spawnScreen = false;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        prefab = Resources.Load<GameObject>("MiniGameCompleteScreen");
        minigameCanvas = GameObject.FindGameObjectWithTag("TarefaCanvas");

        //SpawnScreen();
        //moneyInfo.text = moneyInfo.text.Replace("&m", moneyToAdd.ToString());
    }

    public static void SpawnScreen()
    {
        InstantiateMinigameCompleteScreenManager imc = FindObjectOfType<InstantiateMinigameCompleteScreenManager>();
        imc.go = Instantiate(imc.prefab, imc.minigameCanvas.transform.position, imc.transform.rotation * Quaternion.Euler(0, 0, 0), imc.minigameCanvas.transform);
        imc.go.GetComponentInChildren<Button>().onClick.AddListener(imc.Close);

        imc.texts = imc.go.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI i in imc.texts)
        {
            if (i.text.Contains("&m"))
            {
                i.text = i.text.Replace("&m", imc.moneyToAdd.ToString());
            }
        }
    }

    void Close()
    {
        MoneyManager.AddMoney("gold", moneyToAdd);
        Destroy(go,0.1f);
        go = null;
    }
}
