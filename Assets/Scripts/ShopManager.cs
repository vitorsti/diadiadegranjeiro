using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class ShopManager : MonoBehaviour
{
    public ShopItensContainer shopData;
    public GameObject[] itens;
    // Start is called before the first frame update
    void Awake()
    {
        shopData = Resources.Load<ShopItensContainer>("ShopContainer");


        

    }

    void Start(){
        itens = shopData.itens;

        foreach(GameObject i in itens){

            GameObject go  = i;
            go.name = i.name;
            Instantiate(go, transform.position, transform.rotation, transform);
        }
    }

}
