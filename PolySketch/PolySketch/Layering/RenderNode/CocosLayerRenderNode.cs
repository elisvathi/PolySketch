using CocosSharp;

namespace PolySketch.Layering.RenderNode
{
    internal class CocosLayerRenderNode : IRenderNode
    {
        public CocosLayerRenderNode( CCNode node )
        {
            Node = node;
        }

        private CCNode Node;

        public void ClearNode()
        {
            Node.Cleanup();
        }

        public object GetNode()
        {
            return Node;
        }

        public void Hide()
        {
            Node.Visible = false;
        }

        public void Show()
        {
            Node.Visible = true;
        }
    }
}