using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class RetiarAgua : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool enable;
    public Slider slider;
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        slider.value = slider.maxValue;
        slider.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (image.rectTransform.sizeDelta.y <= 0)
        {*/
        if (slider.value == slider.minValue)
        {
            FiscaliMedirPH fiscal = FindObjectOfType<FiscaliMedirPH>();
            print("agua retirada");
            fiscal.AddCompletedTarefa();
            fiscal.NextTarefa(fiscal.sujeiras, " - remova os limos");
            Destroy(this.gameObject);
        }
        //this.gameObject.GetComponent<RetiarAgua>().enabled = false;
        //}

        if (Input.GetMouseButton(0))
        {
            if (enable)
            {
                /*if (image.rectTransform.sizeDelta.y > 0)
                {
                    Vector2 imageSize = new Vector2(image.rectTransform.sizeDelta.x - Time.deltaTime * 6, image.rectTransform.sizeDelta.y - Time.deltaTime * 20);
                    Vector2 imagePosition = new Vector2(image.rectTransform.position.x, image.rectTransform.position.y - Time.deltaTime * 6);

                    image.rectTransform.position = imagePosition;
                    image.rectTransform.sizeDelta = imageSize;
                }*/

                slider.value -= Time.deltaTime * 0.2f;
            }
        }
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
