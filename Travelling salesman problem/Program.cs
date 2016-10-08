using ConsoleApplication.Algorithms;
using ConsoleApplication.Configuration;
using ConsoleApplication.Graphs;
using ConsoleApplication.Printer;
using ConsoleApplication.Solver;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApplication
{
    public class Program
    {
        private const int Steps = 50;

        public static void Main(string[] args)
        {
            var dataPath = BuildConfiguration()[$"{nameof(AppConfiguration)}:{nameof(AppConfiguration.GraphDataPath)}"];
            var dataLoader = new GraphLoader(dataPath, 100);
            var graph = dataLoader.Load();
            var solver = new TspSolver(graph);
            var resultPrinter = new ResultPrinter()
                .AddPrinter(new ConsolePrinter())
                .AddPrinter(new FilePrinter($"{DateTime.Now.Date.ToFileTime()}_results.txt"));

            SolveAndPrint(solver, new NearestNeighborAlgorithm(Steps), "NEAREST NEIGHBOR", resultPrinter, BuildContent);

            SolveAndPrint(solver, new GreedyCycleAlgorithm(Steps), "GREEDY CYCLE", resultPrinter, BuildContent);

            SolveAndPrint(solver, new NearestNeighbourGraspAlgorithm(Steps, 3), nameof(NearestNeighbourGraspAlgorithm), resultPrinter, BuildContent);

            Console.ReadKey();
        }

        private static IConfigurationRoot BuildConfiguration() => new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("config.json")
            .Build();

        private static string BuildContent(ISolver solver, string title)
        {
            var builder = new StringBuilder();
            builder.AppendLine("".PadRight(10, '*'));
            builder.AppendLine(title + $" Date: {DateTime.Now}");
            builder.AppendLine($"Min cost: {solver.BestResult}, Mean cost: {solver.MeanReasult}," +
                               $" Max cost: {solver.WorstResult}");
            builder.AppendLine($"Elements in path: {solver.BestPath.Count()}");
            builder.AppendLine("Path:");
            foreach (var node in solver.BestPath)
                builder.AppendLine(node.ToString());
            builder.AppendLine("".PadRight(10, '*'));
            return builder.ToString();
        }

        private static void SolveAndPrint(ISolver solver, IAlgorithm algorithm, string title, IResultPrinter resultPrinter, Func<ISolver, string, string> contentBuilder)
        {
            solver.Solve(algorithm);
            resultPrinter.Print(contentBuilder(solver, title));
        }
    }
}