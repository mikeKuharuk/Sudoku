using PlayborschUI.Base;
using Utilities.Logger;

namespace PlayborschUI.LoadingScreen
{
    public class LoadingWindowController : WindowController<LoadingWindowView, LoadingWindowModel>
    {
        protected override void OnInit()
        {
            View.ProgressBar.value = 0;
        }

        public void UpdateLoadingProgress(float loadingProgress)
        {
            var percentsLabel = GetPercents(loadingProgress);
            
            View.ProgressBar.value = loadingProgress;
            View.ProgressLabel.text = percentsLabel;
        }

        private string GetPercents(float loadingProgress)
        {
            return "%";
        }

        protected override void OnDispose()
        {
        }
    }
}