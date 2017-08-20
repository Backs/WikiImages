using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using WikiImages.Algorithm;
using WikiImages.Api.Services;

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

            for (var i = 0; i < data.Count; i++)
            {
                for (int j = i + 1; j < data.Count; j++)
                {
                    var dif = SentenceSimilarity.Distance(data[i], data[j]);
                }
            }
        }
    }
}
