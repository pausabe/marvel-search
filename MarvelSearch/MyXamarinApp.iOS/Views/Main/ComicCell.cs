using System;
using Cirrious.FluentLayouts.Touch;
using Foundation;
using MarvelSearch.Core.Models;
using MarvelSearch.iOS;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using UIKit;

namespace MyXamarinApp.iOS.Views.Main
{
    public class ComicCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString(nameof(ComicCell));

        public UILabel _title { get; private set; }
        public UILabel _subtitle { get; private set; }
        public UIImageView _imageView { get; private set; }

        public ComicCell(IntPtr handle) : base(handle)
        {
            SetupViews();
            SetupConstraints();
            BindData();
        }

        private void SetupViews()
        {
            _title = new UILabel()
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Font = UIFont.SystemFontOfSize(16)
            };
            ContentView.AddSubview(_title);

            _subtitle = new UILabel()
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Font = UIFont.SystemFontOfSize(12)
            };
            ContentView.AddSubview(_subtitle);

            _imageView = new UIImageView()
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                ContentMode = UIViewContentMode.ScaleAspectFill
            };
            _imageView.Layer.MasksToBounds = true;
            _imageView.Layer.CornerRadius = 20;
            ContentView.AddSubview(_imageView);
        }

        private void SetupConstraints()
        {
            var imageSize = 40;
            var leftPadding = 5;
            ContentView.AddConstraints(
                _imageView.AtLeadingOf(ContentView, leftPadding),
                _imageView.Width().EqualTo(imageSize),
                _imageView.Height().EqualTo(imageSize),

                _title.AtLeadingOf(ContentView, imageSize + leftPadding + leftPadding),

                _subtitle.AtLeadingOf(ContentView, imageSize + leftPadding + leftPadding),
                _subtitle.Below(_title)
            );
        }

        private void BindData()
        {
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<ComicCell, Comic>();
                set.Bind(_title).To(vm => vm.Title);
                set.Bind(_subtitle).To(vm => vm.Modified);
                set.Bind(_imageView).For(IOSKeys.SDWebImageTargetCustomBindingName).To(vm => vm.Thumbnail.Url);
                set.Apply();
            });
        }
    }
}
