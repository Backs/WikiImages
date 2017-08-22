using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using WikiImages.Algorithm;
using WikiImages.Api.Services;
using WikiImages.Infrastructure;

namespace WikiImages.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public async Task TestMethod1()
        {
            var service = new ApiService();

            var data = string.Join("|", (await service.GetPages(55.023525, 82.941754)).Select(o => o.PageId));
        }

        [Test]
        public async Task TestMethod2()
        {
            var service = new ApiService();
            var pages = await service.GetPages(55.023525, 82.941754);
            var data = await service.GetImageTitles(pages.Select(o => o.PageId));

            var clearedData = data.Select(StringExtensions.ClearTitle).ToArray();

            Graph g = new Graph(clearedData, SentenceSimilarity.Distance);
            g.MinimalGraph();
        }

        [Test]
        public void TestMethod3()
        {
            Graph g = new Graph(new[] { "asd", "as", "dfg", "qwe" }, SentenceSimilarity.Distance);
            g.MinimalGraph();

        }
    }
}
