using CocosSharp;

namespace PolySketch.Layering.RenderNode
{
    public class CocosDrawRenderNode : IDrawableRenderNode
    {
        public CocosDrawRenderNode( CCDrawNode node )
        {
            Node = node;
        }

        private CCDrawNode Node;

        public void ClearNode()
        {
            Node.Cleanup();
            Node.Clear();
        }

        public object GetDrawNode()
        {
            return Node;
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