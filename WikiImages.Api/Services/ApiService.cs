﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using WikiImages.Api.Services.Interfaces;

namespace WikiImages.Api.Services
{
    internal sealed class ApiService : IApiService, IDisposable
    {
        private static readonly Uri Host = new Uri(@"https://en.wikipedia.org");
        private static readonly Uri ApiUri = new Uri(Host, @"w/api.php");

        private readonly WebClient _client;

        public ApiService()
        {
            _client = new WebClient();
        }

        public async Task<IReadOnlyCollection<Page>> GetPages(double latitude, double longitude)
        {
            var query = HttpUtility.ParseQueryString("action=query&list=geosearch&gsradius=10000&gslimit=50&format=json");
            query["gscoord"] = $"{latitude.ToString("0.000000", CultureInfo.InvariantCulture)}|{longitude.ToString("0.000000", CultureInfo.InvariantCulture)}";

            var requestUri = new UriBuilder(ApiUri) { Query = query.ToString() };

            var json = await _client.DownloadStringTaskAsync(requestUri.Uri);

            var result = JsonConvert.DeserializeObject<PagesQueryRoot>(json);

            return Array.AsReadOnly(result.Query.Geosearch);
        }

        public async Task<IReadOnlyCollection<string>> GetImageTitles(IEnumerable<long> pageIds)
        {
            var query = HttpUtility.ParseQueryString("action=query&prop=images&imlimit=500&format=json");
            query["pageids"] = string.Join("|", pageIds);

            var requestUri = new UriBuilder(ApiUri) { Query = query.ToString() };

            var json = await _client.DownloadStringTaskAsync(requestUri.Uri);

            var result = JsonConvert.DeserializeObject<ImagesRootObject>(json);

            return Array.AsReadOnly(result.Query.Pages.Values.SelectMany(o => o.Images).Select(o => o.Title).ToArray());
        }

        public void Dispose()
        {
            _client?.Dispose();
        }

        private class PagesQueryRoot
        {
            public PagesQuery Query { get; set; }
        }

        private class PagesQuery
        {
            public Page[] Geosearch { get; set; }
        }

        private class Image
        {
            public string Title { get; set; }
        }

        private class ImagesQuery
        {
            public Dictionary<long, ImagesPage> Pages { get; set; }
        }

        private class ImagesRootObject
        {
            public ImagesQuery Query { get; set; }
        }

        private class ImagesPage
        {
            public List<Image> Images { get; set; }
        }
    }
}
