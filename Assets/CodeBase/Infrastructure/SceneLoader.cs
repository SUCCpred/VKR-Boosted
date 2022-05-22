using Assets.CodeBase.Logic;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.CodeBase.Infrastructure
{
    public class SceneLoader
    {
        #region Private Fields

        private ICoroutineRunner _coroutineRunner;

        #endregion

        #region Constructors

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        #endregion

        #region Public Methods

        public void Load(string name, LoadingCurtain loadingCurtain, Action onLoaded = null)
        {
            _coroutineRunner.StartCoroutine(LoadScene(name, loadingCurtain, onLoaded));
        }

        public IEnumerator LoadScene(string name, LoadingCurtain loadingCurtain, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == name)
            {
                onLoaded?.Invoke();
                yield break;
            }

            yield return null;

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(name);
            //waitNextScene.allowSceneActivation = false;

            while (!waitNextScene.isDone)
            {
                loadingCurtain.SetProgressValue(waitNextScene.progress);
                yield return null;
            }
            //waitNextScene.allowSceneActivation = true;

            onLoaded?.Invoke();
        }

        #endregion
    }
}