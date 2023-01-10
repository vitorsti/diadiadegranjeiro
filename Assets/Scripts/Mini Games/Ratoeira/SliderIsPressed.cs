using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SliderIsPressed : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    ArmarRatoeiraManager ratMananger;

    // Start is called before the first frame update
    void Start()
    {
        ratMananger = FindObjectOfType<ArmarRatoeiraManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        ratMananger.enable = false;

    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        ratMananger.enable = true;
    }
}
