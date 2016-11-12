namespace ConsoleApplication.Graphs
{
    public interface ICostCalculationStrategy
    {
        /// <summary>
        /// Calculates path cost
        /// </summary>
        int Calculate(Path path);
    }
}