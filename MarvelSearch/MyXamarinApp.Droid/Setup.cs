using Microsoft.Extensions.Logging;
using MvvmCross.Platforms.Android.Core;
using MarvelSearch.Core;
using Serilog;
using Serilog.Extensions.Logging;
using MvvmCross.Converters;
using MyXamarinApp.Droid;
using MyXamarinApp.Droid.Converters;

namespace MarvelSearch.Droid
{
    public class Setup : MvxAndroidSetup<App>
    {
        protected override ILoggerProvider CreateLogProvider() => new SerilogLoggerProvider();

        protected override ILoggerFactory CreateLogFactory()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.AndroidLog()
                .CreateLogger();

            return new SerilogLoggerFactory();
        }

        protected override void FillValueConverters(IMvxValueConverterRegistry registry)
        {
            base.FillValueConverters(registry);
            registry.AddOrOverwrite(AndroidKeys.StringToImageConverterName, new StringToImageConverter());
            registry.AddOrOverwrite(AndroidKeys.TextToVisibilityConverterName, new TextToVisibilityConverter());
        }
    }
}
