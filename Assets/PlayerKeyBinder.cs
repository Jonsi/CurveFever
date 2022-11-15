using System;
using Cysharp.Threading.Tasks;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerKeyBinder : MonoBehaviour
{
    [SerializeField] private PlayerSettings _playerSettings;
    [SerializeField] private Button _bindKeysButton;

    [SerializeField] private TextMeshProUGUI _leftKeyText;
    [SerializeField] private TextMeshProUGUI _rightKeyText;

    private void OnEnable()
    {
        _bindKeysButton.onClick.AddListener(OnBindKeysClicked);
    }

    private void OnDisable()
    {
        _bindKeysButton.onClick.RemoveListener(OnBindKeysClicked);
    }

    private void Start()
    {
        _leftKeyText.text = _playerSettings.LeftKey.ToString();
        _rightKeyText.text = _playerSettings.RightKey.ToString();
    }

    private async void OnBindKeysClicked()
    {
        var leftKey = await Utils.KeyPressListener.ListenToPressedKey();
        if (leftKey == null)
        {
            throw new Exception("invalid key pressed");
        }
        _leftKeyText.text = leftKey.ToString();
        _playerSettings.LeftKey = (KeyCode)leftKey;
        
        await UniTask.WaitUntil(() => Input.anyKey == false);
        
        var rightKey = await Utils.KeyPressListener.ListenToPressedKey();
        if (rightKey == null)
        {
            throw new Exception("invalid key pressed");
        }
        _rightKeyText.text = rightKey.ToString();
        _playerSettings.RightKey = (KeyCode)rightKey;
    }
}