using System;
using MvvmCross.Commands;

namespace MarvelSearch.Core.ViewModels.Main
{
    public class MainViewModel : BaseViewModel
    {
        private string _test = "binded!";
        public string Test
        {
            get => _test;
            set { SetProperty(ref _test, value); }
        }

        public IMvxCommand SearchCommand => new MvxCommand(SearchHandler);

        private void SearchHandler()
        {
            Test = "Button pressed";
        }
    }
}
