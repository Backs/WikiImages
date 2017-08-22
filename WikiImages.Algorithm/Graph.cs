using System;
using System.Collections.Generic;
using System.Linq;

namespace WikiImages.Algorithm
{
    public sealed class Graph
    {
        private readonly List<Edge> _nodes = new List<Edge>();
        private readonly IDictionary<string, int> _keys;
        public Graph(IReadOnlyList<string> data, Func<string, string, double> distance)
        {
            _keys = data.Distinct().Select((o, i) => new { o, i }).ToDictionary(o => o.o, o => o.i);

            for (var i = 0; i < data.Count; i++)
            {
                var value1 = data[i];
                for (var j = i + 1; j < data.Count; j++)
                {
                    var value2 = data[j];
                    var key = distance(value1, value2);
                    _nodes.Add(new Edge(value1, value2, _keys[value1], _keys[value2], key));
                }
            }
        }

        public void MinimalGraph()
        {
            var dsu = new Dsu(_keys.Count);

            var list = new List<Edge>();

            foreach (var edge in _nodes.OrderByDescending(o => o.Distance))
            {
                var a = edge.Index1;
                var b = edge.Index2;
                if (dsu.Get(a) != dsu.Get(b))
                {
                    dsu.Unite(a, b);
                    list.Add(edge);
                }
            }
            list = list.Where(o => o.Distance > 0.5).ToList();
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
            private readonly string _value1;
            private readonly string _value2;

            public Edge(string value1, string value2, int index1, int index2, double distance)
            {
                _value1 = value1;
                _value2 = value2;
                Index1 = index1;
                Distance = distance;
                Index2 = index2;
            }

            public int Index1 { get; }
            public int Index2 { get; }
            public double Distance { get; }

            public override string ToString()
            {
                return $"{_value1}-{_value2}:{Distance}";
            }
        }
    }
}
