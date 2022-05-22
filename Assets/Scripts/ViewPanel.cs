using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ViewPanel : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI title;
    [SerializeField] public Image image;
    [SerializeField] public TextMeshProUGUI discription;
    
    public void Init(string _title, Sprite _sprite, string _discription)
    {
        title.text = _title;
        Debug.Log(_title + title.text);
        discription.text = _discription;
        //тут обработка картинки
    }
}
