using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenLink : MonoBehaviour
{
    public string link;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OpenInternetLink);
    }

    // Update is called once per frame
    public void OpenInternetLink()
    {
        Application.OpenURL(link);
    }
}
