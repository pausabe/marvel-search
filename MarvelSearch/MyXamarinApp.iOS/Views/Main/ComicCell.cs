using System;
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

        public UILabel _label { get; private set; }
        public UIImageView _imageView { get; private set; }

        public ComicCell(IntPtr handle) : base(handle)
        {
            SetupViews();
            SetupConstraints();
            BindData();
        }

        private void SetupViews()
        {
            _label = new UILabel()
            {
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            ContentView.AddSubview(_label);

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
            _imageView.LeadingAnchor.ConstraintEqualTo(LeadingAnchor).Active = true;
            _imageView.TopAnchor.ConstraintEqualTo(TopAnchor).Active = true;
            _imageView.BottomAnchor.ConstraintEqualTo(BottomAnchor).Active = true;
            _imageView.WidthAnchor.ConstraintEqualTo(_imageView.HeightAnchor).Active = true;
            _label.LeadingAnchor.ConstraintEqualTo(_imageView.TrailingAnchor, constant: 8).Active = true;
            _label.TrailingAnchor.ConstraintEqualTo(TrailingAnchor).Active = true;
            _label.CenterYAnchor.ConstraintEqualTo(CenterYAnchor).Active = true;
        }

        private void BindData()
        {
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<ComicCell, Comic>();
                set.Bind(_label).To(vm => vm.Title);
                set.Bind(_imageView).For(IOSKeys.SDWebImageTargetCustomBindingName).To(vm => vm.Thumbnail.Url);
                set.Apply();
            });
        }
    }
}
