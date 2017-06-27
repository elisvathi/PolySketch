using Poly.Geometry;
using System.Collections.Generic;

namespace PolySketch.Drawing.Rulers
{
    public class StandartRuler : AbstractRuler
    {
        public Line Direction;

        public StandartRuler(Line dir) : base()
        {
            Direction = dir;
        }

        public StandartRuler(PVector p1, PVector p2) : this(new Line(p1, p2))
        {
        }

        public StandartRuler(double x1, double y1, double x2, double y2) : this(new PVector(x1, y1), new PVector(x2, y2))
        {
        }

        public StandartRuler(PVector p1, float angle) : this(new Line(p1, angle, 20))
        {
        }

        public StandartRuler(double x1, double x2, double angle) : this(new PVector(x1, x2), (float)angle)
        {
        }

        public PVector Start { get { return Direction.Start; } }
        public PVector End { get { return Direction.End; } }

        public void SetStart(PVector start)
        {
            Start.Set(start);
        }

        public void SetEnd(PVector end)
        {
            End.Set(end);
        }

        public void Set(PVector start, PVector end)
        {
            SetStart(start);
            SetEnd(end);
        }

        public void MoveStart(PVector vec)
        {
            Start.Move(vec);
        }

        public void MoveEnd(PVector vec)
        {
            End.Move(vec);
        }

        public override List<PVector> CalculateData(List<PVector> lastData, List<PVector> data)
        {
            var retVal = new List<PVector>();
            foreach (var v in data)
            {
                retVal.Add(Direction.ClosestPoint(v));
            }
            return retVal;
        }
    }
}