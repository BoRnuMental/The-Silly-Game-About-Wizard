using UnityEngine;
using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    [SerializeField] private BaseMenuView _view;
    [SerializeField] private SoundSystem _soundSystem;
    [SerializeField] private FadeInOut _fade;

    public override void InstallBindings()
    {
        MenuPresenter presenter = new MenuPresenter();
        MenuModel model = new MenuModel();

        Container.Bind<BaseMenuPresenter>().To<MenuPresenter>().FromInstance(presenter).AsSingle();
        Container.Bind<BaseMenuModel>().To<MenuModel>().FromInstance(model).AsSingle();
        Container.Bind<SoundSystem>().FromInstance(_soundSystem).AsSingle();
        Container.BindInstance(_fade).AsSingle();
        Container.QueueForInject(model);
        Container.QueueForInject(presenter);
        Container.QueueForInject(_view);
    }
}
