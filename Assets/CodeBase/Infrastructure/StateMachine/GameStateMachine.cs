using Assets.CodeBase.Infrastructure;
using Assets.CodeBase.Infrastructure.StateMachine.States;
using Assets.CodeBase.Logic;
using System;
using System.Collections.Generic;

namespace Assets.CodeBase.Infrastructure.StateMachine
{
    public class GameStateMachine
    {
        #region Properties

        public LoadingCurtain LoadingCurtain { get => _loadingCurtain; set => _loadingCurtain = value; }

        #endregion

        #region Private Fields

        private readonly Dictionary<Type, IExtableState> _states;
        private IExtableState _activeState;
        private LoadingCurtain _loadingCurtain;

        #endregion

        #region Public Methods

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
        {
            _loadingCurtain = loadingCurtain;
            _states = new Dictionary<Type, IExtableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader),
                [typeof(GameLoopState)] = new GameLoopState(this),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        #endregion

        #region Private Methods

        private TState GetState<TState>() where TState : class, IExtableState
        {
            return _states[typeof(TState)] as TState;
        }

        private TState ChangeState<TState>() where TState : class, IExtableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }
        #endregion
    }
}
