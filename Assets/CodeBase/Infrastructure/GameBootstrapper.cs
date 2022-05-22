using Assets.CodeBase.Infrastructure.StateMachine.States;
using Assets.CodeBase.Logic;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure
{
    /// <summary>
    /// Точка начала работы модуля
    /// Здесь происходит инициализация "игровой" сессии
    /// </summary>
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        #region Properties

        public static GameBootstrapper Instance { get => instance; set => instance = value; }
        public Game Game { get => _game; set => _game = value; }

        #endregion

        #region Unity Editor

        [SerializeField] private LoadingCurtain _loadingCurtain;

        #endregion

        #region Private Fields

        private Game _game;
        private static GameBootstrapper instance = null;

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
        }

        private void Start()
        {
            Game = new Game(this, _loadingCurtain);
            Game.StateMachine.Enter<BootstrapState>();
        }

        #endregion
    }
}