using System;
using System.Globalization;
using Foundation;
using MarvelSearch.Core.Models;
using MvvmCross.Binding;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Bindings.Target;
using MvvmCross.Converters;
using MvvmCross.Platforms.Ios.Binding.Views;
using SDWebImage;
using UIKit;

namespace MyXamarinApp.iOS.Views.Main
{
    public class ComicCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString(nameof(ComicCell));

        public UILabel Label { get; private set; }
        public UIImageView ImageView { get; private set; }

        public ComicCell(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        private void Initialize()
        {
            Label = new UILabel()
            {
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            ContentView.AddSubview(Label);

            var imageSize = 15;
            ImageView = new UIImageView()
            {
                Frame = new CoreGraphics.CGRect(imageSize, imageSize, imageSize, imageSize),
                BackgroundColor = UIColor.LightGray
            };
            ContentView.AddSubview(ImageView);

            NSLayoutConstraint.ActivateConstraints(new[]
            {
                Label.TopAnchor.ConstraintEqualTo(ContentView.TopAnchor, 8),
                Label.LeadingAnchor.ConstraintEqualTo(ContentView.LeadingAnchor, 16),
                Label.TrailingAnchor.ConstraintEqualTo(ContentView.TrailingAnchor, -16),
                Label.BottomAnchor.ConstraintEqualTo(ContentView.BottomAnchor, -8),
                ImageView.TopAnchor.ConstraintEqualTo(Label.TopAnchor, 10)
            });

            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<ComicCell, Comic>();
                set.Bind(Label).To(vm => vm.Title);
                set.Bind(ImageView).For("ImageUrl").To(vm => vm.Thumbnail.Url);
                set.Apply();
            });
        }
    }

    // TODO: move to another file
    public class SDWebImageTargetBinding : MvxTargetBinding
    {
        protected UIImageView ImageView => Target as UIImageView;

        public override Type TargetType => typeof(string);

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;

        public SDWebImageTargetBinding(object target)
            : base(target)
        {
        }

        public override void SetValue(object value)
        {
            if (ImageView == null) return;

            if (value is string stringValue)
            {
                ImageView.SetImage(
                    url: new NSUrl(stringValue));
            }
        }
    }
}
