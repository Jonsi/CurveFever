using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/GameEvent")]
public class GameEvent : ScriptableObject
{
    private HashSet<GameEventListener> _listeners = new HashSet<GameEventListener>();

    public void Invoke()
    {
        foreach(GameEventListener listener in _listeners)
        {
            listener.RaiseEvent();
        }
    }

    public void RegisterLisener(GameEventListener lisener) => _listeners.Add(lisener);
    public void UnRegisterLisener(GameEventListener lisener) => _listeners.Remove(lisener);
}
