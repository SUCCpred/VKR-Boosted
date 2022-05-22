using UnityEngine;

namespace Assets.CodeBase.Logic
{
    /// <summary>
    /// Base class for UI
    /// </summary>
    public class PageUI : MonoBehaviour
    {
        #region Unity Editor

        [SerializeField] private GameObject currentPageObject;

        #endregion

        #region Public Virtual Methods

        /// <summary>
        /// Show ui page
        /// </summary>
        public virtual void Show()
        {
            currentPageObject.SetActive(true);
        }

        /// <summary>
        /// Hide ui page
        /// </summary>
        public virtual void Hide()
        {
            currentPageObject.SetActive(false);
        }

        #endregion
    }
}