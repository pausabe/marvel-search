using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MarvelSearch.Core.Models;
using MarvelSearch.Core.Models.MarvelAPI;
using Newtonsoft.Json;

namespace MarvelSearch.Core.Services
{
    public class MarvelAPIService : IMarvelAPIService
    {

        private HttpClient _client;

        public MarvelAPIService(HttpClient client = null)
        {
            _client = client;
        }

        public async Task<List<Comic>> GetComics(string marvelTitle)
        {
            try
            {
                var rawResponse = await GetMarvelComicsByTitle(marvelTitle);
                return MapResponse(rawResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        private async Task<string> GetMarvelComicsByTitle(string marvelTitle)
        {
            if (_client == null)
            {
                _client = new HttpClient();
            }

            string baseUrl = "https://gateway.marvel.com/v1/public/comics";
            string timestamp = DateTime.Now.Ticks.ToString();
            string hash = CalculateHash(timestamp, MarvelAPIKeys.PrivateKey, MarvelAPIKeys.PublicKey);
            int limit = 20;
            string url = $"{baseUrl}?titleStartsWith={marvelTitle}&apikey={MarvelAPIKeys.PublicKey}&ts={timestamp}&hash={hash}&limit={limit}&orderBy=title";

            HttpResponseMessage response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            _client.Dispose();
            _client = null;
            return responseBody;
        }

        private string CalculateHash(string timestamp, string privateKey, string publicKey)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                var inputBytes = System.Text.Encoding.ASCII.GetBytes(timestamp + privateKey + publicKey);
                var hashBytes = md5.ComputeHash(inputBytes);

                var sb = new System.Text.StringBuilder();
                foreach (var b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        private List<Comic> MapResponse(string rawResponse)
        {
            ComicsResponse comicsResponse = JsonConvert.DeserializeObject<ComicsResponse>(rawResponse);
            return comicsResponse.Data.Results;
        }
    }
}
