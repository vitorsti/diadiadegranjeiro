using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustUiImageBoxCollider : MonoBehaviour
{
    BoxCollider2D col;
    Image img;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        col = GetComponent<BoxCollider2D>();

        col.size = img.rectTransform.sizeDelta;
    }

    // Update is called once per frame
    void Update()
    {
        if (col.size != img.rectTransform.sizeDelta)
        {
            col.size = img.rectTransform.sizeDelta;
        }
    }
}
