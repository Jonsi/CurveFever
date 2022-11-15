using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Events;
using TMPro;
using UniRx;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countDownText;
    [SerializeField] private VoidGameEvent _gameStartedEvent;
    [SerializeField] private VoidGameEvent _gameInitializedEvent;

    private void OnEnable()
    {
        _gameInitializedEvent.RegisterListener(StartCountDown);
    }

    private void OnDisable()
    {
        _gameInitializedEvent.UnRegisterListener(StartCountDown);
    }
    
    private async void StartCountDown()
    {
        for (int i = 3; i > 0; i--)
        {
            SetText(i.ToString());
            await UniTask.Delay(1000);
        }

        _countDownText.gameObject.SetActive(false);
        _gameStartedEvent.Invoke();
    }
    
    private void SetText(string text)
    {
        _countDownText.text = text;
    }
}
