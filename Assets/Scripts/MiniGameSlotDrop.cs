using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MiniGameSlotDrop : MonoBehaviour, IDropHandler
{
    public int color;
    [SerializeField] private MiniGameLigarPontosController controller;

    void Awake(){

        controller = GameObject.FindObjectOfType<MiniGameLigarPontosController>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerEnter != null && eventData.pointerDrag.gameObject.GetComponent<MiniGameDragDrop>().color == color)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            eventData.pointerDrag.GetComponent<Image>().raycastTarget = false;
            controller.AddCorrects();
        }
    }
}
