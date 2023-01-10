using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class SettingsMenuManager : MonoBehaviour
{
    public AudioMixer musicMixer;
    public Slider musicSlider;
    public AudioMixer sfxMixer;
    public Slider sfxSlider;
    public AudioMixer uiMixer;
    public Slider uiSlider;

    public void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        uiSlider.value = PlayerPrefs.GetFloat("UiVolume", 0.75f);
    }


    public void MusicVolume(float value)
    {
        musicMixer.SetFloat("MusicVolum", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void SfxVolume(float value)
    {
        sfxMixer.SetFloat("SFXVolum", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    public void UiVolume(float value)
    {
        uiMixer.SetFloat("UiVolum", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("UiVolume", value);
    }
}
