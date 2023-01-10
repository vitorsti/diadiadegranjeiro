using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorBehavior : MonoBehaviour
{
    public GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        door.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Player")
        {
            print("entrou");
            door.SetActive(true);
        }
    }

     void OnTriggerExit(Collider other){
        if (other.gameObject.name == "Player")
        {
            door.SetActive(false);
        }
    }
}
