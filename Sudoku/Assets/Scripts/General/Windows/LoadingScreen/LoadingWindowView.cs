using PlayborschUI.Base;
using PlayborschUI.WindowsManager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlayborschUI.LoadingScreen
{
    public class LoadingWindowView : WindowView<LoadingWindowModel>
    {
        [SerializeField] public TMP_Text JokesLabel;
        [SerializeField] public TMP_Text ProgressLabel;
        [SerializeField] public Slider ProgressBar;
        public override CanvasLayers Layer => CanvasLayers.Overlay;
        protected override void InitView()
        {
            
        }
        protected override void OnDestroying()
        {
            
        }
    }
}