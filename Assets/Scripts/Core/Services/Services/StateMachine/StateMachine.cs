using System;
using System.Collections.Generic;

namespace DoodleJump.Core.Services
{
    internal sealed class StateMachine : IStateMachine
    {
        private IExitState _currentState;

        private Dictionary<Type, IState> _states = new();

        internal StateMachine()
        {
            _states = new Dictionary<Type, IState>();
        }

        public void Enter<TState>() where TState : class, IEnterState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }

        public void Add<TState>(TState state) where TState : class, IState
        {
            var type = typeof(TState);

            if (_states.ContainsKey(type))
                _states[type] = state;
            else
                _states.Add(type, state);
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IEnterState<TPayload>
        {
            var state = ChangeState<TState>();
            state.Enter(payload);
        }

        public void Destroy()
        {
            _currentState?.Exit();
            _currentState = null;

            _states.Clear();
            _states = null;
        }

        private TState ChangeState<TState>() where TState : class, IExitState
        {
            _currentState?.Exit();

            var state = GetState<TState>();

            _currentState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}
