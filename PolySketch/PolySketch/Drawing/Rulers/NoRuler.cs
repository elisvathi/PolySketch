using Poly.Geometry;
using System.Collections.Generic;

namespace PolySketch.Drawing.Rulers
{
    public class NoRuler : AbstractRuler
    {
        public NoRuler() : base()
        {
        }

        public override List<PVector> CalculateData( List<PVector> lastData , List<PVector> data )
        {
            return data;
        }
    }
}