using CocosSharp;
using PolySketch.UI.Canvas.Interfaces;

namespace PolySketch.UI.Canvas
{
    public class CocosFormsView : ICanvasFormsView
    {
        private CCGameView GameView;

        public CocosFormsView( CCGameView view )
        {
            GameView = view;
        }

        public object GetView()
        {
            return GameView;
        }
    }
}