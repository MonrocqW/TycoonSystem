namespace TycoonSystem.Core
{
    public interface IGenerator
    {
        public float GeneratingPerProc { get; }
        public float GeneratingTime { get; }
        public float GeneratingPerSecond { get; }
        public AbstractCurrency GeneratingCurrency { get; }
    }
}