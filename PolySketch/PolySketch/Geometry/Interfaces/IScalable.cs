using Poly.Geometry;

namespace PolySketch.Geometry.Interfaces
{
    public interface IScalable
    {
        void Scale(double value);

        void Scale(PVector center, double value);
    }
}