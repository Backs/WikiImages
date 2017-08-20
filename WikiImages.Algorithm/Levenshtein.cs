using System;

namespace WikiImages.Algorithm
{
    public static class Levenshtein
    {
        public static int Distance(string string1, string string2)
        {
            if (string1 == null)
                throw new ArgumentNullException(nameof(string1));
            if (string2 == null)
                throw new ArgumentNullException(nameof(string2));

            string1 = string1.ToLowerInvariant();
            string2 = string2.ToLowerInvariant();

            var m = new int[string1.Length + 1, string2.Length + 1];

            for (var i = 0; i <= string1.Length; i++)
            {
                m[i, 0] = i;
            }
            for (var j = 0; j <= string2.Length; j++)
            {
                m[0, j] = j;
            }

            for (var i = 1; i <= string1.Length; i++)
            {
                for (var j = 1; j <= string2.Length; j++)
                {
                    var diff = string1[i - 1] == string2[j - 1] ? 0 : 1;

                    m[i, j] = Math.Min(
                        Math.Min(m[i - 1, j] + 1,
                            m[i, j - 1] + 1),
                        m[i - 1, j - 1] + diff);
                }
            }
            return m[string1.Length, string2.Length];
        }

        public static bool AreSame(string string1, string string2, int maxDifference)
        {
            if (maxDifference < 0)
                throw new ArgumentOutOfRangeException(nameof(maxDifference));

            return Distance(string1, string2) <= maxDifference;
        }
    }
}
