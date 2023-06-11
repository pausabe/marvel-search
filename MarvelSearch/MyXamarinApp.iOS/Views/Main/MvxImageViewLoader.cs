using System;

namespace MyXamarinApp.iOS.Views.Main
{
    internal class MvxImageViewLoader
    {
        private Func<object> p;

        public MvxImageViewLoader(Func<object> p)
        {
            this.p = p;
        }
    }
}