using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioZone : MonoBehaviour
{
    AudioSource audioSource;
    bool startDistanceCalculation;
    public Transform player, soundZone;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (startDistanceCalculation)
        {
            float dis = Vector3.Distance(player.position, soundZone.position);
            print(dis);
            audioSource.volume = dis; 
        }*/
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            audioSource.Play();
            //startDistanceCalculation = true;
            audioSource.loop = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            //startDistanceCalculation = false;
            audioSource.Stop();
            audioSource.loop = false;
        }
    }
}
