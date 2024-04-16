using Zenject;

public class MenuPresenter : BaseMenuPresenter
{
    [Inject]
    private void Construct(BaseMenuModel model)
    {
        _model = model;
    }
    public override void SetSettings(GameSettingsStruct settings)
    {
        _model.SetSettings(settings);
    }

    public override GameSettingsStruct GetSettings()
    {
        return _model.GetSettings();
    }
}
