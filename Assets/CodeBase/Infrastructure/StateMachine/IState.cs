namespace Assets.CodeBase.Infrastructure.StateMachine
{
    public interface IState : IExtableState
    {
        void Enter();
    }
}