using System;
using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu(menuName = "Events/Void Event")]
    public class VoidGameEvent : ScriptableObject
    {
        private readonly List<Action> _listeners = new List<Action>();

        public void Invoke() 
        {
            foreach(var listener in _listeners)
            {
                listener.Invoke();
            }
        }

        public void RegisterListener(Action listener) => _listeners.Add(listener);
        public void UnRegisterListener(Action listener) => _listeners.Remove(listener);
    }
}