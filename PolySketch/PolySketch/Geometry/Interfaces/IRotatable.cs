using Poly.Geometry;

namespace PolySketch.Geometry.Interfaces
{
    public interface IRotatable
    {
        void RotateVector(double angle);

        void Rotate(PVector center, double angle);
    }
}