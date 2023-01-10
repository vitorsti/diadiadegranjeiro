using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class GalinhaMortaBehavior : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool enable;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (enable)
            {
                Destroy(this.gameObject);
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
