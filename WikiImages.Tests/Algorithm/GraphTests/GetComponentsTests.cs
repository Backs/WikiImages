using NUnit.Framework;
using WikiImages.Algorithm;

namespace WikiImages.Tests.Algorithm.GraphTests
{
    [TestFixture]
    public class GetComponentsTests
    {
        [Test]
        public void OneComponentTest()
        {
            var graph = new Graph(new[] { "a", "b", "c" }, (a, b) => 1);
            var result = graph.GetComponents(0);

            CollectionAssert.AreEqual(new[] { new[] { "a", "b", "c" } }, result);
        }
    }
}
