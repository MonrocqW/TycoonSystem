namespace TycoonSystem.Core
{
    public interface IUpgradeable
    {
        public int CurrentLevel { get; }
        public float BaseUpgradeCost { get; }
        public AbstractCurrency UpgradeCurrency { get; }

        public void UpgradeBy(int p_levels);
    }
}