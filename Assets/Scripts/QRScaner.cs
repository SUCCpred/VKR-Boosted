using ZXing;
using UnityEngine;
using System;
using UnityEngine.UI;
using Assets.CodeBase.Infrastructure;
using Assets.CodeBase.Infrastructure.StateMachine.States;

class QRScaner: MonoBehaviour
{
    [SerializeField]
    public RawImage _rawImage;
    [SerializeField]    
    public AspectRatioFitter _aspectRatioFitter;
    //[SerializeField]
    //public RectTransform _scannerZone;

    private WebCamTexture _camTexture;
    private bool _isCamAvaible = false;

    void Start()
    {
        SetUpCamera();
        StartCoroutine(YieldScanForQR());
    }

    void Update()
    {
        // Update camera render
        if (_isCamAvaible)
        {
            _aspectRatioFitter.aspectRatio = (float)_camTexture.width / (float)_camTexture.height;
            //_rawImage.rectTransform.localEulerAngles = new Vector3(0, 0, -_camTexture.videoRotationAngle);
        }
    }

    private System.Collections.IEnumerator YieldTryToPlayCamera()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(.1f);
            _camTexture.Play();
            if (_camTexture.isPlaying)
            {
                break;
            }
        }
    }

    private void SetUpCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            Debug.Log("No camera detected");
            return;
        }

        Debug.Log("Cameras detected:");
        for (int i = 0; i < devices.Length; i++)
        {
            Debug.Log(String.Format("{0}: {1}", i, devices[i].name));
            if (!devices[i].isFrontFacing)
            {
                //_camTexture = new WebCamTexture(devices[i].name, (int)_scannerZone.rect.width, (int)_scannerZone.rect.height);
                _camTexture = new WebCamTexture(devices[i].name);
                _isCamAvaible = true;
                break;
            }
        }

        // TODO: Remove or comment this in build
        if (!_isCamAvaible)
        {
            //_camTexture = new WebCamTexture(devices[2].name, (int)_scannerZone.rect.width, (int)_scannerZone.rect.height);
            _camTexture = new WebCamTexture(devices[2].name);
            _isCamAvaible = true;
        }

        if (_isCamAvaible)
        {
            _rawImage.texture = _camTexture;
            //_rawImage.material.mainTexture = _camTexture;
            StartCoroutine(YieldTryToPlayCamera());
        } else
        {
            Debug.Log("No camera detected");
        }
    }
    
    // Perfom a scan once a second
    private System.Collections.IEnumerator YieldScanForQR()
    {
        IBarcodeReader barcodeReader = new BarcodeReader();
        //var snap = new Texture2D((int)_scannerZone.rect.width, (int)_scannerZone.rect.height, TextureFormat.RGBA32, false);

        while (true)
        {
            yield return new WaitForSecondsRealtime(1.5f);
            
            if (_isCamAvaible)
            {
                try
                {
                    //snap.SetPixels(_camTexture.GetPixels((int)_scannerZone.anchoredPosition.x, (int)_scannerZone.anchoredPosition.y, (int)_scannerZone.rect.width, (int)_scannerZone.rect.height));
                    //var result = barcodeReader.Decode(snap.GetPixels32(), snap.width, snap.height);
                    var result = barcodeReader.Decode(_camTexture.GetPixels32(), _camTexture.width, _camTexture.height);
                    if (result != null)
                    {
                        Debug.Log("QR code detected: " + result.Text);
                        LoadARScene(result.Text);
                        break;
                    }
                }
                catch(Exception ex)
                {
                    Debug.Log(String.Format("Failed trying to read QR code: {0}", ex));
                }
            }
            else
            {
                Debug.Log("Perfom a scan when camera not ready yet");
            }
        }

        _camTexture.Stop();
    }

    private void LoadARScene(string result)
    {
        string[] param = result.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
        ModelLoader._id = param[0];
        ModelLoader._prefabName = param[1];
        GameBootstrapper.Instance.Game.StateMachine.Enter<LoadLevelState, string>("AR_Ground_Scene");
    }
}