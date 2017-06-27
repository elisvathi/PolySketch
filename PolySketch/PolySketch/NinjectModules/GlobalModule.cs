using Ninject.Modules;
using Poly.NinjectModules.Kernels;
using PolySketch;
using PolySketch.UI.MainUI;
using PolySketch.UI.Tests;
using PolySketch.UI.Tests.MenuTests;

namespace Poly.NinjectModules
{
    public class GlobalModule : NinjectModule
    {
        public override void Load()
        {
            Bind<KernelService>().ToSelf().InSingletonScope();
            Bind<App>().ToSelf().InSingletonScope();

            //Xamarin Forms Pages
            //Tests
            Bind<MenuPage>().ToSelf().InSingletonScope();
            Bind<MenuListData>().ToSelf().InSingletonScope();
            Bind<MenuListView>().ToSelf().InSingletonScope();
            Bind<CocosMasterDetail>().ToSelf().InSingletonScope();

            //Final Implementation
            Bind<MainCanvasPage>().ToSelf().InSingletonScope();
            Bind<CanvasWrapperPage>().ToSelf().InSingletonScope();

            //Temporary TEsts
            Bind<AnimationTest>().ToSelf().InSingletonScope();
            Bind<TestClassCounter>().ToSelf().InSingletonScope();
            Bind<TestCanvasPage>().ToSelf().InSingletonScope();
        }
    }
}