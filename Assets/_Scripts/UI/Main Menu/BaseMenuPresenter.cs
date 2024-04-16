using UnityEngine;
using Zenject;

public abstract class BaseMenuPresenter
{
    protected BaseMenuModel _model;
    abstract public void SetSettings(GameSettingsStruct settings);
    abstract public GameSettingsStruct GetSettings();
}