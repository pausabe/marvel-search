using System;
using Foundation;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using SDWebImage;
using UIKit;

namespace MyXamarinApp.iOS.TargetBindings
{
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
