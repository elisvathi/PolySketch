using Poly.Geometry;
using System;
using System.Linq;

namespace PolySketch.Drawing.Brushes
{
    public class TechnicalBrush : AbstractBrush
    {
        private float _StrokeWidth;
        private float _DashLength;
        private float _SpaceLength;

        public class LinePattern
        {
            public double[] Pattern;

            public struct PatternMoment
            {
                public int SuperUnits;
                public int SubUnits;
                public double SubUnitMoment;
                public bool IsDash;
                public double SubUnitRemaining;
            }

            public struct PatternNextStep
            {
                public bool Dash;
                public float length;
            }

            public LinePattern()
            {
                Pattern = new double[] { 2 , 2 };
            }

            public LinePattern( double[] pat )
            {
                Pattern = pat;
            }

            private double UnitLength
            {
                get
                {
                    double sum = 0;
                    foreach ( var d in Pattern )
                    { sum += d; }
                    return sum;
                }
            }

            private int SuperUnits( double d )
            {
                return ( int ) Math.Floor(d / UnitLength);
            }

            private int SubUnits( double d )
            {
                return IndexOf(d) + 1;
            }

            private double SubUnitMoment( double d )
            {
                return ( d % UnitLength ) - LengthBefore(IndexOf(d));
            }

            private bool IsDash( double d )
            {
                return IndexOf(d) % 2 == 0;
            }

            private double LengthBefore( int n )
            {
                double sum = 0;
                for ( int i = 0 ; i < n ; i++ )
                { sum += Pattern[i]; }
                return sum;
            }

            private double LengthAfter( int n )
            {
                return LengthBefore(n + 1);
            }

            private int IndexOf( double length )
            {
                double len = length % UnitLength;
                var array = new int[Pattern.Length];
                for ( int i = 0 ; i < array.Length ; i++ )
                { array[i] = i; }
                var x = from elem in array
                        where LengthBefore(elem) <= len
                        where LengthAfter(elem) > len
                        select elem;
                return x.First();
            }

            public PatternMoment Moment( double d )
            {
                return new PatternMoment
                {
                    SuperUnits = SuperUnits(d) ,
                    SubUnits = SubUnits(d) ,
                    IsDash = IsDash(d) ,
                    SubUnitMoment = SubUnitMoment(d) ,
                    SubUnitRemaining = Pattern[IndexOf(d)] - SubUnitMoment(d)
                };
            }
        }

        public override void Draw( PVector[] pointData )
        {
            int i = 0;
            double remainingLength = 0;
            while ( i < pointData.Length - 1 )
            {
                double len = PVector.Sub(pointData[i] , pointData[i + 1]).Mag();
                int fullNumber = ( int ) Math.Floor(( len - remainingLength ) / ( _DashLength + _SpaceLength ));
                double remainingLengthTemp = ( len - remainingLength ) % ( _DashLength + _SpaceLength );
                if ( remainingLength > 0 )
                { DrawDash(pointData[i] , pointData[i + 1] , 0 , remainingLength); }
                for ( int k = 0 ; k < fullNumber ; k++ )
                {
                    DrawDash(pointData[i] , pointData[i + 1] , k * ( _DashLength + _SpaceLength ) + remainingLength , _DashLength);
                }
                if ( remainingLengthTemp > _DashLength )
                {
                    DrawDash(pointData[i + 1] , pointData[i] , remainingLengthTemp - _DashLength , _DashLength);
                } else if ( remainingLengthTemp > 0 )
                {
                    DrawDash(pointData[i + 1] , pointData[i] , 0 , remainingLengthTemp);
                }
                remainingLength = ( _DashLength + _SpaceLength ) - remainingLengthTemp;
                i++;
            }
        }

        private void DrawDash( PVector start , PVector end )
        {
            Renderer.DrawLine(start , end);
        }

        private void DrawDash( PVector a , PVector b , double startingPoint , double length )
        {
            PVector vec = PVector.Sub(b , a);
            vec.SetMag(length);
            PVector vecstart = vec.Copy();
            vecstart.SetMag(startingPoint);
            vecstart.Add(a);
            vec.Add(vecstart);
            DrawDash(vecstart , vec);
        }
    }
}