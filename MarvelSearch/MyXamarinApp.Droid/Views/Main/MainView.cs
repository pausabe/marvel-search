using Android.OS;
using Android.Views;
using Android.Widget;
using MarvelSearch.Core.ViewModels.Main;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MyXamarinApp.Droid;

namespace MarvelSearch.Droid.Views.Main
{
    [MvxFragmentPresentation(typeof(MainContainerViewModel), Resource.Id.content_main_frame)]
    public class MainView : BaseFragment<MainViewModel>
    {
        protected override int FragmentLayoutId => Resource.Layout.fragment_main;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var searchView = view.FindViewById<SearchView>(Resource.Id.search_view);
            var btnSearch = view.FindViewById<Button>(Resource.Id.btn_search);

            var set = this.CreateBindingSet();
            set.Bind(searchView).For(v => v.Query).To(vm => vm.SearchText);
            set.Apply();

            return view;
        }
    }
}
