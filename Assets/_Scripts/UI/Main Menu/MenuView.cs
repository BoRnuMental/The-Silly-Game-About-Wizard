using Zenject;
using UnityEngine;

public class MenuView : BaseMenuView
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _settings;

    [Inject]
    private void Construct(BaseMenuPresenter presenter)
    {
        _presenter = presenter;
    }

    public void ShowSettings()
    {
        _settings.SetActive(true);
        _menu.SetActive(false);
    }
    public void ShowMenu()
    {
        _menu.SetActive(true);
        _settings.SetActive(false);
    }
}
