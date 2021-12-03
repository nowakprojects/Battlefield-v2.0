using Battlefield.Core.Domain;

namespace Battlefield.Core.PathFinder;
public class PathFinder
{
    public Node[,] NodeMap { get; private set; }
    public class Path
    {
        public bool IsReachable { get; private set; }
        private IEnumerable<Node> Nodes { get; set; }

        public Path(bool reachable, IEnumerable<Node> nodes)
        {
            IsReachable = reachable;
            Nodes = nodes;
        }

        public Coordinates GetNextStep()
        {
            return Nodes.First().Coords;
        }
    }
    public class Node
    {
        public Coordinates Coords { get; protected set; }
        public float Cost { get; protected set; }
        public bool Blocekd { get; set; }
        public Node? Previous { get; set; }
        public Node(int x, int y)
        {
            Coords = new Coordinates(x, y);
        }
    }
    public PathFinder(Battle battle)
    {
        NodeMap = new Node[battle.Width, battle.Height];
        for (int y = 0; y < battle.Height; y++)
        {
            for (int x = 0; x < battle.Width; x++)
            {
                var tile = battle.TileMap[y, x];
                NodeMap[x, y] = new Node(x, y);
                NodeMap[x, y].Blocekd = tile.Blocked;
            }
        }
    }
    public Path GetPathFromTo(Coordinates from, Coordinates to)
    {
        if (from.Equals(to)) return new Path(true, new List<Node>());
        CalculatePaths(from);
        var nodes = new List<Node>();
        var node = NodeMap[to.X, to.Y];
        while (node.Previous is not null)
        {
            nodes.Add(node);
            node = node.Previous;
        }
        return new Path(nodes.Count > 0, nodes);
    }
    public void CalculatePaths(Coordinates from)
    {

    }
}
