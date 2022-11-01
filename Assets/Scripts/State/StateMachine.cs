using UnityEngine;

namespace State
{
    public abstract class StateMachine : IStateMachine
    {
        private StateBase _currentState;
        
        
        public virtual void SetState(StateBase state)
        {
            if(state == _currentState)
            {
                return;
            }

            _currentState?.ExitState();
            _currentState = state;
            _currentState.EnterState();
        }
    }
}