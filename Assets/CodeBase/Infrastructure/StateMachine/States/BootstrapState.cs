using Assets.CodeBase.Infrastructure;
using Assets.CodeBase.Infrastructure.StateMachine;
using Assets.CodeBase.Logic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.CodeBase.Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
        #region Constants

        private const string BOOTSTRP_SCENE_NAME = "BootstrapScene";
        private const string GAME_SCENE_NAME = "Menu_Scene";

        #endregion

        #region Private Fields

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        #endregion

        #region Constructors

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        #endregion

        #region Public Methods

        public void Enter()
        {
            _stateMachine.LoadingCurtain.Show();

            RegisterServices();

            EnterLoadLevel();
        }

        public void Exit()
        {
            //_stateMachine.LoadingCurtain.Hide();
        }

        #endregion

        #region Private Methods

        private void EnterLoadLevel()
        {

            _stateMachine.Enter<LoadLevelState, string>(GAME_SCENE_NAME);
            
        }

        private void RegisterServices()
        {

        }

        #endregion

    }
}
