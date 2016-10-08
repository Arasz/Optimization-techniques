using System.Collections.Generic;

namespace ConsoleApplication.Graphs
{
    public interface IGraphIterator
    {
        int CurrentNode { get; }

        IList<Edge> Edges { get; }

        void MoveAlongEdge(Edge edge);

        void MoveTo(int node);
    }
}