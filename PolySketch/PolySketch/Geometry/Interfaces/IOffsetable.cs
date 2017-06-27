namespace PolySketch.Geometry.Interfaces
{
    public interface IOffsetable
    {
        IGeometry Offset(double value);
    }
}