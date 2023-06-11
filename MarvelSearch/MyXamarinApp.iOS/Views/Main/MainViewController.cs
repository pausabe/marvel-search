using Cirrious.FluentLayouts.Touch;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MarvelSearch.Core.ViewModels.Main;
using UIKit;
using CoreGraphics;
using MvvmCross.Platforms.Ios.Binding.Views;
using Foundation;
using System.Collections.Generic;
using MvvmCross.Binding.BindingContext;
using MyXamarinApp.iOS.Views.Main;

namespace MarvelSearch.iOS.Views.Main
{
    [MvxRootPresentation(WrapInNavigationController = true)]
    public class MainViewController : BaseViewController<MainViewModel>
    {
        private UISearchBar _searchBar;
        private UITableView _comicList;

        protected override void CreateView()
        {
            _searchBar = new UISearchBar()
            {
                Placeholder = "Search"
            };
            Add(_searchBar);

            _comicList = new UITableView
            {
                AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth,
                BackgroundColor = UIColor.Blue
            };
            Add(_comicList);
        }

        protected override void LayoutView()
        {
            View.AddConstraints(new FluentLayout[] {
                _searchBar.WithSameWidth(View),
                _searchBar.Height().EqualTo(50),

                _comicList.Below(_searchBar, 10f),
                _comicList.WithSameWidth(View)
            });
        }

        public static readonly NSString CellIdentifier = new NSString("ObservationCell");

        protected override void BindView()
        {
            var source = new MvxSimpleTableViewSource(_comicList, typeof(ComicCell), ComicCell.Key);
            _comicList.Source = source;

            var set = this.CreateBindingSet<MainViewController, MainViewModel>();
            set.Bind(_searchBar).For(v => v.Text).To(vm => vm.SearchText);
            set.Bind(source).To(vm => vm.ComicsCollection);
            set.Apply();

            _comicList.ReloadData();
        }
    }
}
