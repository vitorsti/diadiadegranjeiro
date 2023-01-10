using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public Transform target;
    //public float speed;
    public Transform playerSprite;
    public NavMeshAgent agent;
    int TapCount;
    public float MaxDubbleTapTime;
    float NewTime;
    float speedReset;

    public float futurePosition, actualPosition;
    public float extraRotationSpeed;

    private void Awake()
    {
        /*if (Application.CanStreamedLevelBeLoaded("MainCanvas"))
            SceneManager.LoadSceneAsync("MainCanvas", LoadSceneMode.Additive);
        else
            print("Cena não existe");*/

        futurePosition = 0;
        actualPosition = 0;
        SetFirstTime();

    }
    void Start()
    {
        TapCount = 0;
        agent = GetComponent<NavMeshAgent>();
        speedReset = agent.speed;
        //agent.updateRotation = false;

        //agent.speed = 0;
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(1))
        {

            /*targetPos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.position = targetPos;*/
            actualPosition = this.gameObject.transform.position.z;
            TransformPosition();

        }
#endif

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                TapCount += 1;
            }

            if (TapCount == 1)
            {

                NewTime = Time.time + MaxDubbleTapTime;
            }
            else if (TapCount == 2 && Time.time <= NewTime)
            {

                //Whatever you want after a dubble tap
                actualPosition = this.gameObject.transform.position.z;
                TransformPosition();
                print("Dubble tap");


                TapCount = 0;
            }

        }
        if (Time.time > NewTime)
        {
            TapCount = 0;
        }


    }

    void FixedUpdate()
    {

        playerSprite.position = this.transform.position;

        //Altera animação do personagem
        if (actualPosition < futurePosition) //indo para cima
        {
            PlayerAnimationController.SpriteDirectior(true);

        }
        else if (actualPosition > futurePosition) //indo para baixo
        {
            PlayerAnimationController.SpriteDirectior(false);
        }
        /*else if (actualPosition == futurePosition)
        {

        }*/

        //extraRotation();


    }

    public void TransformPosition()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("task");
        if (obj != null)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {

            agent.SetDestination(hit.point);
            target.position = hit.point;
            futurePosition = target.transform.position.z;

            //transform.LookAt(target);

        }

    }

    public void Tp(Vector3 posi)
    {
        //
        //this.enabled = true;
        target.position = posi;
        futurePosition = target.transform.position.z;
        actualPosition = target.transform.position.z;
        this.transform.position = posi;
        agent.SetDestination(posi);
        this.enabled = false;
    }

    void extraRotation()
    {
        Vector3 lookrotation = agent.steeringTarget - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookrotation), extraRotationSpeed * Time.deltaTime);
    }

    void SetFirstTime()
    {
        int ft = PlayerPrefs.GetInt("FirstTimeRuning", 0);
        if (ft == 0)
            PlayerPrefs.SetInt("FirstTimeRuning", 1);
        else
            return;
    }
}
