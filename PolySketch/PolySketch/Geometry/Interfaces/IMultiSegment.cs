using System.Collections.Generic;

namespace PolySketch.Geometry.Interfaces
{
    public interface IMultiSegment
    {
        List<IGeometry> GetSegments();

        double TotalLength();
    }
}