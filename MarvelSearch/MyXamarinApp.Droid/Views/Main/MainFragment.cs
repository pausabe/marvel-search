using Android.OS;
using Android.Views;
using Android.Widget;
using MarvelSearch.Core.ViewModels.Main;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MyXamarinApp.Droid;

namespace MarvelSearch.Droid.Views.Main
{
    [MvxFragmentPresentation(typeof(MainContainerViewModel), Resource.Id.content_frame)]
    public class MainFragment : BaseFragment<MainViewModel>
    {
        protected override int FragmentLayoutId => Resource.Layout.fragment_main;

        private Button _btnSearch;
        private TextView _btnTextView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            _btnSearch = view.FindViewById<Button>(Resource.Id.btn_search);
            _btnTextView = view.FindViewById<TextView>(Resource.Id.txt_welcome);

            var set = this.CreateBindingSet();
            set.Bind(_btnTextView).To(vm => vm.Test);
            set.Bind(_btnSearch).To(nameof(ViewModel.SearchCommand));
            set.Apply();

            return view;
        }
    }
}
