using CocosSharp;
using Ninject;
using Poly.NinjectModules.Kernels;
using PolySketch.Layering;
using PolySketch.UI.Canvas.Interfaces;
using Xamarin.Forms;

namespace PolySketch.UI.MainUI
{
    internal class CanvasWrapperPage : ContentView
    {
        private KernelService Service;

        public CanvasWrapperPage( KernelService serv )
        {
            Service = serv;
            var view = Service.ActiveKernel.Get<IXamarinFormsView>();

            var layout = new StackLayout();
            var vv = view as CocosSharpView;
            vv.HorizontalOptions = LayoutOptions.FillAndExpand;
            vv.VerticalOptions = LayoutOptions.FillAndExpand;
            layout.Children.Add(vv);
            
            Content = layout;
            //TODO: Find an alternative way to initialize layer manager
            var lm = Service.ActiveKernel.Get<LayerManager>();
            lm.CreateLayer();
        }
    }
}