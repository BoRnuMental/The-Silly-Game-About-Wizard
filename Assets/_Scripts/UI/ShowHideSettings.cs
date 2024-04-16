using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ShowHideSettings : MonoBehaviour
{
    private GameObject _settings;

    [Inject]
    private void Construct([Inject(Id ="Settings")] GameObject settings)
    {
        _settings = settings;
    }
    public void Show()
    {
        _settings.SetActive(true);
    }

    public void Hide()
    {
        _settings.SetActive(false);
    }
}
