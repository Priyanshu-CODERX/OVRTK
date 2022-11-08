using UnityEngine;
using UnityEngine.UI;

public class DeviceCameraManager : MonoBehaviour
{
    static WebCamTexture mainCameraTexture;
    public RawImage renderCameraTexture;

    private void Start()
    {
        if (mainCameraTexture == null)
            mainCameraTexture = new WebCamTexture();

        renderCameraTexture.texture = mainCameraTexture;

        //GetComponent<Renderer>().material.mainTexture = mainCameraTexture;

        if (!mainCameraTexture.isPlaying)
            mainCameraTexture.Play();
    }
}
