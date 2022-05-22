using Assets.CodeBase.Infrastructure;
using Assets.CodeBase.Infrastructure.StateMachine;

namespace Assets.CodeBase.Infrastructure.StateMachine.States
{
    public class GameLoopState : IState
    {
        #region Private Fields

        private GameStateMachine _stateMachine;
        private SceneLoader _sceneLoader;

        #endregion

        #region Constructors

        public GameLoopState(GameStateMachine gameStateMachine)
        {
            _stateMachine = gameStateMachine;
        }

        #endregion

        #region Interface Methods

        public void Enter()
        {

        }

        public void Exit()
        {

        }

        #endregion
    }
}