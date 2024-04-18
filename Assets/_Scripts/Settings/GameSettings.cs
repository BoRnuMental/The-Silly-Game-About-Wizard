using System;
using UnityEngine;

public class GameSettings
{

    private bool _fullScreen;
    private Resolution _resolution;

    private float _globalVolume;
    private float _musicVolume;

    public GameSettings()
    {
        Load();
    }

    public bool FullScreen => _fullScreen;
    public Resolution Resolution => _resolution;
    public float GlobalVolume => _globalVolume;
    public float MusicVolume => _musicVolume;

    public void Save(GameSettingsStruct settings)
    {      
        SaveLoadSystem.SaveSettings(settings);
        Set(settings);
    }

    public void Load()
    {
        var settings = SaveLoadSystem.LoadSettings();
        Set(settings);
    }

    public void Set(GameSettingsStruct settings)
    {
        _fullScreen = settings.fullScreen;
        _resolution = new Resolution()
        {
            width = settings.resolution.x,
            height = settings.resolution.y,
            refreshRateRatio = new RefreshRate
            {
                numerator = settings.refreshRateNumerator,
                denominator = settings.refreshRateDenominator,
            }
        };     

        _globalVolume = settings.globalVolume;
        _musicVolume = settings.musicVolume;
        Screen.SetResolution(_resolution.width, _resolution.height, _fullScreen);
        Application.targetFrameRate = (int)_resolution.refreshRateRatio.value;

        if (_fullScreen)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}

[Serializable]
public struct GameSettingsStruct
{

    public bool fullScreen;
    public Vector2Int resolution;
    public uint refreshRateNumerator;
    public uint refreshRateDenominator;

    public float globalVolume;
    public float musicVolume;

    public GameSettingsStruct(bool fullScreen, Resolution resolution, float globalVolume, float musicVolume)
    {
        this.fullScreen = fullScreen;
        this.resolution = new(resolution.width, resolution.height);
        this.refreshRateNumerator =  resolution.refreshRateRatio.numerator;
        this.refreshRateDenominator = resolution.refreshRateRatio.denominator;
        this.globalVolume = globalVolume;
        this.musicVolume = musicVolume;
    }

    public static GameSettingsStruct Default => new GameSettingsStruct
    {
        fullScreen = false,
        resolution =  new(Screen.resolutions[0].width, Screen.resolutions[0].height),
        refreshRateNumerator = Screen.resolutions[0].refreshRateRatio.numerator,
        refreshRateDenominator = Screen.resolutions[0].refreshRateRatio.denominator,
        globalVolume = 0.5f,
        musicVolume = 0.5f,
    };
}