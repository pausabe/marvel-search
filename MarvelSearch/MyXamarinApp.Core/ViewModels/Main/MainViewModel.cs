using System;
using MarvelSearch.Core.Services;
using MvvmCross.Commands;
using System.Linq;
using System.Threading.Tasks;

namespace MarvelSearch.Core.ViewModels.Main
{
    public class MainViewModel : BaseViewModel
    {

        IMarvelAPIService _marvelAPIService;

        public MainViewModel(IMarvelAPIService marvelAPIService)
        {
            _marvelAPIService = marvelAPIService;
        }

        private string _test = "binded!";
        public string Test
        {
            get => _test;
            set { SetProperty(ref _test, value); }
        }

        public IMvxCommand SearchCommand => new MvxAsyncCommand(SearchHandlerAsync);

        private async Task SearchHandlerAsync()
        {
            try
            {
                var marveElements = await _marvelAPIService.GetComics("capta");
                // TODO: collection utils nullOrEmpty
                if(marveElements != null && marveElements.Count > 0)
                {
                    Test = marveElements.FirstOrDefault().Title;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
