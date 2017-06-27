using Poly.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PolySketch.Drawing.Rulers
{
    public class PerspectiveRuler : AbstractRuler
    {
        public PerspectiveRuler() : base()
        {
            _VanishingPoints = new List<PVector>();
        }

        public PerspectiveRuler(List<PVector> vps) : base()
        {
            _VanishingPoints = vps;
        }

        protected List<PVector> _VanishingPoints;

        public void MoveVanishingPoint(PVector vec, int n)
        {
            GetVanishingPoint(n).Move(vec);
        }

        public Line Horizon { get { return new Line(VanishingPoints[0], VanishingPoints[1]); } }
        public virtual List<PVector> VanishingPoints { get => _VanishingPoints; set => _VanishingPoints = value; }

        public void MoveHorizon(PVector vec)
        {
            MoveVanishingPoint(new PVector(0, vec.Y), 0);
            MoveVanishingPoint(new PVector(0, vec.Y), 1);
        }

        public PVector GetVanishingPoint(int n)
        {
            return VanishingPoints[n];
        }

        public void SetVanishingPoint(PVector vp, int n)
        {
            VanishingPoints[n] = vp;
        }

        public override List<PVector> CalculateData(List<PVector> lastData, List<PVector> data)
        {
            PVector refPoint;
            int startingNumber;
            List<PVector> retVal = new List<PVector>();
            if (lastData.Count > 0)
            { refPoint = lastData.Last(); startingNumber = 0; }
            else
            {
                refPoint = data.First();
                startingNumber = 1;
                retVal.Add(refPoint);
            }

            for (int i = startingNumber; i < data.Count; i++)
            {
                var vec = PVector.Sub(data[i], refPoint);
                var min = double.MaxValue;
                PVector calcVp = VanishingPoints[0];
                foreach (var foc in VanishingPoints)
                {
                    var dir = PVector.Sub(foc, refPoint);
                    var angle = Math.Min(PVector.AngleBetween(vec, dir), PVector.AngleBetween(dir, vec));
                    if (angle < min)
                    { min = angle; calcVp = foc; }
                }
                vec.RotateVector(PVector.AngleBetween(vec, PVector.Sub(calcVp, data[i])));
                retVal.Add(PVector.Add(data[i], vec));
            }
            return retVal;
        }
    }
}