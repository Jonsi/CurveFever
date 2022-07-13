using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent _gameEvent;
    [SerializeField] private UnityEvent _unityAction;

    private void OnEnable() => _gameEvent.RegisterLisener(this);

    private void OnDisable() => _gameEvent.UnRegisterLisener(this);

    public void RaiseEvent() => _unityAction.Invoke();
}