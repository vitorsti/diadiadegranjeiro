using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using MiniJSONV;

public class TestLocationService : MonoBehaviour
{
    /*public string GoogleAPIKey;
    public string countryLocation;
    private void Start()
    {
        StartCoroutine(LocationCoroutine());
    }
    IEnumerator LocationCoroutine()
    {
        // Uncomment if you want to test with Unity Remote
#if UNITY_EDITOR
        yield return new WaitWhile(() => !UnityEditor.EditorApplication.isRemoteConnected);
        yield return new WaitForSecondsRealtime(10f);
#endif
#if UNITY_EDITOR
        // No permission handling needed in Editor
#elif UNITY_ANDROID
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.CoarseLocation)) {
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.CoarseLocation);
        }

        // First, check if user has location service enabled
        if (!UnityEngine.Input.location.isEnabledByUser) {
            // TODO Failure
            Debug.LogFormat("Android and Location not enabled");
            yield break;
        }

#elif UNITY_IOS
        if (!UnityEngine.Input.location.isEnabledByUser) {
            // TODO Failure
            Debug.LogFormat("IOS and Location not enabled");
            yield break;
        }
#endif
        // Start service before querying location
        UnityEngine.Input.location.Start(500f, 500f);

        // Wait until service initializes
        int maxWait = 15;
        while (UnityEngine.Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            maxWait--;
        }

        // Editor has a bug which doesn't set the service status to Initializing. So extra wait in Editor.
#if UNITY_EDITOR
        int editorMaxWait = 15;
        while (UnityEngine.Input.location.status == LocationServiceStatus.Stopped && editorMaxWait > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            editorMaxWait--;
        }
#endif

        // Service didn't initialize in 15 seconds
        if (maxWait < 1)
        {
            // TODO Failure
            Debug.LogFormat("Timed out");
            yield break;
        }

        // Connection has failed
        if (UnityEngine.Input.location.status != LocationServiceStatus.Running)
        {
            // TODO Failure
            Debug.LogFormat("Unable to determine device location. Failed with status {0}", UnityEngine.Input.location.status);
            yield break;
        }
        else
        {
            Debug.LogFormat("Location service live. status {0}", UnityEngine.Input.location.status);
            // Access granted and location value could be retrieved
            Debug.LogFormat("Location: "
                + UnityEngine.Input.location.lastData.latitude + " = latitude; "
                + UnityEngine.Input.location.lastData.longitude + " = longitude; "
                + UnityEngine.Input.location.lastData.altitude + " = altitude; "
                + UnityEngine.Input.location.lastData.horizontalAccuracy + " = horizontal accuraccy; "
                + UnityEngine.Input.location.lastData.timestamp);

            var _latitude = UnityEngine.Input.location.lastData.latitude;
            var _longitude = UnityEngine.Input.location.lastData.longitude;

            string lat = _latitude.ToString();
            string lo = _longitude.ToString();
            UnityEngine.Input.location.Stop();
            // TODO success do something with location

            using (UnityWebRequest www = new UnityWebRequest("https://maps.googleapis.com/maps/api/geocode/json?latlng=-22.95495,-47.3143&key=AIzaSyAuhJY1c2hgcS-i5SrVnKNWp0-C9QlE9V0"))
            {
                yield return www;

                //if request was successfully
                if (www.error == null)
                {
                    Debug.LogFormat(www.downloadHandler.text);
                    //Deserialize the JSON file
                    // var location = Json.Deserialize(www.downloadHandler.text) as Dictionary<string, object>;
                    // var locationList = location["results"] as List<object>;
                    // var locationList2 = locationList[0] as Dictionary<string, object>;

                    // //Extract the substring information at the end of the locationList2 string
                    // countryLocation = locationList2["formatted_address"].ToString().Substring(locationList2["formatted_address"].ToString().LastIndexOf(",") + 2);

                    // //This will return the country information
                    // Debug.LogAssertion(countryLocation);
                }
            };
            //Debug.Log("Latitude: " + _latitude + "\n" + "Longitude: " + _longitude);
        }

        // Stop service if there is no need to query location updates continuously

    }*/
}
