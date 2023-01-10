using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DestroyObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool mouseOver;

    private void Start() {

        mouseOver = false;
    }

    private void Update()
    {
        if (mouseOver)
        {
            if (Input.GetMouseButtonDown(1))
            {

                Destroy(this.gameObject);
            }
        }
    }
  
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
    }
}
