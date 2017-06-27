using PolySketch.Geometry;
using PolySketch.Geometry.Interfaces;
using System;

namespace Poly.Geometry
{
    public class Line : IGeometry, IExtendableGeometry, IExtendLimiter, IMovable, IScalable, IRotatable, IOffsetable
    {
        //Properties
        public PVector Start
        {
            get; set;
        }

        public PVector End
        {
            get; set;
        }

        public PVector[] ControlPoints => new PVector[] { Start , End };

        //Constructors
        public Line() { }

        public Line( PVector start , PVector end )
        {
            Start = start;
            End = end;
        }

        public Line( double x1 , double y1 , double x2 , double y2 ) : this(new PVector(x1 , y1) , new PVector(x2 , y2))
        {
        }

        public Line( PVector p1 , float angle , float len )
        {
            Start = p1;
            var v1 = new PVector(angle);
            v1.SetMag(len);
            End = v1;
        }

        //Public Methods
        public IGeometry CopyGeometry()
        {
            return new Line(Start.Copy() , End.Copy());
        }

        public double Length()
        {
            return PVector.Dist(Start , End);
        }

        public PVector Vector()
        {
            return PVector.Sub(End , Start);
        }

        public PVector MidPoint()
        {
            PVector temp = Vector();
            temp.Mult(0.5);
            return PVector.Add(Start , temp);
        }

        public void Rotate( PVector center , double angle )
        {
            PVector v1 = PVector.Sub(Start , center);
            PVector v2 = PVector.Sub(End , center);
            v1.RotateVector(angle);
            v2.RotateVector(angle);
            v1.Add(center);
            v2.Add(center);
            Start = v1;
            End = v2;
        }

        public void RotateVector( double angle )
        {
            Rotate(MidPoint() , angle);
        }

        public void Scale( PVector center , double value )
        {
            PVector v1 = PVector.Sub(Start , center);
            PVector v2 = PVector.Sub(End , center);
            v1.Mult(value);
            v2.Mult(value);
            v1.Add(center);
            v2.Add(center);
            Start = v1;
            End = v2;
        }

        public void Scale( double value )
        {
            Scale(MidPoint() , value);
        }

        public void Move( PVector mov )
        {
            Start.Add(mov);
            End.Add(mov);
        }

        public IGeometry Offset( double value )
        {
            var vec = Vector();
            vec.RotateVector(90);
            vec.SetMag(value);
            var retVal = CopyGeometry();
            ( retVal as IMovable ).Move(vec);
            return retVal;
        }

        public PVector[] Divide( int n )
        {
            PVector[] retVal = new PVector[n + 1];
            for ( int i = 0 ; i < n ; i++ )
            {
                var vec = Vector();
                vec.SetMag(i * Length());
                retVal[i] = PVector.Add(Start , vec);
            }
            retVal[n] = End.Copy();
            return retVal;
        }

        public PVector Intersection( Line that )
        {
            double x = ( that.B() - B() ) / ( M() - that.M() );
            double y = M() * x + B();
            return new PVector(x , y);
        }

        public bool Contains( PVector p )
        {
            return p.Y == M() * p.X + B() && p.Dist(Start) < Length() && p.Dist(End) < Length();
        }

        public bool Intersects( Line that )
        {
            var inter = Intersection(that);
            return Contains(inter) && that.Contains(inter);
        }

        public double Angle()
        {
            return Vector().Angle;
        }

        public void Flip()
        {
            PVector temp = Start.Copy();
            Start = End.Copy();
            End = temp;
        }

        //PRIVATE METHODS
        private double Dx()
        {
            return Vector().X;
        }

        private double Dy()
        {
            return Vector().Y;
        }

        private double M()
        {
            return Dy() / Dx();
        }

        private double B()
        {
            return Start.Y - M() * Start.X;
        }

        public PVector[] GetDataToDraw( int precision )
        {
            return Divide(precision);
        }

        public void Extend( IExtendLimiter limit )
        {
            throw new NotImplementedException();
        }

        public void AddStartingPoint( PVector point )
        {
            Start = point;
        }

        public void AddUpdatePoint( PVector point )
        {
            End = point;
        }

        public void UpdateWithData( PVector[] data )
        {
            Start = data[0];
            End = data[data.Length - 1];
        }

        public PVector ClosestPoint( PVector point )
        {
            var v = Vector();
            v.RotateVector(90);
            var l = new Line(point , ( float ) v.Angle , 20);
            return Intersection(l);
        }

        //STATICS
        public static bool AreParallel( Line l1 , Line l2 )
        {
            return l1.Angle() == l2.Angle();
        }
    }
}