using System;
using System.Collections.Generic;

namespace WikiImages.Algorithm
{
    public sealed class SimilarityGroups
    {
        private readonly int _maxWordDifference;
        private readonly double _minimalDistance;

        public SimilarityGroups(int maxWordDifference, double minimalDistance)
        {
            if (maxWordDifference < 0)
                throw new ArgumentOutOfRangeException(nameof(maxWordDifference), maxWordDifference, "Value must be positive or zero");

            if (minimalDistance < 0.0 || minimalDistance > 1.0)
                throw new ArgumentOutOfRangeException(nameof(minimalDistance), minimalDistance, "Value must be from 0 to 1");

            _maxWordDifference = maxWordDifference;
            _minimalDistance = minimalDistance;
        }

        public IReadOnlyList<IReadOnlyCollection<string>> Find(IReadOnlyList<string> values)
        {
            var g = new Graph(values, (sentence1, sentence2) => SentenceSimilarity.Distance(sentence1, sentence2, _maxWordDifference));
            return g.GetComponents(_minimalDistance);
        }
    }
}
