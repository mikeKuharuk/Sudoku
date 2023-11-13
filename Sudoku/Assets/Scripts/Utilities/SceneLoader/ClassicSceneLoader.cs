using System;
using System.Threading.Tasks;
using General.SceneLoader;
using UnityEngine.SceneManagement;
using Utilities.Logger;

namespace Utilities.SceneLoader
{
    public class ClassicSceneLoader : ISceneLoader
    {
        private Scene _currentScene;

        public ClassicSceneLoader()
        {
            _currentScene = SceneManager.GetActiveScene();
        }

        public async Task LoadScene(string sceneName, IProgress<float> progressTracker = null)
        {
            var sceneLoadingProcess = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            
            LogHandler.LogInfo($"[{nameof(ClassicSceneLoader)}] Loading scene {sceneName}");
            
            while (!sceneLoadingProcess.isDone)
            {
                progressTracker?.Report(sceneLoadingProcess.progress / 2 ); // TODO: Reconsider progress reporting, this is hardcode
                await Task.Delay(TemporaryProjectConstants.AwaitTimeForTask);
            }
            var sceneUnloading = SceneManager.UnloadSceneAsync(_currentScene);     
            
            while (sceneUnloading.isDone)
            {
                progressTracker?.Report(0.5f + sceneUnloading.progress / 2 ); // TODO: Reconsider progress reporting, this is hardcode
                await Task.Delay(TemporaryProjectConstants.AwaitTimeForTask);
            }
            
            _currentScene = SceneManager.GetActiveScene();
        }
    }
}