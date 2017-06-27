using Poly.Geometry;

namespace PolySketch.Drawing.Brushes
{
    public class Pencil : AbstractBrush
    {
        public override void Draw( PVector[] pointData )
        {
            Renderer.DrawPolygon(pointData , false);
        }
    }
}