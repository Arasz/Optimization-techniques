using ConsoleApplication.Solver;
using System;
using System.Linq;
using System.Text;

namespace ConsoleApplication.Printer.ContentBuilders
{
    public class ConsoleContentBuilder : IContentBuilder
    {
        private readonly ISolver _solver;

        public ConsoleContentBuilder(ISolver solver)
        {
            _solver = solver;
        }

        public string BuildContent(string title)
        {
            var builder = new StringBuilder();
            builder.AppendLine("".PadRight(30, '*'));
            builder.AppendLine(title + $" Date: {DateTime.Now}");
            builder.AppendLine($"Min cost: {_solver.BestResult}, Mean cost: {_solver.MeanReasult}," +
                               $" Max cost: {_solver.WorstResult}");
            builder.AppendLine($"Elements in path: {_solver.BestPath.Count()}");
            builder.AppendLine("Path:");
            foreach (var node in _solver.BestPath)
                builder.Append($"{node}, ");
            builder.Remove(builder.Length - 2, 1);
            builder.AppendLine("".PadRight(30, '*'));
            return builder.ToString();
        }
    }
}