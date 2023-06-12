using System.Collections.Generic;
using MarvelSearch.Core.Models;
using MarvelSearch.Core.Services;
using MarvelSearch.Core.ViewModels.Main;
using Moq;
using NUnit.Framework;
using MarvelSearch.Core;

namespace MarvelSearch.Tests.ViewModels
{
    public class MainViewModelTests
    {
        Mock<IMarvelAPIService> _marvelAPIServiceMock;
        MainViewModel _mainViewModel;

        [SetUp]
        public void Setup()
        {
            _marvelAPIServiceMock = new Mock<IMarvelAPIService>();
            _mainViewModel = new MainViewModel(null, _marvelAPIServiceMock.Object);
        }

        [Test]
        public void SearchForComicsAndListThemCorrectly_EmptyList()
        {
            var comicsResponse = new List<Comic>();
            _marvelAPIServiceMock
                .Setup(x => x.GetComics(It.IsAny<string>()))
                .ReturnsAsync(comicsResponse);

            _mainViewModel.SearchCommand.Execute();

            Assert.AreEqual(_mainViewModel.ComicsCollection.Count, 0);
            Assert.AreEqual(_mainViewModel.EmptyListText, LanguageKeys.EmptyComics);
        }

        [Test]
        public void SearchForComicsAndListThemCorrectly_FullList()
        {
            var comicsResponse = new List<Comic>()
            {
                new Comic(),
                new Comic()
            };
            _marvelAPIServiceMock
                .Setup(x => x.GetComics(It.IsAny<string>()))
                .ReturnsAsync(comicsResponse);

            _mainViewModel.SearchCommand.Execute();

            Assert.AreEqual(_mainViewModel.ComicsCollection.Count, 2);
            Assert.AreEqual(_mainViewModel.EmptyListText, string.Empty);
        }

        [Test]
        public void SearchForComicsAndListThemCorrectly_WhenAPIServiceError()
        {
            _marvelAPIServiceMock
                .Setup(x => x.GetComics(It.IsAny<string>()))
                .ReturnsAsync((List<Comic>)null);

            _mainViewModel.SearchCommand.Execute();

            Assert.AreEqual(_mainViewModel.ComicsCollection.Count, 0);
            Assert.AreEqual(_mainViewModel.EmptyListText, LanguageKeys.ErrorGettingComics);
        }
    }
}
