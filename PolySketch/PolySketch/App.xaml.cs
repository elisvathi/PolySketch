using Ninject;
using Poly.NinjectModules.Kernels;
using PolySketch.UI.MainUI;
using Xamarin.Forms;

namespace PolySketch
{
    public partial class App : Application
    {
        private KernelService Service { get; set; }

        // Testers
        private IKernel global { get { return Service.GlobalKernel; } }

        public App( KernelService serv )
        {
            InitializeComponent();

            this.Service = serv;
            MainPage = global.Get<MainCanvasPage>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}