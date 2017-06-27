using Poly.Geometry;

namespace PolySketch.Drawing.Brushes
{
    public interface IBrush
    {
        void Draw( PVector[] pointData );
    }
}