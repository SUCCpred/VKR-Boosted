namespace Assets.CodeBase.Infrastructure.StateMachine
{
    public interface IPayloadedState<TPayload> : IExtableState
    {
        void Enter(TPayload payload);
    }

}