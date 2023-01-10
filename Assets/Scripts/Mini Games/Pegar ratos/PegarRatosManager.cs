using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PegarRatosManager : MonoBehaviour
{
    public GameObject ratPrefab;
    public GameObject eventSpawner;
    public Canvas can;
    public Text ratsCountText;
    public int quantity;
    int maxQuantity;
    bool start;
    Vector3 down;
    public List<GameObject> ratsList = new List<GameObject>();
    GameObject tarefasButton, nextDayButton, missionPanel;
    void Awake()
    {
        maxQuantity = quantity;
        tarefasButton = GameObject.Find("TarefasButton");
        nextDayButton = GameObject.Find("NextDayyButton");
        missionPanel = GameObject.Find("MissionPanel");

        Transform[] childs = transform.parent.GetComponentsInChildren<Transform>();
        for (int i = 0; i < childs.Length; i++)
        {
            if(childs[i].gameObject.GetComponent<Canvas>() != null)
                can = childs[i].GetComponent<Canvas>();
        }

        SetActivate(false);

        can.worldCamera = Camera.main;
    }
    // Start is called before the first frame update
    void Start()
    {
        start = false;
        down = Vector3.down;
        do { SpawnRat(); } while (quantity > 0);
    }

    void Update()
    {
        if (start)
        {
            for (int i = 0; i < ratsList.Count; i++)
            {
                if (ratsList[i] == null)
                {
                    ratsList.RemoveAt(i);
                }
            }

            if (ratsList.Count == 0)
            {
                TarefaTrigger.CallPass(true);
                //CallAds.ShowAdAfter();
                Destroy(transform.parent.gameObject);
            }
        }

        ratsCountText.text = "Ratos: " + ratsList.Count +  "/" + maxQuantity;

        // if (Input.anyKeyDown && !(Input.GetMouseButton(0)
        //     || Input.GetMouseButton(1)
        //     || Input.GetMouseButton(2)
        //     || Input.GetMouseButtonDown(0)
        //     || Input.GetMouseButtonDown(1)
        //     || Input.GetMouseButtonDown(2)))
        // {
        //     SpawnRat();
        // }


    }

    public void SpawnRat()
    {
        transform.position = new Vector3(Random.Range(-35, 35), 10, Random.Range(-70, 84));

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(down), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(down) * hit.distance, Color.yellow);

            if (hit.collider.name.Contains("chao"))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(down) * hit.distance, Color.red);

                if (ratPrefab == null)
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    ratsList.Add(cube);
                    cube.transform.position = hit.point;
                    cube.transform.parent = this.gameObject.transform.parent;
                    quantity--;
                }
                else
                {
                    GameObject go = Instantiate(ratPrefab, new Vector3(hit.point.x, 1, hit.point.z), transform.rotation * Quaternion.Euler(90, 0, 0), this.gameObject.transform.parent);
                    go.name = ratPrefab.name;
                    ratsList.Add(go);
                    quantity--;
                }

                Debug.Log(hit.collider.name + " position: " + hit.point);
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(down) * hit.distance, Color.white);
                Debug.Log("Did not Hit " + "hit name: " + hit.collider.name + " hit position: " + hit.point);
                SpawnRat();
            }

            if (!start)
                start = true;

        }
    }
    void OnDestroy()
    {
        RegionDescriptionContainer regionDescriptionData;
        regionDescriptionData = Resources.Load<RegionDescriptionContainer>("OtherTasksDescriptionData");
        EnergyManager.RemovEnergy("value", regionDescriptionData.GetEstamina("Infestação de ratos"));
        SetActivate(true);
        Destroy(eventSpawner);
    }

    public void SetSpawner(GameObject obj)
    {
        eventSpawner = obj;
    }

    void SetActivate(bool enable)
    {
        tarefasButton.SetActive(enable);

        if (nextDayButton != null)
            nextDayButton.SetActive(enable);

        if (missionPanel != null)
            missionPanel.SetActive(enable);

        TarefaTrigger.CallPass(enable);
    }

}
