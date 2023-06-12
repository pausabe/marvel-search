﻿using MarvelSearch.Core.ViewModels.Main;
using UIKit;
using MvvmCross.Platforms.Ios.Binding.Views;
using MvvmCross.Binding.BindingContext;
using MyXamarinApp.iOS.Views.Main;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.Platforms.Ios.Presenters.Attributes;

namespace MarvelSearch.iOS.Views.Main
{
    [MvxRootPresentation(WrapInNavigationController = true)]
    public class MainViewController : MvxTableViewController<MainViewModel>
    {
        private UISearchBar _searchBar;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _searchBar = new UISearchBar()
            {
                Placeholder = IOSKeys.SearchBarPlaceholderText
            };
            NavigationItem.TitleView = _searchBar;
            NavigationItem.RightBarButtonItem = new UIBarButtonItem(IOSKeys.SearchButtonText, UIBarButtonItemStyle.Plain, null);

            var source = new MvxSimpleTableViewSource(TableView, typeof(ComicCell), ComicCell.Key);
            TableView.Source = source;

            var set = this.CreateBindingSet<MainViewController, MainViewModel>();
            set.Bind(_searchBar).For(v => v.Text).To(vm => vm.SearchText);
            set.Bind(source).To(vm => vm.ComicsCollection);
            set.Bind(source).For(s => s.SelectionChangedCommand).To(vm => vm.OpenDetailCommand);
            set.Bind(NavigationItem.RightBarButtonItem).To(vm => vm.SearchCommand);
            set.Apply();
        }
    }
}
