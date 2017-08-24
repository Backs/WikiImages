using System;
using System.Collections.Generic;
using System.Linq;

namespace WikiImages.Algorithm
{
    public static class SentenceSimilarity
    {
        private static readonly char[] Separator = { ' ', ',', '.', ':', ';', '!', '?', '\n', '\r', '\t', '(', ')', '-' };
        public static double Distance(string sentence1, string sentence2, int maxDifference)
        {
            if (maxDifference < 0)
                throw new ArgumentOutOfRangeException(nameof(maxDifference), maxDifference, null);
            if (sentence1 == null)
                throw new ArgumentNullException(nameof(sentence1));
            if (sentence2 == null)
                throw new ArgumentNullException(nameof(sentence2));

            var words1 = sentence1.Split(Separator, StringSplitOptions.RemoveEmptyEntries);
            var words2 = sentence2.Split(Separator, StringSplitOptions.RemoveEmptyEntries);

            var unitedSet = new HashSet<string>(new Comparer(maxDifference));

            unitedSet.UnionWith(words1);
            unitedSet.UnionWith(words2);

            var totalWordsCount = unitedSet.Count;

            var wordsCount1 = words1.Distinct().Count(word => unitedSet.Contains(word));
            var wordsCount2 = words2.Distinct().Count(word => unitedSet.Contains(word));

            var w1 = (double)wordsCount1 / totalWordsCount;
            var w2 = (double)wordsCount2 / totalWordsCount;

            return w1 * w2;
        }

        private sealed class Comparer : IEqualityComparer<string>
        {
            private readonly int _maxDifference;

            public Comparer(int maxDifference)
            {
                _maxDifference = maxDifference;
            }

            public bool Equals(string x, string y)
            {
                return Levenshtein.AreSame(x, y, _maxDifference);
            }
            public int GetHashCode(string obj)
            {
                return 0;
            }
        }
    }
}
