namespace PolySketch.Layering.RenderNode
{
    public interface IRenderNode
    {
        void ClearNode();

        void Show();

        void Hide();

        object GetNode();
    }
}