using CocosSharp;

namespace PolySketch.UI.Canvas
{
    public class MainLayer : CCLayerColor
    {
        public CCNode MainNode;

        public MainLayer( CCNode node ) : base(CCColor4B.White)
        {
            MainNode = node;
            AddChild(MainNode);
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();
            
            //MainNode.Position = BoundingBox.Center;
        }
    }
}