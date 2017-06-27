namespace PolySketch.Geometry
{
    public interface IExtendableGeometry
    {
        void Extend(IExtendLimiter limit);
    }
}