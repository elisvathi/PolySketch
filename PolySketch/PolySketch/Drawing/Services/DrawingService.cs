using Ninject;
using Poly.Geometry;
using Poly.NinjectModules.Kernels;
using PolySketch.Drawing.Brushes;
using PolySketch.Drawing.Rulers;
using PolySketch.Drawing.Tools;
using PolySketch.Drawing.Tools.Interfaces;
using PolySketch.Helpers;
using PolySketch.Layering;
using PolySketch.Layering.RenderNode;
using PolySketch.UI.Canvas.Interfaces;

namespace PolySketch.Drawing
{
    public class DrawingService
    {
        [Inject]
        public KernelService Service { get; set; }

        private LayerManager Layers { get { return Service.ActiveKernel.Get<LayerManager>(); } }

        private IDrawableRenderNode DrawableNode { get { return Layers.ActveDrawNode; } }
        private IRenderNode SimpleNode { get { return Layers.ActiveSimpleNode; } }

        public ITool ActiveTool { get { return Service.ActiveKernel.Get<ITool>(); } }
        public IRuler ActiveRuler { get { return Service.ActiveKernel.Get<IRuler>(); } }
        public IBrush ActiveBrush { get { return Service.ActiveKernel.Get<IBrush>(); } }

        private ITool _TempTool;
        private IRuler _TempRuler;
        private IBrush _TempBrush;

        public DrawingService( ICanvasLayerContainer layer )
        {
            layer.SingleTouchStarted += TouchStartHandler;
            layer.SingleTouchMoved += TouchMoveHandler;
            layer.SingleTouchEnded += TouchEndHandler;
        }

        private void TouchEndHandler( PTouch touch )
        {
            FinalizeTool();
        }

        private void TouchMoveHandler( PTouch touch )
        {
            Update(touch.ActualPoint);
        }

        private void TouchStartHandler( PTouch touch )
        {
            Initialize(touch.ActualPoint);
        }

        public void Initialize( PVector startingPosition )
        {
            _TempBrush = ActiveBrush;
            _TempRuler = ActiveRuler;
            _TempTool = ActiveTool;
            Layers.DrawTemporary = true;
            if ( _TempTool is IInitializableTool )
            {
                ( ( IInitializableTool ) _TempTool ).Initialize(startingPosition);
            }
        }

        public void Update( PVector UpdatePosition )
        {
            Layers.ClearTemporary();
            if ( _TempTool is IUpdatable )
            {
                ( _TempTool as IUpdatable ).Update(UpdatePosition);
            }
            ApplyRuler();

            _TempBrush.Draw(_TempTool.GetDataToDraw(1).ToArray());
        }

        private void ApplyRuler()
        {
            if ( _TempTool is IRulable )
            {
                ( ( IRulable ) _TempTool ).UpdateWithRuler(_TempRuler);
            }
        }

        public void FinalizeTool()
        {
            Layers.ClearTemporary();
            Layers.DrawTemporary = false;
            if ( _TempTool is ICompoundable )
            {
                ( _TempTool as ICompoundable ).Finish();
            }
            _TempBrush.Draw(_TempTool.GetDataToDraw(1).ToArray());
        }

        public void Dispose()
        {
            if ( _TempTool is ICompoundable )
            {
                ( _TempTool as ICompoundable ).StartNextPart();
            } else
            {
                _TempBrush = null;
                _TempRuler = null;
                _TempTool = null;
            }
        }

        public void Cancel()
        {
            Dispose();
        }
    }
}