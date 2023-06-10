using MvvmCross.IoC;
using MvvmCross.ViewModels;
using MarvelSearch.Core.ViewModels.Main;

namespace MarvelSearch.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<MainViewModel>();
        }
    }
}
