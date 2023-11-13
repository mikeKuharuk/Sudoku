using System;
using PlayborschUI.WindowsManager;
using UnityEngine;
using Utilities.Logger;
using Zenject;

namespace PlayborschUI.Base
{
    public abstract class WindowController : IDisposable
    {
        [Inject] protected IWindowsManager WindowsManager;
        public abstract string GetViewResourceKey();
        public abstract Type GetModelType();
        
        protected WindowView BaseView;
        protected WindowModel BaseModel;
        protected object Params;
        public void Init(WindowView view, WindowModel model, object customParams)
        {
            LogHandler.LogInfo($"[{nameof(WindowController)}] Init controller {GetType().Name}");
            BaseView = view;
            BaseModel = model;
            Params = customParams;
            
            BaseView.OnClose += OnCloseButton;
            OnInit();

        }
        
        /// <summary>
        /// Designed to be called from WindowManager, don`t call directly
        /// </summary>
        public void Dispose()
        {
            LogHandler.LogInfo($"[{nameof(WindowController)}] Dispose controller {GetType().Name}");
            BaseModel = null;
            GameObject.Destroy(BaseView.gameObject);
            BaseView.OnClose -= OnCloseButton;         
            
            OnDispose();
        }

        public void Close()
        {
            WindowsManager.CloseWindow(this);
        }

        protected abstract void OnInit();
        protected abstract void OnDispose();

        private void OnCloseButton()
        {
            WindowsManager.CloseWindow(this);
        }
    }

    public abstract class WindowController<TView, TModel> : WindowController
        where TModel : WindowModel where TView : WindowView
    {

        public TView View => BaseView as TView;
        public TModel Model => BaseModel as TModel;

        public override string GetViewResourceKey() => typeof(TView).Name;
        public override Type GetModelType() => typeof(TModel);
    }
}