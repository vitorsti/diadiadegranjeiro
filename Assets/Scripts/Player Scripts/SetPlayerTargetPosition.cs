using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerTargetPosition : MonoBehaviour
{

    public PlayerController playerCon;

    // Start is called before the first frame update
    void Awake()
    {
        playerCon = FindObjectOfType<PlayerController>();
    }

    public void OnMouseDown(){

        playerCon.TransformPosition();
    }
}
