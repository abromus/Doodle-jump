namespace DoodleJump.Core.Services
{
    internal interface IEnterState : IExitState
    {
        public void Enter();
    }

    internal interface IEnterState<TPayload> : IExitState
    {
        public void Enter(TPayload payload);
    }
}
