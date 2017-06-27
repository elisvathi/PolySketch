namespace PolySketch.Geometry.Interfaces
{
    public interface ICompoundGeometry : IMultiSegment
    {
        void AddGeometry(IGeometry geometry);
    }
}