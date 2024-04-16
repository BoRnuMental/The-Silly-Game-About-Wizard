using UnityEngine;

public abstract class BaseMenuView : MonoBehaviour 
{
    protected BaseMenuPresenter _presenter;
    public abstract void OnDefaultButtonClicked();
    public abstract void OnApplyButtonClicked();
    public abstract void OnPlayButtonClicked();

}
