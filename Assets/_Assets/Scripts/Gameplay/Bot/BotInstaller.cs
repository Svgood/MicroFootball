using Zenject;

namespace _Assets.Scripts.Gameplay.Bot
{
    public class BotInstaller : Installer<BotInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BotFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<BotCollisionModel>().AsSingle();
        }
    }
}