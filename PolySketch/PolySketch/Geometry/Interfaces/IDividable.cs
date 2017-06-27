using Poly.Geometry;

namespace PolySketch.Geometry.Interfaces
{
    public interface IDividable
    {
        PVector[] Divide(int segs);
    }
}