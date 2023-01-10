using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseTransformTranslateDirection : MonoBehaviour
{
    public enum VectorDirection { nothing, up, down, left, rigth, vector3Down }
    public VectorDirection vectorDirectionWithSpeed;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (vectorDirectionWithSpeed == VectorDirection.nothing)
        {
            return;
        }
        else if (vectorDirectionWithSpeed == VectorDirection.up)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime, Space.World);

        }
        else if (vectorDirectionWithSpeed == VectorDirection.down)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime, Space.World);


        }
        else if (vectorDirectionWithSpeed == VectorDirection.left)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);

        }
        else if (vectorDirectionWithSpeed == VectorDirection.rigth)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime, Space.World);

        }
        else if (vectorDirectionWithSpeed == VectorDirection.vector3Down)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime, Space.Self);
        }


    }
}
