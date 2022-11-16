using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUi : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Slider _slider;
    [SerializeField] private Text _sliderValueText;

    [SerializeField] private GameSettings _gameSettings;
    private void OnEnable()
    {
        _playButton.onClick.AddListener(OnPlayButtonClicked);
        _slider.onValueChanged.AddListener(SyncSlideTextView);
    }

    private void Start()
    {
        SyncSlideTextView(_slider.value);
    }

    private void SyncSlideTextView(float arg0)
    {
        _sliderValueText.text = ((int) _slider.value).ToString();
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OnPlayButtonClicked);
        _slider.onValueChanged.RemoveListener(SyncSlideTextView);
    }

    private void OnPlayButtonClicked()
    {
        _gameSettings.PlayerCount = (int) _slider.value;
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }
}
