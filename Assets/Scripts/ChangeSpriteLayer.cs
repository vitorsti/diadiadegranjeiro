using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpriteLayer : MonoBehaviour
{
    public SpriteRenderer sprite;
    public PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        player = (PlayerController)GameObject.FindObjectOfType(typeof(PlayerController));
        sprite.sortingOrder = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.position.z < player.transform.position.z)
        {
            sprite.sortingOrder = 3;
        }
        else if (this.gameObject.transform.position.z > player.transform.position.z)
        {
            sprite.sortingOrder = 0;
        }
    }
}
