using UnityEngine;
using Zenject;

public abstract class BaseMenuPresenter
{
    protected BaseMenuModel _model;
    abstract public void OnNewGameButtonClicked();
    abstract public void OnContinueButtonClicked();
    abstract public void OnExitButtonClicked();
    abstract public void OnSettingsApplyClicked();
    abstract public void OnSettingsDefaultClicked();
}

