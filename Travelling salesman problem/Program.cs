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
<<<<<<< HEAD
            var solver = new TspSolver(graph);
            var RegularFinder = new EdgeFinder();
            var GraspEdgeFinder = new GraspEdgeFinder(3);
            
            SolveAndPrint(solver, new NearestNeighborAlgorithm(Steps, RegularFinder), "NEAREST NEIGHBOR");
=======
            var solver = new TspSolver(graph);

            var resultPrinter = new ResultPrinter()
                .AddPrinter(new ConsolePrinter())
                .AddPrinter(new FilePrinter($"{DateTime.Now.Date.ToFileTime()}_results.txt"));

            SolveAndPrint(solver, new NearestNeighborAlgorithm(Steps, new EdgeFinder()), "NEAREST NEIGHBOR", resultPrinter, BuildContent);
>>>>>>> origin/master

            SolveAndPrint(solver, new GreedyCycleAlgorithm(Steps, new EdgeFinder()), "GREEDY CYCLE", resultPrinter, BuildContent);

            SolveAndPrint(solver, new NearestNeighborAlgorithm(Steps, new GraspEdgeFinder(3)), "NEAREST NEIGHBOR GRASP", resultPrinter, BuildContent);

            SolveAndPrint(solver, new GreedyCycleAlgorithm(Steps, new GraspEdgeFinder(3)), "GREEDY CYCLE GRASP", resultPrinter, BuildContent);

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