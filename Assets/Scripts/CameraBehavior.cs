using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    Vector3 touchStart;
    public float zoomOutMin;
    public float zoomOutMax;
    public float zoomSpeed;
    public bool lockCamera;

    public float speed;
    public enum Direction { left, right }
    public Direction chooseDirection;

    bool temp;

    // Start is called before the first frame update
    void Start()
    {
        //GoToPlayer();
        temp = false;
    }

    // Update is called once per frame
    void Update()
    {

#if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (temp)
                temp = false;
            else
                temp = true;
        }
        if (temp)
        {
            if (chooseDirection == Direction.right)
                transform.Translate(Vector3.right * Time.deltaTime * speed);
            if (chooseDirection == Direction.left)
                transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
#endif

        if (Input.GetMouseButtonDown(0))
        {

            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }

        if (Input.touchCount == 2)
        {

            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            Zoom(difference * 0.01f);
        }
        else if (Input.GetMouseButton(0))
        {
            // bloqueia a camera de se mexer caso tenha uma tarefa ativa
            GameObject obj = GameObject.FindGameObjectWithTag("task");
            if (obj != null)
                return;
            //
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -50, 60), transform.position.y, Mathf.Clamp(transform.position.z, -95, 130));
        }

        Zoom(Input.GetAxis("Mouse ScrollWheel"));

        if (Input.GetKeyDown(KeyCode.C))
        {
            GoToPlayer();
        }
    }
    void Zoom(float increement)
    {
        if (Camera.main != null)
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increement * zoomSpeed, zoomOutMin, zoomOutMax);
    }
    public void GoToPlayer()
    {
        GameObject player = GameObject.Find("Player");
        Transform p = player.transform;
        //Camera.main.orthographicSize = zoomOutMin;
        transform.position = new Vector3(p.position.x, transform.position.y, p.position.z);
    }


}
