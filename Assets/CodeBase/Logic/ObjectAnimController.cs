using UnityEngine;

namespace Assets.CodeBase.Logic
{
    class ObjectAnimController : MonoBehaviour
    {

        #region Constants

        private const string VIBROPLITA_KEY = "isVibroplita";
        private const string PLAN_KEY = "isPlan";
        private const string PLAN_2_KEY = "isPlan_2";
        private const string IDLE_STATE = "isIdle";

        #endregion

        #region Unity Editor

        [SerializeField] private Animator _animator;

        #endregion

        #region Public Methods

        public void SetVibroplitaDownState()
        {
            _animator.SetBool(VIBROPLITA_KEY, true);
        }

        public void SetVibroplitaUpState()
        {
            _animator.SetBool(VIBROPLITA_KEY, false);
        }

        public void SetPlanirovshickStartState()
        {
            _animator.SetBool(PLAN_KEY, true);
        }

        public void SetPlanirovshickExitState()
        {
            _animator.SetBool(PLAN_KEY, false);
        }

        public void SetPlanirovshick_2_StartState()
        {
            _animator.SetBool(PLAN_2_KEY, true);
        }

        public void SetPlanirovshick_2_ExitState()
        {
            _animator.SetBool(PLAN_2_KEY, false);
        }

        private void SetIdleState()
        {
            _animator.SetBool(IDLE_STATE, true);
        }

        #endregion
    }
}
