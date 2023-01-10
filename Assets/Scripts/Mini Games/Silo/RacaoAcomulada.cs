using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RacaoAcomulada : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    bool enable;
    [SerializeField]
    Color imageColor;
    [SerializeField]
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        imageColor = image.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (imageColor.a <= 0){
            Debug.Log("destruiu");
            Destroy(this.gameObject);
        }

        if (Input.GetMouseButton(0))
        {
            if (enable)
                imageColor.a -= Time.deltaTime;

        }

        if (!enable)
        {
            if (imageColor.a >= 1)
            {
                imageColor.a = 1;
                return;

            }
            else
                imageColor.a += Time.deltaTime;
        }

        image.GetComponent<Image>().color = imageColor;


    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        enable = true;

    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        enable = false;
    }
}
