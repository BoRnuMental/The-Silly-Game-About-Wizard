using Zenject;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.Linq;
using DG.Tweening;

public class MenuView : BaseMenuView
{
    [SerializeField] private TMP_Dropdown _resolutions;
    [SerializeField] private Toggle _fullScreen;
    [SerializeField] private Slider _globalVolume;
    [SerializeField] private Slider _musicVolume;
    [SerializeField] private GameObject _bestTime;

    private float _time;
    private SoundSystem _soundSystem;

    [Inject]
    private void Construct(BaseMenuPresenter presenter, SoundSystem soundSystem)
    {
        _presenter = presenter;
        _soundSystem = soundSystem;
    }

    private void Awake()
    {
        _time = SaveLoadSystem.LoadBestTime();
        if (_time > 0f)
        {
            ShowBestTime();
            var timeSpan = TimeSpan.FromSeconds(_time);
            var text = _bestTime.GetComponentInChildren<TMP_Text>();
            if (timeSpan.Hours > 0)
            {
                text.text += $"{timeSpan.Hours:00}:{timeSpan.Minutes:00}:{timeSpan.Seconds:00}:{timeSpan.Milliseconds / 10:00}";
                return;
            }
            text.text += $"{timeSpan.Minutes:00}:{timeSpan.Seconds:00}:{timeSpan.Milliseconds / 10:00}";
        }
    }

    public void ShowBestTime()
    {
        if (_time > 0f)
        {
            _bestTime.SetActive(true);
        }
    }
    public override void OnDefaultButtonClicked()
    {
        var settings = GameSettingsStruct.Default;
        _fullScreen.isOn = settings.fullScreen;
        _resolutions.value = Screen.resolutions.Length - 1;
        _globalVolume.value = settings.globalVolume;
        _musicVolume.value = settings.musicVolume;
    }

    public override void OnApplyButtonClicked()
    {
        var newResolution = Screen.resolutions.Reverse().ToArray()[_resolutions.value];
        GameSettingsStruct settings = new()
        {
            fullScreen = _fullScreen.isOn,
            resolution = new(newResolution.width, newResolution.height),
            refreshRateNumerator = newResolution.refreshRateRatio.numerator,
            refreshRateDenominator = newResolution.refreshRateRatio.denominator,  
            globalVolume = _globalVolume.value,
            musicVolume = _musicVolume.value
        };
        _presenter.SetSettings(settings);
        _soundSystem.ChangeGlobalVolume(settings.globalVolume);
        _soundSystem.ChangeMusicVolume(settings.musicVolume);
    }

    public override void OnPlayButtonClicked()
    {
        DOTween.KillAll();
        SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
    }
}