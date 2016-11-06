using ConsoleApplication.Algorithms;
using ConsoleApplication.Graphs;
using ConsoleApplication.Solver.SolverVisitor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.Int32;

namespace ConsoleApplication.Solver
{
	public abstract class SolverBase : ISolver
	{
		protected readonly IGraph CompleteGraph ;

	    protected SolverStatistics Statistics { get; set;}

	    public ISolverStatistics SolvingStatistics => Statistics;


		protected SolverBase(IGraph completeGraph)
		{
		    CompleteGraph = completeGraph;
		    Statistics = new SolverStatistics();
		}

		public virtual IPathAccumulator Solve(IAlgorithm tspSolvingAlgorithm)
	    {
	        throw new NotImplementedException();
	    }

	    public virtual IPathAccumulator Solve(IAlgorithm tspSolvingAlgorithm, IPathAccumulator pathAccumulator)
	    {
	        throw new NotImplementedException();
	    }

	    public IPathAccumulator Solve(IAlgorithm tspSolvingAlgorithm, int startNode)
	    {
	        throw new NotImplementedException();
	    }

	    protected class SolverStatistics : ISolverStatistics
	    {
	        public Path BestPath { get; private set; }

	        public TimeSpan MaxSolvingTime { get; private set;}

	        public int MeanCost => (int) Math.Round(Costs.Average());

	        public TimeSpan MeanSolvingTime => TimeSpan.FromMilliseconds(SolvingTimes.Average(span => span.TotalMilliseconds));

	        public TimeSpan MinSolvingTime { get; private set; }

	        public int WorstCost { get; private set;}

	        private List<int> Costs = new List<int>();

	        private List<TimeSpan> SolvingTimes = new List<TimeSpan>();

	        public SolverStatistics()
	        {
	            BestPath = new Path(new List<int>(), new ConstCostCalculationStrategy(int.MaxValue));
	        }

	        public void UpdateSolvingResults(Path bestPath, TimeSpan solvingTime)
	        {
	            UpdatePathResults(bestPath);
	            UpdateTimeMeasures(solvingTime);

	        }

	        private void UpdatePathResults(Path path)
	        {
	            if (path.Cost < BestPath.Cost)
	                BestPath = path;

	            if (path.Cost > WorstCost)
	                WorstCost = path.Cost;

	            Costs.Add(path.Cost);
	        }

	        private void UpdateTimeMeasures(TimeSpan localTime)
	        {
	            if (localTime < MinSolvingTime)
	                MinSolvingTime = localTime;

	            if (localTime > MaxSolvingTime)
	                MaxSolvingTime = localTime;

	            SolvingTimes.Add(localTime);
	        }
	    }

		protected class SolvingTimeContext : IDisposable
		{
			private readonly Stopwatch _stopwatch;
			public TimeSpan Elapsed => _stopwatch.Elapsed;

			public static SolvingTimeContext Instance => new SolvingTimeContext(new Stopwatch());

			private SolvingTimeContext(Stopwatch stopwatch)
			{
				_stopwatch = stopwatch;
				_stopwatch.Start();
			}

			public void Dispose()
			{
				_stopwatch.Stop();
			}
		}
	}
}