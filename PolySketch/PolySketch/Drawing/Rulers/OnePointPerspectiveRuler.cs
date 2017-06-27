using Poly.Geometry;
using System.Collections.Generic;

namespace PolySketch.Drawing.Rulers
{
    public class OnePointPerspectiveRuler : PerspectiveRuler
    {
        private OnePointPerspectiveRuler(int width, int height) : base()
        {
            float x = width / 2;
            float y = height / 2;
            _VanishingPoints = new List<PVector>();
            _VanishingPoints.Add(new PVector(x, y));
            _VanishingPoints.Add(new PVector(width * 300, y));
            _VanishingPoints.Add(new PVector(x, height * 300));
        }

        public override List<PVector> VanishingPoints { get { return new List<PVector>() { _VanishingPoints[0] }; } set { _VanishingPoints = value; } }
    }
}