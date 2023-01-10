using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class MinigamePegarCachorro : MonoBehaviour
{
    public float speed, jumpForce;
    public float irParaTrasSpeed;
    //public float jumpSpeed;
    public float timer;
    public float timerReset;
    public bool enablePular;
    public bool irParaTras;
    bool move;
    public List<GameObject> obsToDestroy;
    public GameObject minigameScreen, eventSpawner;
    public GameObject[] prefabs;
    //public string eventSpawnerName;
    public Animator animator;
    public GameObject jogador;
    private GameObject player, scenario, cam, miniGameCanvas;
    public Transform spawner;
    public Rigidbody2D playerRb;

    // Start is called before the first frame update
    void Awake()
    {
        //if (SceneManager.GetActiveScene().name == "Game")
        //{
            cam = Camera.main.gameObject;
            //scenario = GameObject.Find("Scenario");
            //player = GameObject.Find("PlayerGroup");
            //miniGameCanvas = GameObject.Find("MinigameCanvas");

           
            //scenario.SetActive(false);
            
       // }
    }
    void Start()
    { 
        cam.SetActive(false);//player.SetActive(false);
        if (animator != null)
        {
            enablePular = true;
            animator.speed = speed;
        }
        irParaTras = false;
        timerReset = timer;
        move = true;
        //eventSpawner = GameObject.Find(eventSpawnerName);

        if (spawner != null)
            InvokeRepeating("SpawnObs", 1.0f, 5.0f);

        playerRb = jogador.GetComponent<Rigidbody2D>();

        /*if (SceneManager.GetActiveScene().name == "Game")
        {
            miniGameCanvas.SetActive(false);
        }*/

        //TarefaTrigger.CallEnable2(false);

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -1, 12);
        transform.position = pos;

        if (irParaTras == false)
        {
            if (move)
                jogador.transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

        if (irParaTras == true)
        {
            jogador.transform.Translate(Vector2.left * irParaTrasSpeed * Time.deltaTime);
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            timer = 0;
            timer = timerReset;
            irParaTras = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (enablePular == true)
            {
                enablePular = false;
                if (animator != null)
                    animator.Play("pular");

                //transform.Translate(Vector2.up * jumpForce, Space.Self);
                playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }


    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "chao")
        {
            enablePular = true;
            if (animator != null)
                animator.Play("correr");
        }
        if (other.gameObject.name == "obstaculo")
        {
            print("Colidiu");
            irParaTras = true;
            Destroy(other.gameObject);
        }

        if (other.gameObject.name == "cachorro" || other.gameObject.name == "raposa")
        {
            print("chegou");
            CancelInvoke("SpawnObs");
            foreach (GameObject i in obsToDestroy)
            {
                Destroy(i);
            }

            move = false;
            /*if (SceneManager.GetActiveScene().name == "Game")
                TarefaTrigger.CallEnable2(true);*/
            Destroy(eventSpawner);
            RegionDescriptionContainer regionDescriptionData;
            regionDescriptionData = Resources.Load<RegionDescriptionContainer>("OtherTasksDescriptionData");
            if (other.gameObject.name == "cachorro")
            {
                EnergyManager.RemovEnergy("value", regionDescriptionData.GetEstamina("Pegar Cachorro"));
                TimeManager.CalculateDestination(regionDescriptionData.GetTimeToComplete("Pegar Cachorro"));
            }
            else if (other.gameObject.name == "raposa")
            {
                EnergyManager.RemovEnergy("value", regionDescriptionData.GetEstamina("Pegar Raposa"));
                TimeManager.CalculateDestination(regionDescriptionData.GetTimeToComplete("Pegar Raposa"));
            }
            InstantiateMinigameCompleteScreenManager.SpawnScreen();
            Close();

        }
    }

    public void PularFalse()
    {
        animator.SetBool("pular", false);
        enablePular = true;
    }

    public void SpawnObs()
    {
        GameObject go = Instantiate(prefabs[Random.Range(0, prefabs.Length)], spawner.position, Quaternion.identity);
        go.transform.SetParent(spawner.gameObject.transform, true);
        go.name = "obstaculo";
        obsToDestroy.Add(go);
        Destroy(go, 60f);
    }

    public void Close()
    {
        Debug.Log("ta cliccando");
        cam.SetActive(true); 
        /* if (SceneManager.GetActiveScene().name == "Game")
         {
             miniGameCanvas.SetActive(true);
             cam.SetActive(true);
             scenario.SetActive(true);
            
             TarefaTrigger.CallEnable(true);
         }*/

        //TarefaTrigger.CallEnable2(true);
        //ConSubMegaInformationScreen.CallClose();

        if (this.gameObject.transform.parent.name == "MinigameCanvas")
            Destroy(this.gameObject);
        else
            Destroy(transform.parent.gameObject);

        print("working");
        //CallAds.ShowAdAfter();
        
       //player.SetActive(true);

    }

    public void SetSpawner(GameObject obj)
    {
        Debug.Log("Funfo");
        eventSpawner = obj;
        //eventSpawner = GameObject.Find(eventSpawnerName);
    }
}
