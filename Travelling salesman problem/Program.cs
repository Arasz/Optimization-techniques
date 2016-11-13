using ConsoleApplication.Algorithms.LocalSearch;
using ConsoleApplication.Algorithms;
using ConsoleApplication.Configuration;
using ConsoleApplication.Graphs;
using ConsoleApplication.Printer;
using ConsoleApplication.Printer.ContentBuilders;
using ConsoleApplication.Solver;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text;
using Autofac;
using ConsoleApplication.Similarity;

namespace ConsoleApplication
{
    public class Program
    {
        private const int Steps = 50;

        private static IContainer _dependencyContainer;


        private static void InitializeDependencies()
        {
            var containerBuilder = new ContainerBuilder();

            _dependencyContainer = containerBuilder.Build();
        }

        public static void Main(string[] args)
        {
            var buildConfig = BuildConfiguration();
            var config = buildConfig.GetSection(nameof(AppConfiguration));

            var dataPath = config["data"];
            var resultPath = config["results"];
            var graphEdges = System.IO.Path.Combine(dataPath, config["graphEdges"]);
            var graphCoordinates = System.IO.Path.Combine(dataPath, config["graphCoordinates"]);


            var dataLoader = new GraphLoader(graphEdges, 100);
            var graph = dataLoader.Load();

            var calculationStrategies = new ISimilarityCalculationStrategy[]
                {new EdgeSimillarityStrategy(), new NodeSimilarityStrategy()};

            var similaritySolver = new PathSimilaritySolver(graph,
                new InitializationSolver(new TspSolver(graph), new RandomPathAlgorithm(Steps, new EdgeFinder())),
                calculationStrategies);

            similaritySolver.Solve(new LocalSearchAlgorithm(Steps, new EdgeFinder()));



            foreach (var similairityValue in similaritySolver.SimilairityValues)
            {
                var resultString = new StringBuilder();
                var title = similairityValue.Key;
                var filePrinter = new FilePrinter(resultPath, $"{title.Replace(' ', '_').Replace('|', '_')}_results.res");


                resultString.AppendLine($"{nameof(SimilaritySolverResult.Cost)} {nameof(SimilaritySolverResult.SimilarityValue)}");

                foreach (var similaritySolverResult in similairityValue.Value)
                    resultString.AppendLine($"{similaritySolverResult.Cost} {similaritySolverResult.SimilarityValue:F}");

                resultString.AppendLine();
                filePrinter.Print(resultString.ToString());
            }

            Console.WriteLine("SUCCES!!!");
        }

        private static void RunBasicAlgorithms(TspSolver solver, string coordinatesPath, string resultsPath)
        {
            var resultPrinter = new ResultPrinter()
                .AddPrinter(new ConsolePrinter(), new ConsoleContentBuilder(solver.SolvingStatistics))
                .AddPrinter(new FilePrinter(resultsPath, $"{DateTime.Now.Date.ToFileTime()}_results.txt"),
                    new FileContentBuilder(solver.SolvingStatistics, coordinatesPath));


            SolveAndPrint(solver, new NearestNeighborAlgorithm(Steps, new EdgeFinder()), "NEAREST NEIGHBOR", resultPrinter);

            SolveAndPrint(solver, new GreedyCycleAlgorithm(Steps, new EdgeFinder()), "GREEDY CYCLE", resultPrinter);

            SolveAndPrint(solver, new NearestNeighborAlgorithm(Steps, new GraspEdgeFinder(3)), "NEAREST NEIGHBOR GRASP", resultPrinter);

            SolveAndPrint(solver, new GreedyCycleAlgorithm(Steps, new GraspEdgeFinder(3)), "GREEDY CYCLE GRASP", resultPrinter);
        }

        private static void RunAlgorithmsWithLocalSearch(IGraph graph, ISolver solver, string coordinatesPath, string resultsPath)
        {
            var edgeFinder = new EdgeFinder();
            var graspEdgeFiner = new GraspEdgeFinder(3);

            var localSearchSolver = new TspLocalSearchSolver(graph);

            var localSearchALgorithm = new LocalSearchAlgorithm(Steps, edgeFinder);

            var generatedPaths = solver.Solve(new NearestNeighborAlgorithm(Steps, edgeFinder));
            localSearchSolver.Solve(localSearchALgorithm, generatedPaths);
            LocalSearchResultPrinter(localSearchSolver, "NN_LS", coordinatesPath, resultsPath).Print("NN with local search opt");

            generatedPaths = solver.Solve(new GreedyCycleAlgorithm(Steps, edgeFinder));
            localSearchSolver.Solve(localSearchALgorithm, generatedPaths);
            LocalSearchResultPrinter(localSearchSolver, "GC_LS", coordinatesPath, resultsPath).Print("GC with local search opt");

            generatedPaths = solver.Solve(new NearestNeighborAlgorithm(Steps, graspEdgeFiner));
            localSearchSolver.Solve(localSearchALgorithm, generatedPaths);
            LocalSearchResultPrinter(localSearchSolver, "NNG_LS", coordinatesPath, resultsPath).Print("NN Grasp with local search opt");

            generatedPaths = solver.Solve(new GreedyCycleAlgorithm(Steps, graspEdgeFiner));
            localSearchSolver.Solve(localSearchALgorithm, generatedPaths);
            LocalSearchResultPrinter(localSearchSolver, "GCG_LS", coordinatesPath, resultsPath).Print("GC GRASP with local search opt");

            generatedPaths = solver.Solve(new RandomPathAlgorithm(Steps, edgeFinder));
            localSearchSolver.Solve(localSearchALgorithm, generatedPaths);
            LocalSearchResultPrinter(localSearchSolver, "Random_LS", coordinatesPath, resultsPath).Print("RANDOM with local search opt");
        }

        private static void RunMSLSAlgorithms(IGraph graph, ISolver solver, string coordinatesPath, string resultsPath)
        {
            var localSearchSolver = new TspMultipleStartLocalSearchSolver(graph, solver, new NearestNeighborAlgorithm(Steps, new GraspEdgeFinder(3)));
            var localSearchAlgorithm = new LocalSearchAlgorithm(Steps, new EdgeFinder());
            SolveAndPrint(localSearchSolver, localSearchAlgorithm,
                "NN Grasp with local search (MSLS)", LocalSearchResultPrinter(localSearchSolver, "NNG_MSLS", coordinatesPath, resultsPath));

            localSearchSolver = new TspMultipleStartLocalSearchSolver(graph, solver, new GreedyCycleAlgorithm(Steps, new GraspEdgeFinder(3)));
            localSearchAlgorithm = new LocalSearchAlgorithm(Steps, new EdgeFinder());
            SolveAndPrint(localSearchSolver, localSearchAlgorithm,
                "GC Grasp with local search (MSLS)", LocalSearchResultPrinter(localSearchSolver, "GCG_MSLS", coordinatesPath, resultsPath));

            localSearchSolver = new TspMultipleStartLocalSearchSolver(graph, solver, new RandomPathAlgorithm(Steps, new EdgeFinder()));
            localSearchAlgorithm = new LocalSearchAlgorithm(Steps, new EdgeFinder());
            SolveAndPrint(localSearchSolver, localSearchAlgorithm,
                "Random with local search (MSLS)", LocalSearchResultPrinter(localSearchSolver, "Random_MSLS", coordinatesPath, resultsPath));
        }

        private static void RunILSAlgorithm(IGraph graph, TspSolver solver, string coordinatesPath, string resultsPath)
        {
            var localSearchSolver = new TspIteratedLocalSearchSolver(graph, solver, new NearestNeighborAlgorithm(Steps, new GraspEdgeFinder(3)));
            var localSearchAlgorithm = new LocalSearchAlgorithm(Steps, new EdgeFinder());
            SolveAndPrint(localSearchSolver, localSearchAlgorithm,
                "NN Grasp with local search (ILS)", LocalSearchResultPrinter(localSearchSolver, "NNG_ILS", coordinatesPath, resultsPath));

            localSearchSolver = new TspIteratedLocalSearchSolver(graph, solver, new GreedyCycleAlgorithm(Steps, new GraspEdgeFinder(3)));
            localSearchAlgorithm = new LocalSearchAlgorithm(Steps, new EdgeFinder());
            SolveAndPrint(localSearchSolver, localSearchAlgorithm,
                "GC Grasp with local search (ILS)", LocalSearchResultPrinter(localSearchSolver, "GCG_ILS", coordinatesPath, resultsPath));

            localSearchSolver = new TspIteratedLocalSearchSolver(graph, solver, new RandomPathAlgorithm(Steps, new EdgeFinder()));
            localSearchAlgorithm = new LocalSearchAlgorithm(Steps, new EdgeFinder());
            SolveAndPrint(localSearchSolver, localSearchAlgorithm,
                "Random with local search (ILS)", LocalSearchResultPrinter(localSearchSolver, "Random_ILS", coordinatesPath, resultsPath));
        }

        private static void RunRandomLSPathsStatistics(IGraph graph, TspSolver solver, string coordinatesPath)
        {
            var localSearchSolver = new PathSimilaritySolver(graph, new InitializationSolver(solver,
                new RandomPathAlgorithm(Steps, new EdgeFinder())),
                new ISimilarityCalculationStrategy[] { new EdgeSimillarityStrategy(), new NodeSimilarityStrategy() });
            var localSearchAlgorithm = new LocalSearchAlgorithm(Steps, new EdgeFinder());
            localSearchSolver.Solve(localSearchAlgorithm);
        }

        private static IResultPrinter LocalSearchResultPrinter(ISolver localSearchSolver,
            string algorithmName, string coordinatesPath, string resultsPath)
        {
            return new ResultPrinter()
                    .AddPrinter(new ConsolePrinter(), new ConsoleContentBuilder(localSearchSolver.SolvingStatistics))
                    .AddPrinter(new FilePrinter(resultsPath, $"{algorithmName}_{DateTime.Now.Date.ToFileTime()}_results.txt"),
                    new FileContentBuilder(localSearchSolver.SolvingStatistics, coordinatesPath));
        }

        private static IConfigurationRoot BuildConfiguration() => new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("config.json")
            .Build();

        private static void SolveAndPrint(ISolver solver, IAlgorithm algorithm, string title, IResultPrinter resultPrinter)
        {
            solver.Solve(algorithm);
            resultPrinter.Print(title);
        }
    }
}