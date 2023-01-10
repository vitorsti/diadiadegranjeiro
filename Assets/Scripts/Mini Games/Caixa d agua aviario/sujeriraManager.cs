using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class sujeriraManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image image;
    public Color color;
    bool enable;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        color = image.color;
    }

    // Update is called once per frame
    void Update()
    {

        if (color.a <= 0)
        {
            Destroy(this.gameObject);
        }

        if (Input.GetMouseButton(0))
        {
            if (enable)
            {

                color.a -= Time.deltaTime;
                image.color = color;
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
