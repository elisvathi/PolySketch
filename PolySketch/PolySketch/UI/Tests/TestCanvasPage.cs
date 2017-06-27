using CocosSharp;
using Ninject;
using Poly.NinjectModules.Kernels;
using PolySketch.Layering;
using PolySketch.UI.Canvas.Interfaces;
using System;

using Xamarin.Forms;

namespace PolySketch.UI.Tests
{
    public class TestCanvasPage : ContentPage
    {
        private KernelService Service;

        public TestCanvasPage( KernelService serv )
        {
            Service = serv;

            IXamarinFormsView view = Service.ActiveKernel.Get<IXamarinFormsView>();
            var but = new Button
            {
                Text = "Add Circle"
            };
            but.Clicked += ButtonClicked;
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello Page" }      ,
                    view as CocosSharpView      ,
                                 but
                }
            };
            var lm = Service.ActiveKernel.Get<LayerManager>();
            lm.CreateLayer();
        }

        private void ButtonClicked( object sender , EventArgs e )
        {
            var actKernel = Service.ActiveKernel;

            var lm = actKernel.Get<LayerManager>();

            var a = lm.ActiveLayer.DrawNode.GetDrawNode() as CCDrawNode;
            var b = a.Parent;
            ( lm.ActiveLayer.DrawNode.GetDrawNode() as CCDrawNode ).DrawSolidCircle(new CCPoint(0 , 0) , 100 , CCColor4B.Red);
        }
    }
}