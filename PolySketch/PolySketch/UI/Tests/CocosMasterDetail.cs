using Ninject;
using Poly.NinjectModules.Kernels;
using PolySketch.UI.Tests.MenuTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace PolySketch.UI.Tests
{
    public class CocosMasterDetail : MasterDetailPage
    {
        public CocosMasterDetail( KernelService Service )
        {
            Master = Service.GlobalKernel.Get<MenuPage>();
            Detail = new NavigationPage(Service.GlobalKernel.Get<TestCanvasPage>());
            //}
        }
    }
}