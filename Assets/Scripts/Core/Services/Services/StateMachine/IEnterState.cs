namespace DoodleJump.Core.Services
{
    public interface IEnterState : IExitState
    {
        public void Enter();
    }

    public interface IEnterState<TPayload> : IExitState
    {
        public void Enter(TPayload payload);
    }

    public interface IEnterState<TPayload1, TPayload2> : IExitState
    {
        public void Enter(TPayload1 payload1, TPayload2 payload2);
    }
}
