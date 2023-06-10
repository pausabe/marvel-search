using Cirrious.FluentLayouts.Touch;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MarvelSearch.Core.ViewModels.Main;
using UIKit;
using CoreGraphics;

namespace MarvelSearch.iOS.Views.Main
{
    [MvxRootPresentation(WrapInNavigationController = true)]
    public class MainViewController : BaseViewController<MainViewModel>
    {
        private UILabel _labelWelcome, _labelMessage;
        private UIButton _buttonSearch;

        protected override void CreateView()
        {
            _labelWelcome = new UILabel
            {
                Text = "Welcome!! test",
                TextAlignment = UITextAlignment.Center
            };
            Add(_labelWelcome);

            _labelMessage = new UILabel
            {
                Text = "App marvel",
                TextAlignment = UITextAlignment.Center
            };
            Add(_labelMessage);

            _buttonSearch = new UIButton
            {
                Frame = new CGRect(25, 25, 300, 150),
                BackgroundColor = UIColor.Red
            };
            _buttonSearch.SetTitle("Serach", UIControlState.Normal);
            Add(_buttonSearch);
        }

        protected override void LayoutView()
        {
            View.AddConstraints(new FluentLayout[]
           {
                _labelWelcome.WithSameCenterX(View),
                _labelWelcome.WithSameCenterY(View),

                _labelMessage.Below(_labelWelcome, 10f),
                _labelMessage.WithSameWidth(View),

                _buttonSearch.Below(_labelMessage, 10f),
                _buttonSearch.WithSameWidth(View)
           });
        }

        protected override void BindView()
        {
            var set = this.CreateBindingSet();
            set.Bind(_labelWelcome).To(vm => vm.Test);
            set.Bind(_buttonSearch).To(nameof(ViewModel.SearchCommand));
            set.Apply();
        }
    }
}
