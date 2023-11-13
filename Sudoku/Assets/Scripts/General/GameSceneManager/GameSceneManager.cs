using System.Threading.Tasks;
using General.SceneLoader;
using PlayborschUI.LoadingScreen;
using PlayborschUI.WindowsManager;
using Utilities;
using Utilities.Logger;
using Zenject;

namespace General.GameSceneManager
{
    public class GameSceneManager : IGameSceneManager
    {
        [Inject] private ISceneLoader _sceneLoader;
        [Inject] private IWindowsManager _windowsManager;

        private float _minimumLoadingSeconds = 3.0f;
        private float _minimumLoadingMilliseconds => _minimumLoadingSeconds * 1000;
        private float _fakePercents = 0.8f;
        public void LoadLobbyScene()
        {
            LoadScene("Lobby");
        }

        public void LoadGameScene()
        {
            LoadScene("Game");
        }
        
        private async void LoadScene(string sceneName)
        {
            _windowsManager.CloseAllWindows();
            var loadingWindow = _windowsManager.OpenWindow<LoadingWindowController>();
            
            await SimulateLoading(_sceneLoader.LoadScene(sceneName), loadingWindow);
            
            loadingWindow.Close();
        }
        private void UpdateLoadingView(float loadingProgress, LoadingWindowController loadingWindow)
        {
            loadingWindow.UpdateLoadingProgress(loadingProgress);
        }

        private async Task SimulateLoading(Task actualSceneLoading, LoadingWindowController loadingWindow) // TODO: This is temp, working on ideas
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var trueLoadingPart = 1-_fakePercents;
            float currentLoadingPercents = 0f;

            UpdateLoadingView(currentLoadingPercents,loadingWindow);
            
            while (actualSceneLoading.Status == TaskStatus.Running)
            {
                await Task.Delay(TemporaryProjectConstants.AwaitTimeForTask);
                currentLoadingPercents = (watch.ElapsedMilliseconds / _minimumLoadingMilliseconds);
                
                if (currentLoadingPercents > _fakePercents) currentLoadingPercents = _fakePercents;
                
                UpdateLoadingView(currentLoadingPercents,loadingWindow);
            }
            
            currentLoadingPercents += trueLoadingPart;
            UpdateLoadingView(currentLoadingPercents,loadingWindow);

            while (currentLoadingPercents < 1)
            {
                await Task.Delay(TemporaryProjectConstants.AwaitTimeForTask);
                currentLoadingPercents = (watch.ElapsedMilliseconds / _minimumLoadingMilliseconds) + trueLoadingPart;
                LogHandler.LogInfo("Loading "+currentLoadingPercents);
                
                UpdateLoadingView(currentLoadingPercents,loadingWindow);
                
            }
            
            watch.Stop();
        }
    }
}