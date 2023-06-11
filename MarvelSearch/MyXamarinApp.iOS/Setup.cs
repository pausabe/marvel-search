using Microsoft.Extensions.Logging;
using MvvmCross.Platforms.Ios.Core;
using MarvelSearch.Core;
using Serilog;
using Serilog.Extensions.Logging;
using MvvmCross.Binding.Bindings.Target.Construction;
using UIKit;
using MyXamarinApp.iOS.TargetBindings;

namespace MarvelSearch.iOS
{
    public class Setup : MvxIosSetup<App>
    {
        protected override ILoggerProvider CreateLogProvider() => new SerilogLoggerProvider();

        protected override ILoggerFactory CreateLogFactory()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.NSLog()
                .CreateLogger();

            return new SerilogLoggerFactory();
        }

        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            base.FillTargetFactories(registry);

            registry.RegisterCustomBindingFactory<UIImageView>(IOSKeys.SDWebImageTargetCustomBindingName, view => new SDWebImageTargetBinding(view));
        }
    }
}
