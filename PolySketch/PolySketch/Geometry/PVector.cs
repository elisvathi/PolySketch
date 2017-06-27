using CocosSharp;
using PolySketch.Geometry;
using PolySketch.Geometry.Interfaces;
using System;

namespace Poly.Geometry
{
    public class PVector : IGeometry, IMovable, IRotatable, IScalable, IRulerFlagged
    {
        //PROPERTIES
        public double X
        {
            get; set;
        }

        public double Y
        {
            get; set;
        }

        public bool IsRuled { get; set; } = false;

        //CONSTRUCTOR
        public PVector( double X , double Y )
        {
            this.X = X;
            this.Y = Y;
        }

        public PVector()
        {
            X = 0;
            Y = 0;
        }

        public PVector( double angle )
        {
            X = Math.Cos(angle);
            Y = Math.Sin(angle);
            Normalize();
        }

        //METHODS
        public double AngleWith( PVector that )
        {
            return ( Math.Atan2(Cross(that) , Dot(that)) ).ToDegrees();
        }

        public double Dot( PVector that )
        {
            return X * that.X + Y * that.Y;
        }

        public double Cross( PVector that )
        {
            return X * that.Y - Y * that.X;
        }

        public void Set( double X , double Y )
        {
            this.X = X;
            this.Y = Y;
        }

        public void Set( PVector v )
        {
            Set(v.X , v.Y);
        }

        public double Mag()
        {
            return Math.Sqrt(Math.Pow(X , 2) + Math.Pow(Y , 2));
        }

        public void Add( PVector that )
        {
            X += that.X;
            Y += that.Y;
        }

        public void Sub( PVector that )
        {
            X -= that.X;
            Y -= that.Y;
        }

        public void SetMag( double val )
        {
            Mult(val / Mag());
        }

        public void Mult( double val )
        {
            X *= val;
            Y *= val;
        }

        public PVector Copy()
        {
            return new PVector(X , Y);
        }

        public double Dist( PVector v )
        {
            return PVector.Dist(this , v);
        }

        public double Angle
        {
            get
            {
                return ( Math.Atan(Tan()) ).ToDegrees();
            }
        }

        public PVector[] ControlPoints => new PVector[] { this };

        public void Normalize()
        {
            SetMag(1);
        }

        public void RotateVector( double angle )
        {
            double rad = angle.ToRadians();
            double cs = Math.Cos(rad);
            double sn = Math.Sin(rad);
            double tX = X * cs - Y * sn;
            double tY = X * sn + Y * cs;
            X = tX;
            Y = tY;
        }

        public CCPoint GetPoint()        
        {
            return new CCPoint(( float ) X , ( float ) Y);
        }

        //PRIVATE METHODS
        private double Tan()
        {
            return Y / X;
        }

        //STATIC METHODS
        public static double AngleBetween( PVector v1 , PVector v2 )
        {
            return v1.AngleWith(v2);
        }

        public static PVector Add( PVector v1 , PVector v2 )
        {
            var temp = v1.Copy();
            temp.Add(v2);
            return temp;
        }

        public static PVector Sub( PVector v1 , PVector v2 )
        {
            var temp = v1.Copy();
            temp.Sub(v2);
            return temp;
        }

        public static double Dist( PVector v1 , PVector v2 )
        {
            var temp = v2.Copy();
            temp.Sub(v1);
            return temp.Mag();
        }

        public static PVector FromAngle( double angle )
        {
            return new PVector(angle);
        }

        public static PVector Random2D()
        {
            return PVector.FromAngle(new Random().Next(0 , 360));
        }

        public PVector[] GetDataToDraw( int precision )
        {
            return new PVector[] { Copy() };
        }

        IGeometry IGeometry.CopyGeometry()
        {
            return Copy();
        }

        public void Move( PVector vect )
        {
            Add(vect);
        }

        public void Rotate( PVector center , double angle )
        {
            var v = Copy();
            var sub = PVector.Sub(v , center);
            sub.RotateVector(angle);
            sub.Add(center);
            Set(sub);
        }

         
        public void Scale( double value )
        {
        }

        public void Scale( PVector center , double value )
        {
            PVector v = PVector.Sub(this , center);
            v.Mult(value);
            v.Add(center);
            Set(v);
        }

        public void AddStartingPoint( PVector point )
        {
            Set(point);
        }

        public void AddUpdatePoint( PVector point )
        {
            Set(point);
        }

        public void UpdateWithData( PVector[] data )
        {
            Set(data[0]);
        }
    }
}