using Assets.CodeBase.Infrastructure;
using Assets.CodeBase.Infrastructure.StateMachine;
using Assets.CodeBase.Logic;

namespace Assets.CodeBase.Infrastructure.StateMachine.States
{
    internal class LoadLevelState : IPayloadedState<string>
    {

        #region Private Fields

        private GameStateMachine _stateMachine;
        private SceneLoader _sceneLoader;

        #endregion

        #region Constructors

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        #endregion

        #region Interface MEthods

        public void Enter(string sceneName)
        {
            _stateMachine.LoadingCurtain.Show();
            _sceneLoader.Load(sceneName, _stateMachine.LoadingCurtain, OnLoaded);
        }

        public void Exit()
        {
            _stateMachine.LoadingCurtain.Hide();
        }

        #endregion

        #region Private Methods

        private void OnLoaded()
        {
            _stateMachine.Enter<GameLoopState>();
        }

        #endregion
    }
}