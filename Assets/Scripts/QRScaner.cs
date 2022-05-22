using ZXing;
using UnityEngine;
using System;
using Assets.CodeBase.Infrastructure;
using Assets.CodeBase.Infrastructure.StateMachine.States;

class QRScaner: MonoBehaviour
{
    [SerializeField]
    public UnityEngine.UI.RawImage rawimage;

    private WebCamTexture camTexture;
    private Rect screenRect;
    private bool detected = false;

    void Start()
    {

        WebCamDevice[] devices = WebCamTexture.devices;
        for (int i = 0; i < devices.Length; i++)
        {
            Debug.Log(String.Format("{0} : {1}", i, devices[i].name));
        }

        //screenRect = new Rect(0, 0, Screen.width, Screen.height);
        camTexture = new WebCamTexture();

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
        if (!detected)
        {
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
                        string[] param = result.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                        detected = true;
                        ModelLoader._id = param[0];
                        ModelLoader._prefabName = param[1];
                        GameBootstrapper.Instance.Game.StateMachine.Enter<LoadLevelState, string>("AR_Ground_Scene");
                }
                }
                catch (Exception ex) { Debug.LogWarning(ex.Message); }
            }
            else
            {
                camTexture.Play();
            }
        }
    }
}