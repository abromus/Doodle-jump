using System;
using System.Collections.Generic;

namespace DoodleJump.Core.Services
{
    public sealed class StateMachine : IStateMachine
    {
        private IExitState _currentState;

        private readonly Dictionary<Type, IState> _states = new(8);

        public void Enter<TState>() where TState : class, IEnterState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IEnterState<TPayload>
        {
            var state = ChangeState<TState>();
            state.Enter(payload);
        }

        public void Enter<TState, TPayload1, TPayload2>(TPayload1 payload1, TPayload2 payload2) where TState : class, IEnterState<TPayload1, TPayload2>
        {
            var state = ChangeState<TState>();
            state.Enter(payload1, payload2);
        }

        public void Add<TState>(TState state) where TState : class, IState
        {
            var type = typeof(TState);

            if (_states.ContainsKey(type))
                _states[type] = state;
            else
                _states.Add(type, state);
        }

        public void Destroy()
        {
            _currentState?.Exit();
            _currentState = null;

            _states.Clear();
        }

        private TState ChangeState<TState>() where TState : class, IExitState
        {
            _currentState?.Exit();

            var state = GetState<TState>();

            _currentState = state;

            return state;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private TState GetState<TState>() where TState : class, IState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}
