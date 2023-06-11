﻿using System;
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

        private ObservableCollection<Comic> _comicsCollection = new ObservableCollection<Comic>();
        public ObservableCollection<Comic> ComicsCollection
        {
            get => _comicsCollection;
            set => SetProperty(ref _comicsCollection, value);
        }

        public IMvxCommand SearchCommand => new MvxAsyncCommand(SearchHandlerAsync);

        public IMvxCommand OpenDetailCommand => new MvxAsyncCommand(OpenDetailHandlerAsync);

        private async Task OpenDetailHandlerAsync()
        {
            // TODO: real selected
            var selectedComic = new Comic
            {
                Title = "testing navigation"
            };
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
