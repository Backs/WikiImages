using NUnit.Framework;
using WikiImages.Algorithm;

namespace WikiImages.Tests.Algorithm.SentenceSimilarityTests
{
    [TestFixture]
    public class DistanceTests
    {
        [Test]
        public void Test()
        {
            var s1 = "i like dogs";
            var s2 = "i dont like dog";
            var result = SentenceSimilarity.Distance(s1, s2);
            Assert.That(result, Is.EqualTo(0.75).Within(0.001));
        }
    }
}
