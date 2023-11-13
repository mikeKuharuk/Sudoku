using System;
using PlayborschUI.WindowsManager;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Logger;

namespace PlayborschUI.Base
{
    public abstract class WindowView : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        public event Action OnClose;
        public abstract CanvasLayers Layer { get; }

        protected WindowModel _baseModel;
        public void Init(WindowModel model)
        {
            LogHandler.LogInfo($"[{nameof(WindowView)}] Initializing window: {GetType()}");

            _baseModel = model;
            Subscribe();
            InitView();
        }

        protected abstract void InitView();
        protected abstract void OnDestroying();

        private void OnDestroy()
        {
            LogHandler.LogInfo($"[{nameof(WindowView)}] Destroying window: {GetType()}");
            Unsubscribe();
            OnDestroying();
        }

        private void Subscribe()
        {
            _closeButton?.onClick.AddListener(OnCloseButtonClick);
        }

        private void OnCloseButtonClick()
        {
            OnClose?.Invoke();
        }

        private void Unsubscribe()
        {
            _closeButton?.onClick.RemoveListener(OnCloseButtonClick);

        }
    }

    public abstract class WindowView<TModel> : WindowView where TModel : WindowModel
    {
        protected TModel Model => _baseModel as TModel;
    }
}