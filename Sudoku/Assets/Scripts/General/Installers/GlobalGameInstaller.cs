using GameCore;
using General.GameSceneManager;
using General.SceneLoader;
using PlayborschUI.WindowProvider;
using PlayborschUI.WindowsManager;
using UnityEngine;
using Utilities.Logger;
using Utilities.SceneLoader;
using Zenject;

namespace General.Installers
{
    public class GlobalGameInstaller : MonoInstaller
    {
        [SerializeField] private WindowManager _windowManager;
        public override void InstallBindings()
        {
            LogHandler.LogInfo($"[{nameof(GlobalGameInstaller)}] Installing Project context");

            Container.Bind<ISceneLoader>().To<ClassicSceneLoader>().AsSingle();
            Container.Bind<IWindowProvider>().To<ResourcesWindowProvider>().AsSingle();
            Container.Bind<IWindowsManager>().To<WindowManager>().FromInstance(_windowManager).AsSingle();
            Container.Bind<IGameSceneManager>().To<GameSceneManager.GameSceneManager>().AsSingle();
        }
    }
}