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
            var dataLoader = new GraphLoader(dataPath, 100);
            var graph = dataLoader.Load();
            var solver = new TspSolver(graph);
            EdgeFinder RegularFinder = new EdgeFinder();
            GraspEdgeFinder GraspEdgeFinder = new GraspEdgeFinder(3);
            SolveAndPrint(solver, new NearestNeighborAlgorithm(Steps, RegularFinder), "NEAREST NEIGHBOR");

            SolveAndPrint(solver, new GreedyCycleAlgorithm(Steps, RegularFinder), "GREEDY CYCLE");

            SolveAndPrint(solver, new NearestNeighborAlgorithm(Steps, GraspEdgeFinder), "NEAREST NEIGHBOR GRASP");

            Console.ReadKey();
        }

        private static IConfigurationRoot BuildConfiguration() => new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("config.json")
            .Build();

        private static void SolveAndPrint(ISolver solver, IAlgorithm algorithm, string title)
        {
            solver.Solve(algorithm);
            Console.WriteLine(title);
            Console.WriteLine($"Min cost {solver.BestResult}, max cost {solver.WorstResult}, mean cost {solver.MeanReasult}");
            Console.WriteLine(
                $"Best path ({solver.BestPath.Count()} elements): {solver.BestPath.Select(i => i.ToString()).Aggregate("", (accu, str) => accu += $"{str}, ")}");
            Console.WriteLine();
        }
    }
}