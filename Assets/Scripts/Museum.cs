using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Museum : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI mName;
    [SerializeField] private Image image;
    private Transform museumExhibitsParent;
    private JSONLoader.Config.Exhibit[] myMuseumExhibits;
    private GameObject orgPanel;
    private GameObject objPanel;
    private GameObject search;
    private GameObject viewPanel;
    private Sprite exhImage;
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => {
            objPanel.SetActive(true);
            orgPanel.SetActive(false);
            BuildExhibits();
        });
    }
    

    private void BuildExhibits()
    {
        foreach (Transform child in museumExhibitsParent)
        {
            Destroy(child.gameObject);
        }        
        
        foreach (JSONLoader.Config.Exhibit exhibit in myMuseumExhibits)
        {
            Exhibit exhibitObj = Instantiate(Resources.Load<GameObject>("Prefabs/Exhibit"), museumExhibitsParent).GetComponent<Exhibit>();
            exhibitObj.Init(exhibit, exhImage, objPanel, search, viewPanel);
        }
    }

    public void Init(string _mName, Sprite _sprite, GameObject _orgPanel, GameObject _objPanel, GameObject _search, GameObject _viewPanel, Transform _museumExhibitsParent, JSONLoader.Config.Exhibit[] _myMuseumExhibits)
    {
        this.name = System.String.Format("Museum[{0}]", _mName);
        mName.text = _mName;
        //тут будет обработка картинки
        objPanel = _objPanel;
        orgPanel = _orgPanel;
        search = _search;
        viewPanel = _viewPanel;
        museumExhibitsParent = _museumExhibitsParent;
        myMuseumExhibits = _myMuseumExhibits;
    }
}
