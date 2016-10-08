using ConsoleApplication.Algorithms;
using ConsoleApplication.Configuration;
using ConsoleApplication.Graphs;
using ConsoleApplication.Solver;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

namespace ConsoleApplication
{
    public class Program
    {
        private const int Steps = 50;

        public static void Main(string[] args)
        {
            var dataPath = BuildConfiguration()[$"{nameof(AppConfiguration)}:{nameof(AppConfiguration.GraphDataPath)}"];

            Console.WriteLine("Hello World!");
            var dataLoader = new GraphLoader(dataPath, 100);
            var graph = dataLoader.Load();
            var solver = new TspSolver(graph);
            solver.Solve(new NearestNeighborAlgorithm(Steps));
            Console.WriteLine("NEAREST NEIGHBOUR");
            Console.WriteLine($"Min cost {solver.BestResult}, max cost {solver.WorstResult}, mean cost {solver.MeanReasult}");
            Console.WriteLine($"Best path ({solver.BestPath.Count()} elements): {solver.BestPath.Select(i => i.ToString()).Aggregate("", (accu, str) => accu += $"{str}, ")}");
            Console.WriteLine();

            solver.Solve(new GreedyCycleAlgorithm(Steps));
            Console.WriteLine("GREEDY CYCLE");
            Console.WriteLine($"Min cost {solver.BestResult}, max cost {solver.WorstResult}, mean cost {solver.MeanReasult}");
            Console.WriteLine($"Best path ({solver.BestPath.Count()} elements): {solver.BestPath.Select(i => i.ToString()).Aggregate("", (accu, str) => accu += $"{str}, ")}");
            Console.WriteLine();
            Console.ReadKey();
        }

        private static IConfigurationRoot BuildConfiguration() => new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("config.json")
            .Build();
    }
}