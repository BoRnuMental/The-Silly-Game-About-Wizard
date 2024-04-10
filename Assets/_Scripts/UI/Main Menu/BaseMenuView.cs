using UnityEngine;

public abstract class BaseMenuView : MonoBehaviour 
{
    protected BaseMenuPresenter _presenter;

    public void NewGameButtonClicked()
    {
        _presenter.OnNewGameButtonClicked();
    }

    public void ContinueButtonClicked()
    {
        _presenter.OnContinueButtonClicked();
    }

    public void ExitButtonClicked()
    {
        _presenter.OnExitButtonClicked();
    }

    public void SettingsDefaultButtonClicked()
    {
        _presenter.OnSettingsDefaultClicked();
    }

    public void SettingsApplyButtonClicked()
    {
        _presenter.OnSettingsApplyClicked();
    }
}
