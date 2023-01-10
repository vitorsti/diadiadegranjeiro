using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBehavior : MonoBehaviour
{
   
    void OnMouseDown()
    {
        Destroy(gameObject);
    }

    void OnMouseOver(){
        print("Rato");
    }
}
