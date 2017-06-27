using Android.App;
using Android.Content.PM;
using Android.OS;
using Ninject;
using Poly.NinjectModules;
using Poly.NinjectModules.Kernels;
using PolySketch.Drawing;
using PolySketch.UI.Canvas;

namespace PolySketch.Droid
{
    [Activity(Label = "PolySketch" , Icon = "@drawable/icon" , Theme = "@style/MainTheme" , MainLauncher = true , ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate( Bundle bundle )
        {
            IKernel kernel = new StandardKernel(new GlobalModule());
            var service = kernel.Get<KernelService>();
            service.GlobalKernel = kernel;
            service.AddFile += AddNewKernel;
            service.OnAddFile();
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this , bundle);
            LoadApplication(kernel.Get<App>());
        }

        private void AddNewKernel( KernelService service )
        {
            service.AddLocalKernel(new StandardKernel(new LocalModule(ref service)));
            var a = service.ActiveKernel.Get<CanvasCamera>();
            var b = service.ActiveKernel.Get<DrawingService>();
        }
    }
}