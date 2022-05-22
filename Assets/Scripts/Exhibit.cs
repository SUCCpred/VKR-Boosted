using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Exhibit : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI discription;
    [SerializeField] private Image image;
    private GameObject objPanel;
    private GameObject search;
    private GameObject viewPanel;
    private Button button;
    private string id;
    private string prefabName;
    private string prefabDescription;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(()=>{
            ModelLoader._id = id;
            ModelLoader._prefabName = prefabName;
            objPanel.SetActive(false);
            search.SetActive(false);
            viewPanel.SetActive(true);
            GameObject.Find("View_Заголовок").GetComponent<TextMeshProUGUI>().text = title.text;
            GameObject.Find("View_Описание").GetComponent<TextMeshProUGUI>().text = prefabDescription;
        });
    }
    public void Init(JSONLoader.Config.Exhibit exhibit, Sprite _sprite, GameObject _objPanel, GameObject _search, GameObject _viewPanel)
    {
        this.name = String.Format("Exhibit[{0}]", exhibit.id);
        prefabDescription = exhibit.discription;
        title.text = exhibit.title;
        discription.text = prefabDescription.Length > 100 ? String.Format("{0}...", prefabDescription.Substring(0, 100)) : prefabDescription;
        //тут будет обработка картинки
        id = exhibit.id;
        prefabName = exhibit.pref_name;
        objPanel = _objPanel;
        search = _search;
        viewPanel = _viewPanel;
    }
}
