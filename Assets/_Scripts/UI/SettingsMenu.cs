using ModestTree;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _resolutions;
    [SerializeField] private Toggle _fullscreen;
    [SerializeField] private Slider _globalVolume;
    [SerializeField] private Slider _musicVolume;

    private GameSettings _settings;

    [Inject]
    private void Construct(GameSettings settings)
    {
        _settings = settings;
        _settings.Load();
    }
    private void Awake()
    {
        Debug.Log($"Screen.currentResolution: {Screen.currentResolution}\nScreen.width + Screen.height: {Screen.width}x{Screen.height}");
        _resolutions.options.Clear();
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            _resolutions.options.Add(
                new TMP_Dropdown.OptionData($"{Screen.resolutions[i].width}x{Screen.resolutions[i].height}"));
        }
        _resolutions.value = GetResolutionIndex(_settings.Resolution);
        _fullscreen.isOn = _settings.FullScreen;
        _globalVolume.value = _settings.GlobalVolume;
        _musicVolume.value = _settings.MusicVolume;
    }
    private int GetResolutionIndex(Resolution resolution)
    {
        var resolutions = Screen.resolutions;
        for(int i = 0; i < resolutions.Length;i++)
        {
            if (resolutions[i].width == resolution.width && resolutions[i].height == resolution.height) return i;
        }
        return -1;
    }
    public void OnApplyButtonClicked()
    {
        var newResolution = Screen.resolutions[_resolutions.value];
        GameSettingsStruct settings = new()
        {
            fullScreen = _fullscreen.isOn,
            resolution = new(newResolution.width, newResolution.height),
            globalVolume = _globalVolume.value,
            musicVolume = _musicVolume.value
        };
        _settings.Save(settings);
    }

    public void OnDefaultButtonClicked()
    {
        var settings = GameSettingsStruct.Default;
        _fullscreen.isOn = settings.fullScreen;
        _resolutions.value = Screen.resolutions.IndexOf(Screen.currentResolution);
        _globalVolume.value = settings.globalVolume;
        _musicVolume.value = settings.musicVolume;
    }
}
