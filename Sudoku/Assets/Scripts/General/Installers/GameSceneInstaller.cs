using GameCore;
using General.Game;
using PlayborschUI.WindowsManager;
using Zenject;

namespace General.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var gameEvents = new GameEventsManager();
            
            Container.Bind<IGameEventsInvoker>().To<GameEventsManager>().FromInstance(gameEvents);
            Container.Bind<IGameEventsListener>().To<GameEventsManager>().FromInstance(gameEvents);
            Container.Bind<IGameManager>().To<GameManager>().AsSingle();
            Container.Bind<IPainter>().To<NumberPainter>().AsSingle();
            Container.Bind<IGameField>().To<GameField>().AsSingle();
            
            var windowsManager = Container.Resolve<IWindowsManager>();
            Container.Inject(windowsManager);
        }
    }
}