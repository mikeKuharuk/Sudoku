using GameCore;
using General.Game;
using PlayborschUI.Base;
using Zenject;

namespace PlayborschUI.GameHUD
{
    public class GameHudWindowModel : WindowModel
    {
        [Inject] public IPainter Painter;
        [Inject] public IGameEventsListener GameEvents;
    }
}