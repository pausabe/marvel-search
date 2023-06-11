using System;
using MarvelSearch.Core.Services;
using MvvmCross.Commands;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MarvelSearch.Core.Models;
using MvvmCross.Navigation;
using MarvelSearch.Core.ViewModels.Detail;

namespace MarvelSearch.Core.ViewModels.Main
{
    public class MainViewModel : BaseViewModel
    {

        private readonly IMvxNavigationService _navigationService;
        private readonly IMarvelAPIService _marvelAPIService;

        public MainViewModel(
            IMvxNavigationService navigationService,
            IMarvelAPIService marvelAPIService)
        {
            _navigationService = navigationService;
            _marvelAPIService = marvelAPIService;
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set {
                SetProperty(ref _searchText, value);
                // TODO: search command by button
                _ = SearchHandlerAsync();
            }
        }

        private ObservableCollection<Comic> _comicsCollection = new ObservableCollection<Comic>();
        public ObservableCollection<Comic> ComicsCollection
        {
            get => _comicsCollection;
            set => SetProperty(ref _comicsCollection, value);
        }

        public IMvxCommand SearchCommand => new MvxAsyncCommand(SearchHandlerAsync);
        public IMvxCommand OpenDetailCommand => new MvxAsyncCommand<Comic>(OpenDetailHandlerAsync);

        private async Task OpenDetailHandlerAsync(Comic selectedComic)
        {
            await _navigationService.Navigate<DetailViewModel, Comic>(selectedComic);
        }

        private async Task SearchHandlerAsync()
        {
            try
            {
                ComicsCollection.Clear();
                if (SearchText != "")
                {
                    var comics = await _marvelAPIService.GetComics(SearchText);
                    if (comics != null && comics.Count > 0)
                    {
                        comics.ForEach(comic => ComicsCollection.Add(comic));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
