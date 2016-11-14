using ConsoleApplication.Solver.Statistics;
using System;
using System.Text;

namespace ConsoleApplication.Printer.ContentBuilders
{
    public class ConsoleContentBuilder : IContentBuilder
    {
        private readonly ISolverStatistics _solverStatistics;

        public ConsoleContentBuilder(ISolverStatistics solverStatistics)
        {
            _solverStatistics = solverStatistics;
        }

        public string BuildContent(string title)
        {
            var builder = new StringBuilder();
            builder.AppendLine("".PadRight(30, '*'));
            builder.AppendLine(title + $"\tDate: {DateTime.Now}");
            builder.AppendLine($"Min solving time: {_solverStatistics.MinSolvingTime.TotalMilliseconds} ms, " +
                               $"Mean solving time: {_solverStatistics.MeanSolvingTime.TotalMilliseconds} ms, " +
                               $"Max solving time: {_solverStatistics.MaxSolvingTime.TotalMilliseconds} ms , ");
            builder.AppendLine($"Min cost: {_solverStatistics.BestPath.Cost}, Mean cost: {_solverStatistics.MeanCost}," +
                               $" Max cost: {_solverStatistics.WorstCost}");
            builder.AppendLine($"Elements in path: {_solverStatistics.BestPath.Count}");
            builder.AppendLine("Path:");
            foreach (var node in _solverStatistics.BestPath.Nodes)
                builder.Append($"{node}, ");
            builder.Remove(builder.Length - 2, 1);
            builder.AppendLine();
            builder.AppendLine("".PadRight(30, '*'));
            return builder.ToString();
        }
    }
}