using General.GameSceneManager;
using UnityEngine;
using Zenject;

namespace General.Bootstrap
{
    public class SplashBootstrap : MonoBehaviour
    {
        [Inject] private IGameSceneManager _gameSceneManager;
        public void Start()
        {
            _gameSceneManager.LoadLobbyScene();
        }
    }
}