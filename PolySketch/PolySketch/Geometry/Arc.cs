using PolySketch.Geometry;
using PolySketch.Geometry.Interfaces;
using System;

namespace Poly.Geometry
{
    public class Arc : IGeometry, IExtendableGeometry, IExtendLimiter, IMovable, IRotatable, IScalable, IOffsetable
    {
        //Private Properties
        public double _StartAngle;

        public double _EndAngle;

        //Properties
        public PVector Center { get; set; }

        public double Radius { get; set; }

        public double StartAngle
        {
            get { return _StartAngle; }
            set
            {
                if (value < 0)
                { _StartAngle = 0; }
                else if (value > 360)
                { _StartAngle = 360; }
                else
                { _StartAngle = value; }
                FixAngles();
            }
        }

        public double EndAngle
        {
            get { return _EndAngle; }
            set
            {
                if (value < 0)
                { _EndAngle = 0; }
                else if (value > 360)
                { _EndAngle = 360; }
                else
                { _EndAngle = value; }
                FixAngles();
            }
        }

        public PVector[] ControlPoints => new PVector[] { Center, StartPoint, EndPoint };

        //Constructors
        public Arc(PVector center, double radius, double startAngle, double endAngle)
        {
            Center = center;
            Radius = radius;
            StartAngle = startAngle;
            EndAngle = endAngle;
        }

        public Arc(double x, double y, double radius) : this(new PVector(x, y), radius, 0, 360)
        {
        }

        public Arc(PVector center, double radius) : this(center.X, center.Y, radius)
        {
        }

        public Arc(PVector center, double radius, double angle) : this(center, radius, 0, angle)
        {
        }
        public Arc() { }
        //Public methods
        public IGeometry CopyGeometry() { return new Arc(Center.Copy(), Radius, StartAngle, EndAngle); }

        public void RotateVector(double angle)
        {
            StartAngle += angle; EndAngle += angle;
        }

        public void Rotate(PVector point, double angle)
        {
            PVector v1 = PVector.Sub(Center, point);
            v1.RotateVector(angle);
            v1.Add(Center);
            Center = v1;
            RotateVector(angle);
        }

        public void Move(PVector mov)
        {
            Center.Add(mov);
        }

        public PVector CirclePoint(double angle)
        {
            PVector rad = new PVector(0); rad.SetMag(Radius); rad.RotateVector(angle); rad.Add(Center); return rad;
        }

        public PVector StartPoint
        {
            get
            { return CirclePoint(StartAngle); }
        }

        public PVector EndPoint
        {
            get
            { return CirclePoint(EndAngle); }
        }

        public PVector MidPoint
        {
            get
            { return CirclePoint(EndAngle - StartAngle); }
        }

        public bool Contains(double rad)
        {
            return rad >= StartAngle && rad <= EndAngle;
        }

        public IGeometry Offset(double value)
        {
            return new Arc(Center, Radius + value, StartAngle, EndAngle);
        }

        public void Scale(PVector point, double value)
        {
            PVector v1 = PVector.Sub(Center, point); v1.Mult(value); v1.Add(Center); Center = v1; Radius *= value;
        }

        public void Scale(double value)
        {
            Scale(Center, value);
        }

        public PVector[] Divide(int n)
        {
            PVector[] retVal = new PVector[n + 1];
            double rap = (EndAngle - StartAngle) / n;
            for (int i = 0; i < n; i++)
            {
                retVal[i] = CirclePoint(i * rap);
            }
            retVal[n] = EndPoint;
            return retVal;
        }

        //PRIVATE METHODS
        private void FixAngles()
        {
            if (StartAngle > EndAngle)
            {
                double temp = StartAngle;
                StartAngle = EndAngle;
                EndAngle = temp;
            }
        }

        public PVector[] GetDataToDraw(int precision)
        {
            return Divide(precision*50);
        }

        public void Extend(IExtendLimiter limit)
        {
            
        }

        public void AddStartingPoint(PVector point)
        {
            Center = point;
        }

        public void AddUpdatePoint(PVector point)
        {
            Radius = PVector.Dist(Center, point);
        }

        public void UpdateWithData(PVector[] data)
        {
           
        }
    }
}