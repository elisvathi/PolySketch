using PolySketch.Layering.RenderNode;

namespace PolySketch.Layering
{
    public class Layer
    {
        private IRenderNode node;
        private IDrawableRenderNode _DrawNode;
        private bool _IsVisible = true;

        public IRenderNode Node { get => node; set { node = value; } }

        public IDrawableRenderNode DrawNode { get => _DrawNode; set => _DrawNode = value; }

        public bool IsVisible { get => _IsVisible; set { IsVisible = value; if ( value == false ) { Node.Hide(); DrawNode.Hide(); } else { Node.Show(); DrawNode.Show(); } } }

        public Layer( IRenderNode rNode , IDrawableRenderNode dNode )
        {
            DrawNode = dNode;
            Node = rNode;
        }

        public void ToggleVisibility()
        {
            IsVisible = !IsVisible;
        }

        public void HideLayer()
        {
            IsVisible = false;
        }

        public void ShowLayer()
        {
            IsVisible = false;
        }
    }
}