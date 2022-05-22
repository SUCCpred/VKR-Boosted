using Assets.CodeBase.Infrastructure.StateMachine;
using Assets.CodeBase.Logic;

namespace Assets.CodeBase.Infrastructure
{
    /// <summary>
    /// Основной контейнер - "игровая" сессия
    /// </summary>
    public class Game
    {

        #region Properties

        public GameStateMachine StateMachine { get => _stateMachine; set => _stateMachine = value; }

        #endregion

        #region Private Fields

        private GameStateMachine _stateMachine;

        #endregion

        #region Constructors

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain);
        }

        #endregion

    }
}