using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchController : MonoBehaviour
{
    bool enable;
    Color color;
    // Start is called before the first frame update
    void Awake()
    {
        enable = false;
        if (enable == false)
        {
            color = Color.red;
            GetComponent<Image>().color = color;
        }
        else
        {
            color = Color.red;
            GetComponent<Image>().color = color;
        }
    }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (enable){
                    MiniGameFilcalAquecedor f = FindObjectOfType<MiniGameFilcalAquecedor>();
                    f.AddCompletedTarefa();
                    print("Win Win");
                }
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.name == "Handle")
            {
                enable = true;
                color = Color.green;
                GetComponent<Image>().color = color;
                print(other.name + " entrou");
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.name == "Handle")
            {
                enable = false;
                color = Color.red;
                GetComponent<Image>().color = color;
                print(other.name + " saiu");
            }
        }
    }
