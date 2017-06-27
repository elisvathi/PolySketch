using Ninject;
using Poly.NinjectModules.Kernels;
using PolySketch.Layering.RenderNode;
using PolySketch.UI.Canvas.Interfaces;
using System.Collections.Generic;

namespace PolySketch.Layering
{
    public class LayerManager
    {
        private List<Layer> layers;
        private int _ActiveLayerIndex = -1;

        public List<Layer> Layers { get => layers; }
        public int LayerCount { get { return Layers.Count; } }
        public Layer ActiveLayer { get { return Layers[_ActiveLayerIndex]; } }
        public IDrawableRenderNode ActiveLayerDrawableNode { get { return ActiveLayer.DrawNode; } }
        public IRenderNode ActiveLayerSimpleNode { get { return ActiveLayer.Node; } }

        private IDrawableRenderNode _TempDrawableNode;

        private IRenderNode _TempSimpleNode;

        private ICanvasLayerContainer _Container;

        [Inject]
        public ICanvasLayerContainer Container { get { return _Container; } set { _Container = value; AddTemporaryNodes(); } }

        private void AddTemporaryNodes()
        {
            Container.AddChild(TempSimpleNode);
            Container.AddChild(TempDrawableNode);
        }

        private KernelService Service;

        public bool DrawTemporary { get; set; } = false;

        public IDrawableRenderNode ActveDrawNode { get { return DrawTemporary == true ? _TempDrawableNode : ActiveLayerDrawableNode; } }
        public IRenderNode ActiveSimpleNode { get { return DrawTemporary == true ? _TempSimpleNode : ActiveLayerSimpleNode; } }

        public IDrawableRenderNode TempDrawableNode { get => _TempDrawableNode; set { _TempDrawableNode = value; } }

        public IRenderNode TempSimpleNode { get => _TempSimpleNode; set { _TempSimpleNode = value; } }

        public LayerManager( KernelService service , IDrawableRenderNode tempDrawableNode , IRenderNode tempSimpleNode )
        {
            Service = service;
            layers = new List<Layer>();
            TempDrawableNode = tempDrawableNode;
            TempSimpleNode = tempSimpleNode;
        }

        public void ClearTemporary()
        {
            TempSimpleNode.ClearNode();
            TempDrawableNode.ClearNode();
        }

        public void AddLayer( Layer l )
        {
            Layers.Add(l);
            _ActiveLayerIndex = LayerCount - 1;
            Container.AddChild(l.Node);
            Container.AddChild(l.DrawNode);
        }

        public void CreateLayer()
        {
            var l = Service.ActiveKernel.Get<Layer>();
            AddLayer(l);
        }

        public void DeleteLayer( int n )
        {
            if ( HasLayer(n) )
            {
                Container.RemoveChild(Layers[n].Node);
                Container.RemoveChild(Layers[n].DrawNode);
                Layers.RemoveAt(n);
            }
        }

        public void DeleteLayer( Layer l )
        {
            if ( HasLayer(l) )
            {
                Container.RemoveChild(l.Node);
                Container.RemoveChild(l.DrawNode);
                Layers.Remove(l);
            }
        }

        //Layers Visibility
        public void HideLayer( int layerIndex )
        {
            if ( HasLayer(layerIndex) )
            {
                Layers[layerIndex].HideLayer();
            }
        }

        public void ShowLayer( int layerIndex )
        {
            if ( HasLayer(layerIndex) )
            {
                Layers[layerIndex].ShowLayer();
            }
        }

        public void ToggleLayerVisibility( int layerIndex )
        {
            if ( HasLayer(layerIndex) )
            {
                Layers[layerIndex].ToggleVisibility();
            }
        }

        //Private Methods
        private bool HasLayer( int n )
        {
            return LayerCount <= n + 1;
        }

        private bool HasLayer( Layer l )
        {
            return Layers.Contains(l);
        }
    }
}