using Cirrious.FluentLayouts.Touch;
using MarvelSearch.Core.ViewModels.Detail;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace MyXamarinApp.iOS.Views.Detail
{
    [MvxRootPresentation(WrapInNavigationController = true)]
    public class DetailViewController : MvxViewController<DetailViewModel>
    {
        private UILabel _title;

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.White;

            NavigationController.NavigationBar.Hidden = false;

            _title = new UILabel()
            {
                TextAlignment = UITextAlignment.Center
            };
            Add(_title);

            View.AddConstraints(new FluentLayout[]
            {
                _title.WithSameCenterX(View),
                _title.WithSameCenterY(View)
            });

            var set = this.CreateBindingSet();
            set.Bind(_title).To(vm => vm.SelectedComic.Title);
            set.Apply();
        }
    }
}
