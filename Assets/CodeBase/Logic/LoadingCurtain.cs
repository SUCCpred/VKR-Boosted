using UnityEngine;
using UnityEngine.UI;

namespace Assets.CodeBase.Logic
{
    public class LoadingCurtain : PageUI
    {
        public static LoadingCurtain Instance { get => _instance; set => _instance = value; }

        #region Unity Editor

        [SerializeField] private Slider _progressSlider;

        #endregion

        #region Private Fields

        private static LoadingCurtain _instance = null;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                DestroyImmediate(gameObject);
            }
            _progressSlider.interactable = false;
        }

        #endregion


        #region Overrides Methods

        public override void Show()
        {
            base.Show();
        }
        public override void Hide()
        {
            base.Hide();
            _progressSlider.value = 0;
        }

        #endregion

        #region Publc Methods

        public void SetProgressValue(float value)
        {
            _progressSlider.value = value;
        }

        #endregion
    }
}
