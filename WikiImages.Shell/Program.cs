using System;
using System.Collections.Generic;
using System.Linq;
using Unity;
using WikiImages.Algorithm;
using WikiImages.Api;
using WikiImages.Api.Services.Interfaces;
using WikiImages.Infrastructure;
using WikiImages.Infrastructure.Services.Interfaces;

namespace WikiImages.Shell
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var container = InitContainer())
            {
                var locationService = container.Resolve<IUserLocationService>();
                var location = locationService.GetCurrentLocation();
                Console.WriteLine("Current location: {0}:{1}", location.Latitude, location.Longitude);

                var apiService = container.Resolve<IApiService>();
                var pages = apiService.GetPages(location.Latitude, location.Longitude);
                var titles = apiService.GetImageTitles(pages.Select(o => o.PageId));

                titles = ClearTitles(titles);
                var groups = new SimilarityGroups(1, 0.6).Find(titles);

                for (var i = 0; i < groups.Count; i++)
                {
                    Console.WriteLine("Group {0}:", i + 1);
                    Console.WriteLine(string.Join("; ", groups[i]));
                }
            }
        }

        private static IReadOnlyList<string> ClearTitles(IEnumerable<string> titles)
        {
            return titles.Select(StringExtensions.ClearTitle).ToList().AsReadOnly();
        }

        private static IUnityContainer InitContainer()
        {
            var container = new UnityContainer();
            container.AddNewExtension<InfrastructureContainerExtension>();
            container.AddNewExtension<ApiContainerExtension>();
            return container;
        }
    }
}
