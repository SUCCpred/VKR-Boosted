using Assets.CodeBase.Infrastructure;
using Assets.CodeBase.Infrastructure.StateMachine.States;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Logic
{
    class MainMenuController : MonoBehaviour
    {
        #region Unity Editor

        [SerializeField] private Button _quitButton;
        [SerializeField] private Button _transformButton;
        [SerializeField] private Button _trackingButton;

        [SerializeField] private GameObject _transformText;

        [SerializeField] private GameObject _planeFinderObject;

        #endregion

        #region Private Fields

        private bool isTransform = false;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _quitButton.onClick.AddListener(() =>
            {
                GameBootstrapper.Instance.Game.StateMachine.Enter<LoadLevelState, string>("Menu_Scene");
            });
            _transformButton.onClick.AddListener(() =>
            {
                isTransform = !isTransform;
                _transformText.SetActive(isTransform);
                TouchMoveController.Instance.Switch(isTransform);
            });
            _trackingButton.onClick.AddListener(() =>
            {
                _planeFinderObject.SetActive(true);
            });
        }

        #endregion
    }
}
