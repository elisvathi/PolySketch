using Poly.Geometry;
using System.Collections.Generic;

namespace PolySketch.Drawing.Rulers
{
    public interface IRuler
    {
        float Weight { get; set; }

        List<PVector> UpdateWithRuler(List<PVector> lastData, List<PVector> data);
    }
}