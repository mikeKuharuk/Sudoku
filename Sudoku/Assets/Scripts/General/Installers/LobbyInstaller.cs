using PlayborschUI;
using PlayborschUI.WindowProvider;
using Utilities.Logger;
using Zenject;

namespace General.Installers
{
    public class LobbyInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            LogHandler.LogInfo($"[{nameof(GlobalGameInstaller)}] Installing Lobby context");

        }
    }
}