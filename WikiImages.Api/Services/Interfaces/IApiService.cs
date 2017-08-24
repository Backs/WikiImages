using System.Collections.Generic;

namespace WikiImages.Api.Services.Interfaces
{
    public interface IApiService
    {
        IReadOnlyCollection<Page> GetPages(double latitude, double longitude);
        IReadOnlyList<string> GetImageTitles(IEnumerable<long> pageIds);
    }
}
