using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGround
{
    public enum State
    {
        SwitchOn,
        EnterMoney,
        DispenseCandy,
        SwitchOff
    }
    public enum Trigger
    {
        CoinInserted,
        ButtonPressed,
        CandyDispensed,
    }
    public class StateMachine<TState,TTrigger>
{
        private readonly Dictionary<(TState, TTrigger), TState> _transitions;
        public TState CurrentState { get; private set; }
        public StateMachine(TState initialState)
        {
            CurrentState = initialState;
            _transitions = new Dictionary<(TState, TTrigger), TState>();
        }
        
        public void AddTransition(TState from, TTrigger trigger, TState to)
        {
            _transitions[(from, trigger)] = to;
        }

        public void Fire(TTrigger trigger)
        {
            var key = (CurrentState, trigger);
            if (_transitions.TryGetValue(key, out var nextState))
            {
                CurrentState = nextState;
            }
            else
            {
                throw new InvalidOperationException($"No transition defined for {CurrentState} on {trigger}");
            }
        }
    }
}
