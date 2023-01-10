using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GetClosestMapMarker : MonoBehaviour
{
    //public GameObject[] markers;
    //public List<float> values = new List<float>();
    public GameObject marker;
    public PlayerController playerController;
    public Vector3 tp;
    // Start is called before the first frame update
    void Start()
    {
        //markers = GameObject.FindGameObjectsWithTag("MapMarker");
        playerController = FindObjectOfType<PlayerController>();
        CalculateClosest();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            //GoToClosest();
        }
    }

    void CalculateClosest()
    {
        //for (int i = 0; i < markers.Length; i++)
        //{
        //    float dif = Vector3.Distance(this.gameObject.transform.position, markers[i].transform.position);
        //    values.Add(dif);
        //}

        //for (int i = 0; i < markers.Length; i++)
        //{
        //    float dif = Vector3.Distance(this.gameObject.transform.position, markers[i].transform.position);
        //    if (dif == values.Min())
        //    {

        //        Debug.LogError("the closest is: " + dif);
        //        Debug.LogError(markers[i].name + " position: " + markers[i].transform.position);
        //        tp = markers[i].transform.position;
        //        break;
        //    }

        //}
    }

    //public void GoToClosest()
    //{
    //    playerController.Tp(tp);
    //}

    public void GoToMarker()
    {
        tp = marker.transform.position;
        playerController.Tp(tp);
    }

}
