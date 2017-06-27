using Poly.Geometry;

namespace PolySketch.Geometry
{
    public interface IGeometry
    {
        PVector[] GetDataToDraw(int precision);

        IGeometry CopyGeometry();

        void AddStartingPoint(PVector point);

        void AddUpdatePoint(PVector point);

        void UpdateWithData(PVector[] data);

        PVector[] ControlPoints { get; }
    }
}