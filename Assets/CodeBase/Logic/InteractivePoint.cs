using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Logic
{
    public class InteractivePoint : MonoBehaviour
    {
        #region Unity Editor

        [SerializeField] private Button _interactiveButton;
        [SerializeField] private string _infoText;
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private InfoResponser _infoResponser;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _interactiveButton.onClick.AddListener(() =>
            {
                Debug.Log("int but");
                _infoResponser.StartMessage(_infoText, _audioClip, gameObject);
            });
        }

        #endregion
    }
}