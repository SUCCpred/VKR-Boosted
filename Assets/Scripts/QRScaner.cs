using ZXing;
using ZXing.QrCode;
using UnityEngine;
using System;

class QRScaner: MonoBehaviour
{
    [SerializeField]
    public UnityEngine.UI.RawImage rawimage;

    private WebCamTexture camTexture;
    private Rect screenRect;

    void Start()
    {

        WebCamDevice[] devices = WebCamTexture.devices;
        for (int i = 0; i < devices.Length; i++)
        {
            Debug.Log(String.Format("{0} : {1}", i, devices[i].name));
        }

        screenRect = new Rect(0, 0, Screen.width, Screen.height);
        camTexture = new WebCamTexture(devices[0].name, Screen.width, Screen.height);

        if (camTexture != null)
        {
            camTexture.requestedHeight = Screen.height;
            camTexture.requestedWidth = Screen.width;
            GetComponent<Renderer>().material.mainTexture = camTexture;
            rawimage.texture = camTexture;
            camTexture.Play();
        }
    }

    void OnGUI()
    {
        // drawing the camera on screen
        GUI.DrawTexture(screenRect, camTexture, ScaleMode.ScaleToFit);
        // do the reading — you might want to attempt to read less often than you draw on the screen for performance sake
        if (camTexture.isPlaying)
        {
            try
            {
                IBarcodeReader barcodeReader = new BarcodeReader();
                // decode the current frame
                var result = barcodeReader.Decode(camTexture.GetPixels32(),
                  camTexture.width, camTexture.height);
                if (result != null)
                {
                    Debug.Log("DECODED TEXT FROM QR: " + result.Text);

                }
            }
            catch (Exception ex) { Debug.LogWarning(ex.Message); }
        } else
        {
            camTexture.Play();
        }
    }
}