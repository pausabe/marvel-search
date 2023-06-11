using Android.App;
using Android.Views;
using MarvelSearch.Core.ViewModels.Main;
using MyXamarinApp.Droid;

namespace MarvelSearch.Droid.Views.Main
{
    [Activity(
        Theme = "@style/AppTheme",
        WindowSoftInputMode = SoftInput.AdjustResize | SoftInput.StateHidden)]
    public class MainContainerActivity : BaseActivity<MainContainerViewModel>
    {
        protected override int ActivityLayoutId => Resource.Layout.activity_detail_container;
    }
}
