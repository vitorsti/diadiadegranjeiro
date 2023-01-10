/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public static class CallAds
{

    public static void ShowAd()
    {
        //GameObject.FindObjectOfType<NonRewardAds>().StartAds();
    }

    public static void ShowAdAfter()
    {
        //GameObject.FindObjectOfType<NonRewardAds>().ShowAdsAfter();
    }

}
public class NonRewardAds : MonoBehaviour
{

#if UNITY_IOS
    private string gameId = "4213906";
#elif UNITY_ANDROID
    private string gameId = "4213907";
#elif UNITY_EDITOR
    private string gameId = "3996554";
#endif

    bool testMode = true;
    public int showAdsAfter;

    [SerializeField]
    private int counter, counterReset;
    string mySurfacingId = "Interstitial_Android";
    public bool enableInEditor;

    void Awake()
    {
        #if UNITY_EDITOR
        this.gameObject.SetActive(enableInEditor);
        #endif
        Advertisement.Initialize(gameId, testMode);
    }


    void Start()
    {
        counter = showAdsAfter;
        counterReset = counter;
        //StartCoroutine(ShowAds());
    }

    public void ShowInterstitialAd()
    {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady())
        {
            Advertisement.Show(mySurfacingId);
            // Replace mySurfacingId with the ID of the placements you wish to display as shown in your Unity Dashboard.
        }
        else
        {
            Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
        }
    }
    IEnumerator ShowAds()
    {
        yield return new WaitForSeconds(3f);
        ShowInterstitialAd();
    }

    public void ShowAdsAfter()
    {
        counter--;
        if (counter == 0)
        {
            StartAds();
            counter = counterReset;
        }
    }

    public void StartAds()
    {
        StartCoroutine(ShowAds());
    }
}
*/