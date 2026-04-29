using _Assets.Scripts.Common;
using _Assets.Scripts.Configs;
using UnityEngine;
using Zenject;

namespace _Assets.Scripts.Menu
{
    public sealed class MenuInstaller : MonoInstaller
    {
        [SerializeField] private MenuSettings _settings;
        [SerializeField] private MenuView _view;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CustomDisposable>().AsSingle();
            Container.BindInstance(_settings).IfNotBound();
            Container.BindInstance(_view).IfNotBound();

            Container.Bind<MenuModel>().AsSingle();
            Container.BindInterfacesTo<MenuPresenter>().AsSingle().NonLazy();
            Container.BindInterfacesTo<MenuTrigger>().AsSingle();
        }
    }
}
