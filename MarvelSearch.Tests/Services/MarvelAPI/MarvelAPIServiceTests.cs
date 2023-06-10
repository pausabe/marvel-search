using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using MarvelSearch.Core.Models;
using MarvelSearch.Core.Services;
using NUnit.Framework;
using Moq;
using Moq.Protected;
using System.Threading;
using System;

namespace MarvelSearch.Tests
{
    public class MarvelAPIServiceTests
    {
        IMarvelAPIService _marvelAPIService;
        Mock<HttpMessageHandler> _httpMessageHandlerMock;

        [SetUp]
        public void Setup()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            _marvelAPIService = new MarvelAPIService(new HttpClient(_httpMessageHandlerMock.Object));
        }

        [Test]
        public async Task GetComics_ShouldMakeHttpRequestAndDeserializeResponseAsync()
        {
            var responseContent = "{\"code\": 200, \"status\": \"Ok\", \"data\": {\"offset\": 0, \"limit\": 20, \"total\": 100, \"count\": 2, \"results\": [{\"id\": 1, \"title\": \"Comic 1\", \"description\": \"Description 1\"}, {\"id\": 2, \"title\": \"Comic 2\", \"description\": \"Description 2\"}]}}";
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseContent)
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var comics = await _marvelAPIService.GetComics("");
            Assert.AreEqual(comics.Count, 2);
        }

        [Test]
        public async Task GetComics_ShouldHandleHttpRequestException()
        {
             _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new HttpRequestException("An error occurred while making the API request."));

            var comics = await _marvelAPIService.GetComics("");

            Assert.AreEqual(comics.Count, 0);
        }

        [Test]
        public void CalculateHash_ShouldReturnCorrectHash()
        {
            string timestamp = "1234567890";
            string publicKey = "public123";
            string privateKey = "private123";
            string expectedHash = "d666fa7d229ce8f430e4a0abd669ea7a";

            MethodInfo methodInfo = typeof(MarvelAPIService)
                .GetMethod("CalculateHash", BindingFlags.NonPublic | BindingFlags.Instance);
            object[] parameters = { timestamp, privateKey, publicKey };
            object hash = methodInfo.Invoke(_marvelAPIService, parameters);

            Assert.AreEqual(expectedHash, hash);
        }

        [Test]
        public void MapResponse_ShouldDeserializeCorrectly()
        {
            string json = "{\"code\": 200, \"status\": \"Ok\", \"data\": {\"offset\": 0, \"limit\": 20, \"total\": 100, \"count\": 2, \"results\": [{\"id\": 1, \"title\": \"Comic 1\", \"description\": \"Description 1\"}, {\"id\": 2, \"title\": \"Comic 2\", \"description\": \"Description 2\"}]}}";

            MethodInfo methodInfo = typeof(MarvelAPIService)
                .GetMethod("MapResponse", BindingFlags.NonPublic | BindingFlags.Instance);
            object[] parameters = { json };
            List<Comic> comics = (List<Comic>)methodInfo.Invoke(_marvelAPIService, parameters);

            Assert.AreEqual(2, comics.Count);
            Assert.AreEqual("Comic 1", comics[0].Title);
            Assert.AreEqual("Description 1", comics[0].Description);
            Assert.AreEqual("Comic 2", comics[1].Title);
            Assert.AreEqual("Description 2", comics[1].Description);
        }

        [Test]
        public void MapResponse_WhenIncorrectDeserializationWeGetEmptyList()
        {
            string incorrectJson = "{ incorrect JSON }";
            string correctJsonWithIncorrectCode = "{\"code\": 500, \"status\": \"Ok\", \"data\": {\"offset\": 0, \"limit\": 20, \"total\": 100, \"count\": 2, \"results\": [{\"id\": 1, \"title\": \"Comic 1\", \"description\": \"Description 1\"}, {\"id\": 2, \"title\": \"Comic 2\", \"description\": \"Description 2\"}]}}";
            
            MethodInfo methodInfo = typeof(MarvelAPIService)
                .GetMethod("MapResponse", BindingFlags.NonPublic | BindingFlags.Instance);

            object[] parameters = { incorrectJson };
            List<Comic> comics = (List<Comic>)methodInfo.Invoke(_marvelAPIService, parameters);

            Assert.AreEqual(0, comics.Count);

            parameters = new object[]{ correctJsonWithIncorrectCode };
            comics = (List<Comic>)methodInfo.Invoke(_marvelAPIService, parameters);

            Assert.AreEqual(0, comics.Count);
        }
    }
}
