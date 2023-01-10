using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public Vector3 sunStartPosition;
    public float startTime, endTime, timeSpeed;
    public float timer;
    public bool startTimer;
    string niceTime;
    TextMeshProUGUI text;
    public Light2D sun;

    public GameObject[] lights;
    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        //sun = GameObject.FindGameObjectWithTag("Sun").GetComponent<Light2D>();
        lights = GameObject.FindGameObjectsWithTag("Light");

        timer = startTime;
        startTimer = true;

        if (timeSpeed == 0)
            timeSpeed = 1;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer)
        {

            timer += Time.deltaTime * timeSpeed;

            int minutes = Mathf.FloorToInt(timer / 60F);
            int seconds = Mathf.FloorToInt(timer - minutes * 60);

            niceTime = string.Format("{0:00}:{1:00}", minutes, seconds);

            text.text = niceTime;

            /*if (minutes < 13 && sun.intensity <= 1)
                sun.intensity += Time.deltaTime * 0.005f * timeSpeed;*/
            if (minutes == 12 && seconds == 00)
            {
                sun.transform.position = new Vector3(0, sun.transform.position.y, 0);
            }


            if (minutes <= 6 || minutes >= 18)
            {
                TurnOnOffLights(true);
            }
            else
            {
                TurnOnOffLights(false);
            }
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                CalculateDestination(100);
            }
#endif

        }

        if (timer >= endTime)
        {
            timer = endTime;
            startTimer = false;
            GameEventManager gameEvent = FindObjectOfType<GameEventManager>();
            EnergyManager.pass = true;
            gameEvent.NextDay();
            EnergyManager.pass = false;


        }


    }

    void FixedUpdate()
    {
        if (startTimer)
        {
            sun.transform.Translate(new Vector3(2.5f, 0, -2.5f) * Time.deltaTime * timeSpeed, Space.World);
            //sun.transform.position += sun.transform.right * Time.deltaTime * timeSpeed;
            //sun.transform.position -= sun.transform.up * Time.deltaTime * timeSpeed;
        }

    }

    void SunCycle()
    {
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);

        if (minutes <= 6)
            sun.intensity = 0.5f;
        else if (minutes <= 6 && seconds <= 01)
            sun.intensity = 0.55f;

    }


    public static void ResetTime()
    {
        TimeManager timeManager;
        timeManager = FindObjectOfType<TimeManager>();
        timeManager.timer = timeManager.startTime;
        timeManager.startTimer = true;
        timeManager.sun.transform.position = timeManager.sunStartPosition;
    }

    //true para ligar luzes e false para desligar
    public void TurnOnOffLights(bool enable)
    {
        foreach (GameObject i in lights)
        {
            i.GetComponent<Light2D>().enabled = enable;
        }
    }


    //     void OnGUI()
    //     {

    //     }

    public static void CalculateDestination(float f)
    {
        //s = so + v * t

        TimeManager t = FindObjectOfType<TimeManager>();
        // calcula a posicao do x
        float sx; float sox; float vx; float tx; float tox; float x; float z;

        sox = t.sun.transform.position.x;
        vx = 2.5f * t.timeSpeed;
        tox = t.timer;
        tx = t.timer + f;

        sx = sox + vx * (tx - tox);
        Debug.Log("posi x = " + sx);
        x = sx;

        // calcula a posicao do z
        float sz; float soz; float vz; float tz; float toz;

        soz = t.sun.transform.position.z;
        vz = -2.5f * t.timeSpeed;
        toz = t.timer;
        tz = t.timer + f;

        sz = soz + vz * (tz - toz);
        Debug.Log("posi z = " + sz);
        z = sz;

        Debug.Log(tx + " " + tz);

        t.timer = tx;
        t.sun.gameObject.transform.position = new Vector3(x, t.sun.transform.position.y, z);

    }
}

