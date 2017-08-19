using System.Collections.Generic;
using System.Threading.Tasks;

namespace WikiImages.Api.Services.Interfaces
{
    public interface IApiService
    {
        Task<IReadOnlyCollection<Page>> GetPages(double latitude, double longitude);
        Task<IReadOnlyCollection<string>> GetImageTitles(IEnumerable<long> pageIds);
    }
}
