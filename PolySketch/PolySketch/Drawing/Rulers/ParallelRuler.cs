using Poly.Geometry;
using System.Collections.Generic;
using System.Linq;

namespace PolySketch.Drawing.Rulers
{
    public class ParallelRuler : StandartRuler
    {
        public ParallelRuler(Line dir) : base(dir)
        {
        }

        public ParallelRuler(PVector p1, PVector p2) : base(p1, p2)
        {
        }

        public ParallelRuler(PVector p1, float angle) : base(p1, angle)
        {
        }

        public ParallelRuler(double x1, double x2, double angle) : base(x1, x2, angle)
        {
        }

        public ParallelRuler(double x1, double y1, double x2, double y2) : base(x1, y1, x2, y2)
        {
        }

        /// <summary>
        /// Data is calculated from the first point (or last calculated point) and gives the line parallel to the ruler
        /// </summary>
        /// <param name="lastData"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public override List<PVector> CalculateData(List<PVector> lastData, List<PVector> data)
        {

            var retVal = new List<PVector>();
            foreach (var v in data)
            {
                retVal.Add(Direction.ClosestPoint(v));
            }
            PVector startingPoint;
            if (lastData.Count > 0)
            {
                var last = lastData.Last();
                startingPoint = last;
            }
            else
            {
                startingPoint = data.First();
            }
            var moveVector = PVector.Sub(retVal.First(), startingPoint);
            foreach (var v in retVal)
            {
                v.Move(moveVector);
            }
            return retVal;
        }
    }
}