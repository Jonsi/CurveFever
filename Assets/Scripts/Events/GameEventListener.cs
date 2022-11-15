using System;

namespace Events
{
    public abstract class GameEventListener<TData>
    {
        private GameEvent<TData> _gameEvent;
        private Action<TData> _action;

        protected GameEventListener(GameEvent<TData> gameEvent, Action<TData> action)
        {
            _gameEvent = gameEvent;
            _action = action;
        }

        public void RaiseEvent(TData data) => _action.Invoke(data);
    }
}