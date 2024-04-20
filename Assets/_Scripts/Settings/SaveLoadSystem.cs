using System;
using System.IO;
using UnityEngine;
public static class SaveLoadSystem
{
    private const string SETTINGS_FILE_NAME = "Settings.json";
    private const string BEST_TIME_FILE_NAME = "Best Time.json";

    private static string _settingsPath;
    private static string _bestTimePath;

    static SaveLoadSystem()
    {
        _settingsPath = Path.Combine(Application.dataPath, SETTINGS_FILE_NAME);
        _bestTimePath = Path.Combine(Application.dataPath, BEST_TIME_FILE_NAME);
    }
    public static void SaveSettings(GameSettingsStruct settings)
    {
        string json = JsonUtility.ToJson(settings, true);
        try
        {
            File.WriteAllText(_settingsPath, json);
        }
        catch
        {
            Debug.LogWarning("Can't save the settings file");
        }
    }
    public static GameSettingsStruct LoadSettings()
    {
        if (!File.Exists(_settingsPath))
        {
            SaveSettings(GameSettingsStruct.Default);
            return GameSettingsStruct.Default;
        }
        string json = File.ReadAllText(_settingsPath);
        return JsonUtility.FromJson<GameSettingsStruct>(json);

    }
    public static float LoadBestTime()
    {
        if (File.Exists(_bestTimePath))
        {
            string json = File.ReadAllText(_bestTimePath);
            return JsonUtility.FromJson<SerializableFloat>(json).value;
        }
        return 0f;
    }
    public static void SaveBestTime(float seconds)
    {
        string json = JsonUtility.ToJson(new SerializableFloat(seconds), true);
        try
        {
            File.WriteAllText(_bestTimePath, json);
        }
        catch
        {
            Debug.LogWarning("Can't save the best time file");
        }
    }
}

[Serializable]
public struct SerializableFloat
{
   public float value;

    public SerializableFloat(float value)
    {
        this.value = value;
    }
}
