using System.IO;
using UnityEngine;
public static class SaveLoadSystem
{
    private const string SETTINGS_FILE_NAME = "Settings.json";
    private static string _path;

    static SaveLoadSystem()
    {
        _path = Path.Combine(Application.dataPath, SETTINGS_FILE_NAME);
    }
    public static void Save(GameSettingsStruct settings)
    {
        string json = JsonUtility.ToJson(settings, true);
        try
        {
            File.WriteAllText(_path, json);
        }
        catch
        {
            Debug.LogWarning("Can't save the settings file");
        }
    }

    public static GameSettingsStruct Load()
    {
        if (!File.Exists(_path))
        {
            Save(GameSettingsStruct.Default);
            return GameSettingsStruct.Default;
        }
        string json = File.ReadAllText(_path);
        return JsonUtility.FromJson<GameSettingsStruct>(json);

    }
}
