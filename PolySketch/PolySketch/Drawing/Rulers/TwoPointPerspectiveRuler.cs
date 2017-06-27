using Poly.Geometry;
using System.Collections.Generic;

namespace PolySketch.Drawing.Rulers
{
    public class TwoPointPerspectiveRuler : PerspectiveRuler
    {
        public TwoPointPerspectiveRuler() : this(200 , 100) { }
        public TwoPointPerspectiveRuler(int width, int height) : base()
        {
            _VanishingPoints = new List<PVector>();
            _VanishingPoints.Add(new PVector(0, height / 3));
            _VanishingPoints.Add(new PVector(width, height / 3));
            _VanishingPoints.Add(new PVector(width / 2, height * 300));
        }

        public override List<PVector> VanishingPoints { get { return new List<PVector>() { _VanishingPoints[0], _VanishingPoints[1] }; } set { _VanishingPoints = value; } }
    }
}