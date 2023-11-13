using PlayborschUI.Base;
using UnityEngine;

namespace PlayborschUI.WindowProvider
{
    public interface IWindowProvider
    {
        WindowView LoadWindow(string windowKey);
    }
}