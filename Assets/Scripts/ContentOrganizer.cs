using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CI.QuickSave;

public class ContentOrganizer : MonoBehaviour
{
    public GameObject content, notificationImage;
    RectTransform rt;
    public GameObject childPrefab;
    public EventHeader[] children;
    public int length;
    public int heigthAdjust;
    bool startFlicker = false;

    // Start is called before the first frame update
    void Start()
    {
        heigthAdjust = (int)childPrefab.GetComponent<RectTransform>().sizeDelta.y;
        content = this.gameObject;
        rt = content.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, heigthAdjust);

    }

    // Update is called once per frame
    void Update()
    {
        children = content.GetComponentsInChildren<EventHeader>();
        foreach (EventHeader i in children)
        {

            if (length == children.Length)
            {
                break;
            }
            if (length < children.Length)
            {
                rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y + heigthAdjust);
                length++;
                notificationImage.GetComponent<StartFlicker>().enabled = true;

            }
            if (length > children.Length)
            {
                rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y - heigthAdjust);
                length--;
            }

        }

        if (length == 1 && children.Length == 0)
        {
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, heigthAdjust);
            length = 0;

        }


    }



    
}
