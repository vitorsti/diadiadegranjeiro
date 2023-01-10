using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContaminacaoBehavior : MonoBehaviour
{
    void OnMouseDown()
    {
        Destroy(gameObject);
    }

    void OnMouseOver(){
        print(this.gameObject.name);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == "Player"){
            Destroy(this.gameObject);
        }
    }
}
