using Poly.Geometry;
using PolySketch.Geometry;
using System.Collections.Generic;

namespace PolySketch.Drawing.Tools
{
    public interface ITool
    {
        IGeometry BaseGeometry { get; set; }

        List<PVector> GetDataToDraw(int precision);
    }
}