using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

// namespace AutoDeploy.Logic
// {
    public class Api : MonoBehaviour
    {

        #region Properties

        public static string MainUrlPath { get => mainUrlPath; set => mainUrlPath = value; }

    #endregion

    #region Private Fields

    private static string mainUrlPath = "https://drive.google.com/uc?export=download&id=";
    //private static string mainUrlPath = "http://100.69.67.213:4444/api/getFile/";
    //private static string mainUrlPath = "http://127.0.0.1:4444/api/getFile/";

    #endregion

    #region Public Methods

    public static IEnumerator DownloadImage(string url, System.Action<Texture2D> callback)
        {
            Debug.Log("<color=grey> try download image: " + MainUrlPath + url + "</color>");

            UnityWebRequest request = UnityWebRequestTexture.GetTexture(MainUrlPath + url);
            yield return request.SendWebRequest();
            Texture2D myTexture = null;
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                myTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            }
            callback(myTexture);
        }

        public static IEnumerator GetRequest(string path, System.Action<long, string> callback)
        {
            long code = 0;
            string text = "";

            Debug.Log("<color=grey>try get path id: " + MainUrlPath + path + "</color>");

            UnityWebRequest request = UnityWebRequest.Get(MainUrlPath + path);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("Something went wrong, and returned error: " + request.error);
                code = 500;
            }
            else
            {
                text = request.downloadHandler.text;
                code = request.responseCode;
            }

            yield return null;

            callback(code, text);

        }

        public static IEnumerator GetAssetBundleRequest(string path, System.Action<long, AssetBundle> callback)
        {
            long code = 0;
            AssetBundle bundle = null;

            Debug.Log("<color=grey>try get path id: " + MainUrlPath + path + "</color>");

            UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(MainUrlPath + path);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("Something went wrong, and returned error: " + request.error);
                code = 500;
            }
            else
            {
                bundle = DownloadHandlerAssetBundle.GetContent(request);
                code = request.responseCode;
            }

            yield return null;

            callback(code, bundle);

        }

        #endregion
    }
// }