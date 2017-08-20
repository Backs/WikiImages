using NUnit.Framework;
using WikiImages.Algorithm;

namespace WikiImages.Tests.Algorithm.LevenshteinTests
{
    [TestFixture]
    public class LevenshteinDistanceTests
    {
        [Test]
        public void EqualsTest()
        {
            var result = Levenshtein.Distance("abs", "abs");

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void IgnoreCaseTest()
        {
            var result = Levenshtein.Distance("abs", "ABS");

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void ReplaceTest()
        {
            var result = Levenshtein.Distance("abs", "abc");

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void AddTest()
        {
            var result = Levenshtein.Distance("abs", "ab");

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void SwapTest()
        {
            var result = Levenshtein.Distance("ab", "ba");

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void ComplexTest()
        {
            var result = Levenshtein.Distance("ca", "abc");

            //ca -> ac -> abc
            Assert.That(result, Is.EqualTo(3));
        }
    }
}
