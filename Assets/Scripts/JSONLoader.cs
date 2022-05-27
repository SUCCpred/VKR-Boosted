using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class JSONLoader : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _title, _discription, _mName;
    [SerializeField] private Image _mLogo, _objImg;
    [SerializeField] private Museum museumPrefab;
    [SerializeField] private Transform museumParent;
    [SerializeField] private Exhibit exhibitPrefab;
    [SerializeField] private Transform exhibitParent;
    [SerializeField] private Transform museumExhibitsParent;
    [SerializeField] private Transform viewParent;
    [SerializeField] private GameObject objPanel;
    [SerializeField] private GameObject search;
    [SerializeField] private GameObject viewPanel;
    [SerializeField] private GameObject orgPanel;
    private string _id = "1yJ3Xbay9NSx6wE-JOgSEK0fF33yjRsfw";

    [Serializable]
    public struct Config
    {
        [Serializable]
        public struct Exhibit
        {
            [field: SerializeField] public string id, pref_name, image, title, discription;
        };

        [Serializable]
        public struct Museum
        {
            [field: SerializeField] public string m_id, name, m_image;
            [field: SerializeField] public Exhibit[] exhibits;
        };

        [field: SerializeField] public Museum[] museums;
    }

    void Start()
    {
        new UIController();
        StartCoroutine(Api.GetRequest(_id, (_error, _jsonString) =>
        {
            //JSONObject obj = new JSONObject(_jsonString);

            //Config obj = JsonUtility.FromJson<Config>(_jsonString);
            Newtonsoft.Json.Utilities.AotHelper.EnsureList<Config.Museum>();
            Newtonsoft.Json.Utilities.AotHelper.EnsureList<Config.Exhibit>();
            
            Config obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Config>(_jsonString);

            //Debug.Log(obj);

            foreach (Config.Museum museum in obj.museums)
            {
                Museum MuseumInstance = Instantiate(museumPrefab, museumParent);

                MuseumInstance.Init(museum.name, null, orgPanel, objPanel, search, viewPanel, museumExhibitsParent, (JSONLoader.Config.Exhibit[])museum.exhibits);

            }
        }));
        
        
    }

}