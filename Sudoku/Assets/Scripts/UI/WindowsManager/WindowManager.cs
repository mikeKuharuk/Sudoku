using System;
using System.Collections.Generic;
using PlayborschUI.Base;
using PlayborschUI.WindowProvider;
using UnityEngine;
using Utilities.Logger;
using Zenject;

namespace PlayborschUI.WindowsManager
{
    public class WindowManager : MonoBehaviour, IWindowsManager
    {
        [SerializeField] private MainCanvas _canvasPrefab;
        [Inject] private IWindowProvider _windowProvider;
        [Inject] private DiContainer _container;
        
        private MainCanvas _canvasRoot;
        private List<WindowController> _openedWindows;

        public void Awake()
        {
          _openedWindows = new List<WindowController>();
          _canvasRoot = Instantiate(_canvasPrefab);
          DontDestroyOnLoad(_canvasRoot);
          _container.Inject(_canvasRoot);
          _canvasRoot.Init();
        }

        public TWindowController OpenWindow<TWindowController>(object customParams = null) where TWindowController : WindowController
        {
            LogHandler.LogInfo($"[{nameof(WindowManager)}] Opening window with next type: {typeof(TWindowController)}");
            
            foreach (var openedWindow in _openedWindows)
            {
                if (openedWindow.GetType() == typeof(TWindowController))
                {
                    return (TWindowController)openedWindow;
                }
            }

            var windowController = Activator.CreateInstance<TWindowController>();
            var windowModel = Activator.CreateInstance(windowController.GetModelType()) as WindowModel;

            var viewPrefab = _windowProvider.LoadWindow(windowController.GetViewResourceKey());

            if (viewPrefab == null)
            {
                LogHandler.LogWarning($"[{nameof(WindowManager)} Can`t find prefab for window type: {typeof(TWindowController)}]");
                return null;
            }

            var targetRoot = _canvasRoot.GetRootForLayer(viewPrefab.Layer);
            var windowView = Instantiate(viewPrefab, targetRoot);
            
            _container.Inject(windowView);
            _container.Inject(windowController);
            _container.Inject(windowModel);
            
            windowView.Init(windowModel);
            windowController.Init(windowView, windowModel, customParams);
            
            _openedWindows.Add(windowController);
            
            return windowController;
        }

        public void CloseWindow<TWindowController>() where TWindowController : WindowController
        {
            LogHandler.LogInfo($"[{nameof(WindowManager)}] Closing window of type {typeof(TWindowController).Name}");
            var windowController = _openedWindows.Find(window => window.GetType() == typeof(TWindowController));
            
            if (windowController != null)
            {
                windowController.Dispose();
                _openedWindows.Remove(windowController);
            }
        }

        public void CloseWindow(WindowController windowController)
        {
            var targetController = _openedWindows.Find(controller => controller == windowController);
            
            if (targetController != null)
            {
                targetController.Dispose();
                _openedWindows.Remove(targetController);
            }
        }

        public void CloseAllWindows()
        {
            foreach (var window in _openedWindows)
            {
                window.Dispose();
            }
            
            _openedWindows.Clear();
        }
    }
    
}