using MarvelSearch.Core.Models;
using MarvelSearch.Core.ViewModels.Detail;
using NUnit.Framework;

namespace MarvelSearch.Tests.ViewModels
{
    public class DetailViewModelTests
    {
        DetailViewModel _detailViewModel;

        [SetUp]
        public void Setup()
        {
            _detailViewModel = new DetailViewModel();
        }

        private Comic GetHardcodedComic()
        {
            return new Comic
            {
                Title = "Test Title",
                Thumbnail = new Image
                {
                    Path = "http://i.annihil.us/u/prod/marvel/i/mg/1/30/5925b458255f8",
                    Extension = "jpg"
                },
                Description = "Test description"
            };
        }
    }
}
