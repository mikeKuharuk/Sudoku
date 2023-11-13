using PlayborschUI.Base;

namespace PlayborschUI.WindowsManager
{
    public interface IWindowsManager
    {
        TWindowController OpenWindow<TWindowController>(object customParams = null) where TWindowController : WindowController;
        void CloseWindow<TWindowController>() where TWindowController : WindowController;

        void CloseWindow(WindowController windowController);

        void CloseAllWindows();
    }
    
}