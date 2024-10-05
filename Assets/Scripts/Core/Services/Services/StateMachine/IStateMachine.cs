namespace DoodleJump.Core.Services
{
    public interface IStateMachine : IService
    {
        public void Add<TState>(TState state) where TState : class, IState;

        public void Enter<TState>() where TState : class, IEnterState;

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IEnterState<TPayload>;

        public void Enter<TState, TPayload1, TPayload2>(TPayload1 payload1, TPayload2 payload2) where TState : class, IEnterState<TPayload1, TPayload2>;
    }
}
