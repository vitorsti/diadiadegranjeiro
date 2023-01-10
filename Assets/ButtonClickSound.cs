using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ButtonClickSound : MonoBehaviour
{
    public AudioClip clip;
    public AudioMixerGroup group;

    Button button;
    AudioSource audioSource;

    void Start()
    {
        button = GetComponent<Button>();
		
        if (button != null)
            button.onClick.AddListener(PlaySound);
    }

    public void PlaySound()
    {
        audioSource = GetComponent<AudioSource>();

        if (clip == null)
        {
            return;
        }

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.outputAudioMixerGroup = group;
        }

        if (audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
