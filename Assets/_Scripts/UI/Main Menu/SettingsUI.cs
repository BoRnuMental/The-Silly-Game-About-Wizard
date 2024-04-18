using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _resolutions;
    [SerializeField] private Toggle _fullScreen;
    [SerializeField] private Slider _globalVolume;
    [SerializeField] private Slider _musicVolume;
    private BaseMenuPresenter _presenter;

    [Inject]
    private void Construct(BaseMenuPresenter presenter)
    {
        _presenter = presenter;
    }

    private void OnEnable()
    {
        var screenResolutions = Screen.resolutions.Reverse().ToArray();
        var settings = _presenter.GetSettings();
        _resolutions.options.Clear();
        for (int i = 0; i < screenResolutions.Length; i++)
        {
            _resolutions.options.Add(
                new TMP_Dropdown.OptionData(
                    $"{screenResolutions[i].width}x{screenResolutions[i].height} " +
                    $"{Math.Round(screenResolutions[i].refreshRateRatio.value, 1)} hz"));
        }
        Resolution resolution = new()
        {
            width = settings.resolution.x,
            height = settings.resolution.y,
            refreshRateRatio = new RefreshRate()
            {
                numerator = settings.refreshRateNumerator,
                denominator = settings.refreshRateDenominator
            }
        };
        _resolutions.value = GetResolutionIndex(resolution);
        _fullScreen.isOn = settings.fullScreen;
        _globalVolume.value = settings.globalVolume;
        _musicVolume.value = settings.musicVolume;
    }
    private int GetResolutionIndex(Resolution resolution)
    {
        var screenResolutions = Screen.resolutions.Reverse().ToArray();
        for (int i = 0; i < screenResolutions.Length; i++)
        {
            if (screenResolutions[i].width == resolution.width &&
                screenResolutions[i].height == resolution.height &&
                screenResolutions[i].refreshRateRatio.value == resolution.refreshRateRatio.value) return i;
        }
        return -1;
    }
}
