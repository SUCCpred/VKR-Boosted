using Assets.CodeBase.Logic;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Logic
{
    public class InfoResponser : PageUI
    {
        #region Enums

        public enum AnimState
        {
            none,
            plan1,
            plan2,
            vibroplit,
        }

        #endregion

        #region Unity Editor

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Text _messageText;

        [SerializeField] private Button _hideButton;
        [SerializeField] private Slider _audioValueSlider;

        [SerializeField] private ObjectAnimController _objectAnimController;

        [SerializeField] private GameObject _interstPointsObects;

        #endregion

        public AnimState currentAnimState = AnimState.none;

        #region Private Fields

        private GameObject _currentPointObject;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _startButton.onClick.AddListener(() =>
            {
                _startButton.gameObject.SetActive(false);
                _pauseButton.gameObject.SetActive(true);

                _audioSource.Play();
            });
            _pauseButton.onClick.AddListener(() =>
            {
                SetDefaultAudioPlayer();
            });
            _hideButton.onClick.AddListener(() =>
            {
                _audioSource.Stop();
                _audioSource.time = 0;
                Hide();
            });
            _audioValueSlider.onValueChanged.AddListener((value) =>
            {
                if(_audioSource.clip!=null)
                {
                    _audioSource.time = _audioValueSlider.value * _audioSource.clip.length;
                }
                else
                {
                    _audioValueSlider.value = 0;
                }
            });
        }

        private void Update()
        {
            if (_audioSource.isPlaying)
            {
                _audioValueSlider.value = _audioSource.time / _audioSource.clip.length;
            }
            else if (_pauseButton.gameObject.activeInHierarchy)
            {
                _audioValueSlider.value = 0;

                SetDefaultAudioPlayer();
            }
        }

        #endregion

        #region Overrides Methods

        public override void Hide()
        {
            base.Hide();
            _audioSource.Stop();
            _audioSource.time = 0;
            SetDefaultAudioPlayer();
            _interstPointsObects.SetActive(true);

            switch (currentAnimState)
            {
                case AnimState.plan1:
                    _objectAnimController.SetPlanirovshickExitState();
                    break;
                case AnimState.plan2:
                    _objectAnimController.SetPlanirovshick_2_ExitState();
                    break;
                case AnimState.vibroplit:
                    _objectAnimController.SetVibroplitaUpState();
                    break;
            }
            currentAnimState = AnimState.none;
        }

        #endregion

        #region Public Methods

        public void StartMessage(string messageText, AudioClip clip, GameObject pointObj)
        {
            Show();
            _messageText.text = messageText;
            _audioSource.Stop();
            _audioSource.clip = clip;
            _currentPointObject = pointObj;
            _interstPointsObects.SetActive(false);
        }


        public void SetPlanAnim()
        {
            currentAnimState = AnimState.plan1;
        }

        public void SetPlan2Anim()
        {
            currentAnimState = AnimState.plan2;
        }

        public void SetVibroAnim()
        {
            currentAnimState = AnimState.vibroplit;
        }
        #endregion

        #region Private Fields

        private void SetDefaultAudioPlayer()
        {
            _pauseButton.gameObject.SetActive(false);
            _startButton.gameObject.SetActive(true);

            _audioSource.Pause();
        }

        #endregion
    }
}