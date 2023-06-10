using Microsoft.Extensions.Logging;
using MvvmCross.Platforms.Android.Core;
using MarvelSearch.Core;
using Serilog;
using Serilog.Extensions.Logging;

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
    }
}
