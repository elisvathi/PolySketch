using Poly.Geometry;
using PolySketch.Drawing.Rulers;
using System.Collections.Generic;

namespace PolySketch.Drawing.Tools.Interfaces
{
    public interface IRulable
    {
        void UpdateWithRuler(IRuler ruler);

        List<PVector> UnRuledData { get; }
    }
}