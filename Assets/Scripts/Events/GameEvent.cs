using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GameEvent")]
    public class GameEvent<TData> : ScriptableObject
    {
        private readonly List<GameEventListener<TData>> _listeners = new List<GameEventListener<TData>>();

        public void Invoke(TData data) 
        {
            foreach(var listener in _listeners)
            {
                listener.RaiseEvent(data);
            }
        }

        public void RegisterListener(GameEventListener<TData> listener) => _listeners.Add(listener);
        public void UnRegisterListener(GameEventListener<TData> listener) => _listeners.Remove(listener);
    }
}
