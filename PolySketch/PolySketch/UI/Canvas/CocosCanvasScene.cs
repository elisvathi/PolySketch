using CocosSharp;
using PolySketch.UI.Canvas.Interfaces;

namespace PolySketch.UI.Canvas
{
    public class CocosCanvasScene : IPaintCanvasScene
    {
        private CCScene Scene;

        public CocosCanvasScene( ICanvasFormsView view , ICanvasLayerContainer LayerContainer )
        {
            Scene = new CCScene(( CCGameView ) view.GetView());
            Scene.AddLayer(( CCLayer ) LayerContainer.GetLayer());
            
        }

        public object GetScene()
        {
            return Scene;
        }
    }
}