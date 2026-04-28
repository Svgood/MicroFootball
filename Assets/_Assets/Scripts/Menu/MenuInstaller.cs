using MicroFootball.Configs;
using MicroFootball.Menu.Model;
using MicroFootball.Menu.Presenter;
using MicroFootball.Menu.View;
using UnityEngine;
using Zenject;

namespace MicroFootball.Menu.Installers
{
    public sealed class MenuInstaller : MonoInstaller
    {
        [SerializeField] private MenuSettings _settings;
        [SerializeField] private MenuView _view;

        public override void InstallBindings()
        {
            Container.BindInstance(_settings).IfNotBound();
            Container.BindInstance(_view).IfNotBound();

            Container.Bind<MenuModel>().AsSingle();
            Container.BindInterfacesTo<MenuPresenter>().AsSingle().NonLazy();
        }
    }
}
