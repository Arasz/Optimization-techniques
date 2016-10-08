using ConsoleApplication.Graphs;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication.Algorithms
{
    public class GreedyCycleAlgorithm : IAlgorithm
    {
        private int _steps {get;}

        public GreedyCycleAlgorithm(int steps){
            _steps = steps;
        }
        public int Solve(int startNode, Graph graph, out IList<int> path)
        {
            path = new List<int>();
            if(_steps<1){
                return 0;
            }
            graph.CurrentNode = startNode;
            var pathCost = 0;

            path.Add(startNode);
            var unvisitedNodes = graph.Nodes.Where(node => node != startNode).ToList();
            var steps = _steps;

            //find nearest to start point
            var nearestUnvisited = graph.NearestNode(unvisitedNodes);
            pathCost += graph.Cost(startNode, nearestUnvisited);
            pathCost += graph.Cost(nearestUnvisited, startNode);

            graph.CurrentNode = nearestUnvisited;
            unvisitedNodes.Remove(nearestUnvisited);

            path.Add(nearestUnvisited);
            path.Add(startNode);
            steps--;

            while (--steps > 0)
            {
                int bestConnection1 = -1;
                int bestBefore = -1;
                int newCost1 = -1;
                for(int node = 0 ; node < (path.Count-2) ; node++)
                {
                    int firstNode = path[node];
                    int secondNode = path[node+1];
                    int newCost = -1;
                    int bestConnection = -1;
                    foreach(int unvisitedNode in unvisitedNodes){
                        int cost = graph.Cost(firstNode, unvisitedNode) + graph.Cost(unvisitedNode, secondNode);
                        if(newCost < 0 || cost < newCost){
                            newCost = cost;
                            bestConnection = unvisitedNode;
                        }
                    }
                    if(newCost1 < 0 || newCost < newCost1){
                        bestConnection1 = bestConnection;
                        bestBefore = node+1;
                        newCost1 = newCost;
                    }
                }
                int before = path[bestBefore-1];
                int after = path[bestBefore];
                path.Insert(bestBefore, bestConnection1);
                int oldCost = graph.Cost(before, after);
                int newestCost = graph.Cost(before, bestConnection1) + graph.Cost(bestConnection1, after);
                pathCost -= oldCost;
                pathCost += newestCost;
                unvisitedNodes.Remove(bestConnection1);
            }
            return pathCost;
        }
    }
}