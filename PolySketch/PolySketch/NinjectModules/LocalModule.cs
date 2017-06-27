using CocosSharp;
using Ninject.Modules;
using Poly.NinjectModules.Kernels;
using PolySketch.Drawing;
using PolySketch.Drawing.Brushes;
using PolySketch.Drawing.Rulers;
using PolySketch.Drawing.Services;
using PolySketch.Drawing.Tools;
using PolySketch.Layering;
using PolySketch.Layering.RenderNode;
using PolySketch.UI.Canvas;
using PolySketch.UI.Canvas.Interfaces;
using PolySketch.UI.ControlHandlers;
using PolySketch.UI.CustomControls;
using PolySketch.UI.CustomControls.ColorWheelComponents;
using PolySketch.UI.Tests;

namespace Poly.NinjectModules
{
    public class LocalModule : NinjectModule
    {
        private KernelService Service;

        public LocalModule( ref KernelService service )
        {
            Service = service;
        }

        public override void Load()
        {
            //Canvas UI

            Bind<KernelService>().ToConstant(Service).InSingletonScope();
            Bind<ICanvasLayerContainer>().To<CocosCanvasLayerContainer>().InSingletonScope();
            Bind<ICanvasFormsView>().To<CocosFormsView>().InSingletonScope();
            Bind<CanvasCamera>().ToSelf().InSingletonScope();
            Bind<IXamarinFormsView>().To<CocosXamarinFormsView>().InSingletonScope();
            Bind<IPaintCanvasScene>().To<CocosCanvasScene>().InSingletonScope();
            //Drawing
            Bind<ITool>().To<PolygonTool>().InTransientScope();
            Bind<IRuler>().To<TwoPointPerspectiveRuler>().InSingletonScope();
            Bind<IBrush>().To<Pencil>().InSingletonScope();

            //Singletons
            Bind<LayerManager>().ToSelf().InSingletonScope();
            Bind<DrawingToolsManager>().ToSelf().InTransientScope();
            Bind<DrawingService>().ToSelf().InSingletonScope();
            Bind<IRenderer>().To<CocosRenderer>().InSingletonScope();
            Bind<DrawingConfigService>().ToSelf().InSingletonScope();

            Bind<BrushControlHandler>().ToSelf().InSingletonScope();
            Bind<ToolControlHandler>().ToSelf().InSingletonScope();
            Bind<RulerControlHandler>().ToSelf().InSingletonScope();

            //Prototypes
            Bind<Layer>().ToSelf().InTransientScope();
            Bind<IRenderNode>().To<CocosLayerRenderNode>().InTransientScope();
            Bind<IDrawableRenderNode>().To<CocosDrawRenderNode>().InTransientScope();

            //Cocos Prototypes
            Bind<CCLayer>().To<MainLayer>().InTransientScope();
            Bind<CCNode>().ToSelf().InTransientScope();
            Bind<CCDrawNode>().ToSelf().InTransientScope();

            //TEst
            Bind<TestCocosPage>().ToSelf().InSingletonScope();
            Bind<TestClassCounter>().ToSelf().InSingletonScope();

            //Custom Controllers
            Bind<ColorWheelCocos>().ToSelf().InSingletonScope();
            Bind<ColorChangedEventDispatcher>().ToSelf().InSingletonScope();
            Bind<SaturationBar>().ToSelf().InSingletonScope();
            Bind<ValueBar>().ToSelf().InSingletonScope();
            Bind<HueCircle>().ToSelf().InSingletonScope();
        }
    }
}