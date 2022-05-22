using Assets.CodeBase.Infrastructure;
using Assets.CodeBase.Infrastructure.StateMachine.States;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button _spaceMachineButton;
    [SerializeField] private Button _QRCodeReadButton;

    private void Awake()
    {
        _spaceMachineButton.onClick.AddListener(() =>
        {
            GameBootstrapper.Instance.Game.StateMachine.Enter<LoadLevelState, string>("AR_Ground_Scene");
        });
        _QRCodeReadButton.onClick.AddListener(() =>
        {
            GameBootstrapper.Instance.Game.StateMachine.Enter<LoadLevelState, string>("QRScaner");
        });
    }
}
