using Foundation;
using Ninject;
using Poly.NinjectModules;
using Poly.NinjectModules.Kernels;
using UIKit;

namespace PolySketch.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            IKernel kernel = new StandardKernel(new GlobalModule());
            var service = kernel.Get<KernelService>();
            service.GlobalKernel = kernel;
            service.AddFile += AddNewKernel;

            LoadApplication(kernel.Get<App>());

            return base.FinishedLaunching(app, options);
        }

        private void AddNewKernel(KernelService service)
        {
            service.AddLocalKernel(new StandardKernel(new LocalModule(ref service)));
        }
    }
}