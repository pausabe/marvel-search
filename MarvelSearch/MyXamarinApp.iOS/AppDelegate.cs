using Foundation;
using MvvmCross.Platforms.Ios.Core;
using MarvelSearch.Core;

namespace MarvelSearch.iOS
{
    [Register(nameof(AppDelegate))]
    public class AppDelegate : MvxApplicationDelegate<Setup, App>
    {
    }
}
