using Poly.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolySketch.Geometry
{
    public class Circle : Arc
    {
        public Circle()
        {
            StartAngle = 0;
            EndAngle = 360;
        }
        public Circle(PVector cent, float rad) : this()
        {
            Center = cent;
            Radius = rad;
        }
        public Circle(PVector cent, PVector sec):this(cent, (float)PVector.Sub(sec, cent).Mag()) {

        }
    }
}
