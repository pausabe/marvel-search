using System;
using MarvelSearch.Core.Services;
using MvvmCross.Commands;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MarvelSearch.Core.Models;
using System.Collections.Generic;

namespace MarvelSearch.Core.ViewModels.Main
{
    public class MainViewModel : BaseViewModel
    {

        IMarvelAPIService _marvelAPIService;

        public MainViewModel(IMarvelAPIService marvelAPIService)
        {
            _marvelAPIService = marvelAPIService;
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set {
                SetProperty(ref _searchText, value);

                // TODO:Throttle

                /* Observable.FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                s => PropertyChanged += s,
                    s => PropertyChanged -= s)
                .Select(evnt => evnt.EventArgs.PropertyName)
                .Where(evnt => evnt == nameof(SearchTerm) && AdvancedSearch)
                .Throttle(TimeSpan.FromMilliseconds(ThrottleMilliseconds))
                .ObserveOn(await MainThread.UISyncContext())
                .Do(_ =>
                {
                    RunningAdvancedSearch = true;
                    Collection.Clear();
                })
                .Select(_ => Items.Where(Filter))
                .Subscribe(items =>
                {
                    Collection.AddRange(items);
                    RunningAdvancedSearch = false;
                });*/
                _ = SearchHandlerAsync();
            }
        }

        private List<string> _items;

        public List<string> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        private ObservableCollection<Comic> _comicsCollection = new ObservableCollection<Comic>();
        public ObservableCollection<Comic> ComicsCollection
        {
            get => _comicsCollection;
            set => SetProperty(ref _comicsCollection, value);
        }

        public IMvxCommand SearchCommand => new MvxAsyncCommand(SearchHandlerAsync);

        public IMvxCommand OpenDetailCommand => new MvxAsyncCommand(OpenDetailHandlerAsync);

        private Task OpenDetailHandlerAsync()
        {
            throw new NotImplementedException();
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
