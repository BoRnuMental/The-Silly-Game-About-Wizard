using Zenject;

public abstract class BaseMenuModel
{
    public abstract void SetSettings(GameSettingsStruct settings);
    public abstract GameSettingsStruct GetSettings(); 
}
