using System;
using Android.OS;
using Android.Views;
using MarvelSearch.Core.ViewModels.Detail;
using MarvelSearch.Core.ViewModels.Main;
using MarvelSearch.Droid.Views;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace MyXamarinApp.Droid.Views.Detail
{
    [MvxFragmentPresentation(typeof(DetailContainerViewModel), Resource.Id.content_detail_frame)]
    public class DetailView : BaseFragment<DetailViewModel>
    {
        protected override int FragmentLayoutId => Resource.Layout.fragment_detail;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            return view;
        }
    }
}
