using System;
using System.Collections.Generic;
using System.Linq;

namespace WikiImages.Algorithm
{
    public sealed class Graph
    {
        private readonly List<Edge> _edges = new List<Edge>();
        private readonly IDictionary<string, int> _keys;
        private readonly IDictionary<int, string> _values;
        public Graph(IReadOnlyList<string> data, Func<string, string, double> distance)
        {
            _keys = data.Distinct().Select((o, i) => new { o, i }).ToDictionary(o => o.o, o => o.i);
            _values = _keys.ToDictionary(o => o.Value, o => o.Key);

            for (var i = 0; i < data.Count; i++)
            {
                var value1 = data[i];
                for (var j = i + 1; j < data.Count; j++)
                {
                    var value2 = data[j];
                    var key = distance(value1, value2);
                    _edges.Add(new Edge(_keys[value1], _keys[value2], key));
                }
            }
        }

        public IReadOnlyList<IReadOnlyCollection<string>> GetComponents(double minimalDistance)
        {
            var dsu = new Dsu(_keys.Count);

            var list = new List<Edge>();

            foreach (var edge in _edges.OrderByDescending(o => o.Distance))
            {
                var a = edge.Index1;
                var b = edge.Index2;
                if (dsu.Get(a) != dsu.Get(b))
                {
                    dsu.Unite(a, b);
                    list.Add(edge);
                }
            }
            list = list.Where(o => o.Distance >= minimalDistance).ToList();

            var result = new Dictionary<int, List<int>>();

            foreach (var edge in list)
            {
                if (!result.ContainsKey(edge.Index1))
                {
                    result[edge.Index1] = new List<int>();
                }
                if (!result.ContainsKey(edge.Index2))
                {
                    result[edge.Index2] = new List<int>();
                }

                result[edge.Index1].Add(edge.Index2);
                result[edge.Index2].Add(edge.Index1);
            }

            var groups = new Dfs(result).Find();

            return groups.Select(o => o.Select(s => _values[s]).ToList().AsReadOnly())
                .OrderByDescending(o => o.Count)
                .ToList()
                .AsReadOnly();
        }

        private class Dfs
        {
            private readonly Dictionary<int, List<int>> _nodes;
            private readonly HashSet<int> _visited = new HashSet<int>();

            public Dfs(Dictionary<int, List<int>> nodes)
            {
                _nodes = nodes;
            }

            public List<List<int>> Find()
            {
                var result = new List<List<int>>();
                foreach (var v in _nodes.Keys)
                {
                    if (!_visited.Contains(v))
                    {
                        var group = new List<int>();
                        Find(v, group);
                        result.Add(group);
                    }
                }
                return result;
            }

            private void Find(int node, ICollection<int> result)
            {
                if (_visited.Contains(node))
                    return;
                _visited.Add(node);

                result.Add(node);
                foreach (var v in _nodes[node])
                {
                    Find(v, result);
                }
            }
        }

        private class Dsu
        {
            private readonly Random _rand = new Random();
            private readonly int[] _set;
            public Dsu(int count)
            {
                _set = new int[count];
                for (var i = 0; i < count; i++)
                {
                    _set[i] = i;
                }
            }

            public int Get(int key)
            {
                return key == _set[key] ? key : (_set[key] = Get(_set[key]));
            }

            public void Unite(int a, int b)
            {
                a = Get(a);
                b = Get(b);
                if (_rand.Next(2) == 1)
                {
                    var t = a;
                    a = b;
                    b = t;
                }
                if (a != b)
                    _set[a] = b;
            }
        }

        private sealed class Edge
        {
            public Edge(int index1, int index2, double distance)
            {
                Index1 = index1;
                Distance = distance;
                Index2 = index2;
            }

            public int Index1 { get; }
            public int Index2 { get; }
            public double Distance { get; }
        }
    }
}
