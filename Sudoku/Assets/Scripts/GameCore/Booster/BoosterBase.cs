namespace GameCore.Booster
{
    public abstract class BoosterBase
    {
        public abstract BoosterType Type { get; }

        public abstract void ApplyBooster();
    }
}