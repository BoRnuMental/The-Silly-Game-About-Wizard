using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Toggle _fullScreen;
    [SerializeField] private Slider _globalVolume;
    [SerializeField] private Slider _musicVolume;

    private GameSettings _settings;

    [Inject]
    private void Construct(GameSettings settings)
    {
        _settings = settings;
        _settings.Load();
    }
    private void OnEnable()
    {
        _fullScreen.isOn = _settings.FullScreen;
        _globalVolume.value = _settings.GlobalVolume;
        _musicVolume.value = _settings.MusicVolume;
    }

    public void OnApplyButtonClicked()
    {
        GameSettingsStruct settings = new()
        {
            fullScreen = _fullScreen.isOn,
            resolution = new(_settings.Resolution.width, _settings.Resolution.height),
            refreshRateNumerator = _settings.Resolution.refreshRateRatio.numerator,
            refreshRateDenominator = _settings.Resolution.refreshRateRatio.denominator,
            globalVolume = _globalVolume.value,
            musicVolume = _musicVolume.value
        };
        _settings.Save(settings);
    }
}