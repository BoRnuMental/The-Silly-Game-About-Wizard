using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MenuPresenter : BaseMenuPresenter
{
    [Inject]
    private void Construct(BaseMenuModel model)
    {
        _model = model;
    }

    public override void OnNewGameButtonClicked()
    {
        throw new System.NotImplementedException();
    }

    public override void OnContinueButtonClicked()
    {
        throw new System.NotImplementedException();
    }

    public override void OnExitButtonClicked()
    {
        throw new System.NotImplementedException();
    }

    public override void OnSettingsApplyClicked()
    {
        throw new System.NotImplementedException();
    }

    public override void OnSettingsDefaultClicked()
    {
        throw new System.NotImplementedException();
    }   
}
