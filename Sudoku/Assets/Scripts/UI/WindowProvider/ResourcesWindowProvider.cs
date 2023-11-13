using PlayborschUI.Base;
using UnityEngine;

namespace PlayborschUI.WindowProvider
{
    public class ResourcesWindowProvider : IWindowProvider
    {
        public WindowView LoadWindow(string windowKey)
        {
            var prefab = Resources.Load<WindowView>(windowKey);
            return prefab;
        }
    }
}