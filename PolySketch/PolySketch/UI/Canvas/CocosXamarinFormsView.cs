using CocosSharp;
using Ninject;
using Poly.NinjectModules.Kernels;
using PolySketch.UI.Canvas.Interfaces;
using System;
using Xamarin.Forms;

namespace PolySketch.UI.Canvas
{
    public class CocosXamarinFormsView : CocosSharpView, IXamarinFormsView
    {
        [Inject]
        public KernelService Service { get; set; }

        public IKernel ActiveKernel { get { return Service.ActiveKernel; } }

        public CocosXamarinFormsView()
        {
            this.HorizontalOptions = LayoutOptions.FillAndExpand;
            this.VerticalOptions = LayoutOptions.FillAndExpand;
            ViewCreated = HandleViewCreated;
        }

        private void HandleViewCreated( object sender , EventArgs e )
        {
            if ( sender is CCGameView view )
            {
                view.DesignResolution = new CCSizeI(( int ) Width , ( int ) Height);

                ActiveKernel.Bind<CCGameView>().ToConstant(view).InSingletonScope();

                var kernelView = ActiveKernel.Get<ICanvasFormsView>().GetView() as CCGameView;
                kernelView.RunWithScene(ActiveKernel.Get<IPaintCanvasScene>().GetScene() as CCScene);
            }
        }
    }
}