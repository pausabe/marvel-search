using System;
using System.Collections.Generic;
using Foundation;
using MarvelSearch.Core.Models;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace MyXamarinApp.iOS.Views.Main
{
    public class ComicCellViewController : MvxTableViewController
    {
        public static readonly NSString Key = new NSString("ComicCellViewController");

        private UILabel _title;

        public ComicCellViewController()
        {
            //var imageViewLoader = new MvxImageViewLoader(() => monkeyImage);

            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<ComicCellViewController, Comic>();
                //set.Bind(imageViewLoader).To(m => m.Thumbnail);
                set.Bind(_title).To(m => m.Title);
                set.Apply();
            });
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            TableView.RowHeight = 120f;

            _title = new UILabel()
            {
                BackgroundColor = UIColor.Red
            };
            TableView.Add(_title);

            TableView.ReloadData();
        }
    }
}
