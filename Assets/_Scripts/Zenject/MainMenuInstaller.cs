using UnityEngine;
using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    [SerializeField] private BaseMenuView _view;

    public override void InstallBindings()
    {
        MenuPresenter presenter = new MenuPresenter();
        MenuModel model = new MenuModel();

        Container.QueueForInject(new object[] { model, presenter, _view });
        Container.Bind<BaseMenuPresenter>().To<MenuPresenter>().FromInstance(presenter).AsSingle();
        Container.Bind<BaseMenuModel>().To<MenuModel>().FromInstance(model).AsSingle();     
    }
}
