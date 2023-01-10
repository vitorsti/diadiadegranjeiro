using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDayManager : MonoBehaviour
{
    GameEventManager eventManager;

    // Start is called before the first frame update
    void Start()
    {
        eventManager = GameObject.FindObjectOfType<GameEventManager>();
    }

}
