using System;
using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    public abstract class GameEvent<TData>  : ScriptableObject
    {
        private readonly List<Action<TData>> _listeners = new List<Action<TData>>();

        public void Invoke(TData data) 
        {
            foreach(var listener in _listeners)
            {
                listener.Invoke(data);
            }
        }

        public void RegisterListener(Action<TData> listener) => _listeners.Add(listener);
        public void UnRegisterListener(Action<TData> listener) => _listeners.Remove(listener);
    }
}
