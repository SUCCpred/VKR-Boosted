using UnityEngine;
using System;

public class ModelLoader : MonoBehaviour
{
    //private ICoroutineRunner _coroutineRunner;
    public static string _id;
    public static string _prefabName;
    void Start()
    {
        /*if (UIController._butIndex == 0){
            _id = "1AcN53O8Tmlc7jJFT9tfja-nX0Jd5OAff";
            _prefabName = "vpo-s";
        }
        else if (UIController._butIndex == 1){
            _id = "17RUDOSsmmFL5dv-xpHrpYMqQe8iEyQHz";
            _prefabName = "robot";
        }*/
        LoadPrefabsFromBundle(_id, (bundle)=>{
            if (bundle != null) {
                GameObject obj = (GameObject)bundle.LoadAsset(_prefabName);
                Instantiate(obj, gameObject.transform);
                bundle.Unload(false);
            }
        });
    }

    public void LoadPrefabsFromBundle(string path, Action<AssetBundle> callback)
        {
            StartCoroutine(Api.GetAssetBundleRequest(path, (code, response) =>
                {
                    if (code == 200)
                    {
                        callback(response);
                        //GameBootstrapper.Instance.StartLevelInformant.SetLevelPrefabsBundle(response);
                    }
                    else
                    {
                        Debug.LogError(code.ToString() + " " + response);
                        callback(null);
                        //GameBootstrapper.Instance.StartLevelInformant.SetLevelPrefabsBundle(null);
                    }
                }));
        }
}
