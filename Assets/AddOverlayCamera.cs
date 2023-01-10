using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AddOverlayCamera : MonoBehaviour
{
    public Camera overlayCamera, mainCamera;
    
    private void Awake()
    {
        overlayCamera = this.GetComponent<Camera>();

        mainCamera = Camera.main;
        var cameraData =  mainCamera.GetUniversalAdditionalCameraData();
        cameraData.cameraStack.Add(overlayCamera);
        
    }

    private void OnDestroy()
    {
        var cameraData = mainCamera.GetUniversalAdditionalCameraData();
        cameraData.cameraStack.Clear();

    }
}
