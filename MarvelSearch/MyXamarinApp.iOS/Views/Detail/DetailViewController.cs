using System;
using Cirrious.FluentLayouts.Touch;
using MarvelSearch.Core.ViewModels.Detail;
using MarvelSearch.iOS;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace MyXamarinApp.iOS.Views.Detail
{
    public class DetailViewController : MvxViewController<DetailViewModel>
    {
        private UILabel _title;
        private UITextView _description;
        private UIImageView _imageView;

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationItem.HidesBackButton = false;
            View.BackgroundColor = UIColor.White;

            _imageView = new UIImageView()
            {
                ContentMode = UIViewContentMode.ScaleAspectFit,
                BackgroundColor = UIColor.Black
            };
            Add(_imageView);

            _title = new UILabel()
            {
                TextAlignment = UITextAlignment.Center,
                Font = UIFont.BoldSystemFontOfSize(24)
            };
            Add(_title);

            _description = new UITextView()
            {
                TextAlignment = UITextAlignment.Left,
                Font = UIFont.SystemFontOfSize(16)
            };
            Add(_description);

            View.AddConstraints(
                _imageView.AtTopOf(View),
                _imageView.WithSameWidth(View),
                _imageView.Height().EqualTo(200),

                _title.Below(_imageView, 10),
                _title.AtLeftOf(View, 20),
                _title.AtRightOf(View, 20),

                _description.Below(_title, 10),
                _description.AtLeftOf(View, 20),
                _description.AtRightOf(View, 20),
                _description.AtBottomOf(View, 20)
            );

            var set = this.CreateBindingSet();
            set.Bind(_title).To(vm => vm.SelectedComic.Title);
            set.Bind(_description).To(vm => vm.SelectedComic.Description);
            set.Bind(_imageView).For(IOSKeys.SDWebImageTargetCustomBindingName).To(vm => vm.SelectedComic.Thumbnail.Url);
            set.Apply();
        }
    }
}
