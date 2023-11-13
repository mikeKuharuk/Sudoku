using System;
using System.Threading.Tasks;

namespace General.SceneLoader
{
    public interface ISceneLoader
    {
        Task LoadScene(string sceneName, IProgress<float> progressTracker = null);
    }
}