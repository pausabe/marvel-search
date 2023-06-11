using System;
using Foundation;
using MarvelSearch.Core.Models;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using UIKit;

namespace MyXamarinApp.iOS.Views.Main
{
    public class ComicCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString(nameof(ComicCell));

        public UILabel Label { get; private set; }

        protected ComicCell(IntPtr handle) : base(handle)
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

            NSLayoutConstraint.ActivateConstraints(new[]
            {
                Label.TopAnchor.ConstraintEqualTo(ContentView.TopAnchor, 8),
                Label.LeadingAnchor.ConstraintEqualTo(ContentView.LeadingAnchor, 16),
                Label.TrailingAnchor.ConstraintEqualTo(ContentView.TrailingAnchor, -16),
                Label.BottomAnchor.ConstraintEqualTo(ContentView.BottomAnchor, -8)
            });

            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<ComicCell, Comic>();
                set.Bind(Label).To(vm => vm.Title);
                set.Apply();
            });
        }
    }
}
