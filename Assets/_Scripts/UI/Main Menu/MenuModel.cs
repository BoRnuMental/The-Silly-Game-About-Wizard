using UnityEngine;
using Zenject;

public class MenuModel : BaseMenuModel
{
    private GameSettings _settings;

    public MenuModel()
    {
        Time.timeScale = 1.0f;
    }

    [Inject]
    private void Construct(GameSettings settings)
    {
        _settings = settings;
        _settings.Load();
    }

    public override void SetSettings(GameSettingsStruct settings)
    {
        _settings.Save(settings);
        Screen.SetResolution(
            _settings.Resolution.width,
            _settings.Resolution.height,
            _settings.FullScreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed,
            new RefreshRate() 
            { 
                numerator = _settings.Resolution.refreshRateRatio.numerator, 
                denominator = _settings.Resolution.refreshRateRatio.denominator
            });
    }

    public override GameSettingsStruct GetSettings()
    {
        return new()
        {
            fullScreen = _settings.FullScreen,
            resolution = new(_settings.Resolution.width, _settings.Resolution.height),
            refreshRateNumerator = _settings.Resolution.refreshRateRatio.numerator,
            refreshRateDenominator = _settings.Resolution.refreshRateRatio.denominator,
            globalVolume = _settings.GlobalVolume,
            musicVolume = _settings.MusicVolume,
            localizationId = _settings.LocalizationId
        };
    }
}
