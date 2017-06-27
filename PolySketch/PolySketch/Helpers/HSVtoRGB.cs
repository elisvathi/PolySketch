using CocosSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Poly.Geometry;
using System;

namespace PolySketch.Helpers
{
    public static class HSVtoRGB
    {
        #region Converting base

        public static void Convert( double h , double S , double V , out int r , out int g , out int b )
        {
            double H = h;
            while ( H < 0 )
            { H += 360; };
            while ( H >= 360 )
            { H -= 360; };
            double R, G, B;
            if ( V <= 0 )
            { R = G = B = 0; } else if ( S <= 0 )
            {
                R = G = B = V;
            } else
            {
                double hf = H / 60.0;
                int i = ( int ) Math.Floor(hf);
                double f = hf - i;
                double pv = V * ( 1 - S );
                double qv = V * ( 1 - S * f );
                double tv = V * ( 1 - S * ( 1 - f ) );
                switch ( i )
                {
                    // Red is the dominant color

                    case 0:
                        R = V;
                        G = tv;
                        B = pv;
                        break;

                    // Green is the dominant color

                    case 1:
                        R = qv;
                        G = V;
                        B = pv;
                        break;

                    case 2:
                        R = pv;
                        G = V;
                        B = tv;
                        break;

                    // Blue is the dominant color

                    case 3:
                        R = pv;
                        G = qv;
                        B = V;
                        break;

                    case 4:
                        R = tv;
                        G = pv;
                        B = V;
                        break;

                    // Red is the dominant color

                    case 5:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                    case 6:
                        R = V;
                        G = tv;
                        B = pv;
                        break;

                    case -1:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // The color is not defined, we should throw an error.

                    default:
                        //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                        R = G = B = V; // Just pretend its black/white
                        break;
                }
            }
            r = Clamp(( int ) ( R * 255.0 ));
            g = Clamp(( int ) ( G * 255.0 ));
            b = Clamp(( int ) ( B * 255.0 ));
        }

        private static int Clamp( int i )
        {
            if ( i < 0 )
                return 0;
            if ( i > 255 )
                return 255;
            return i;
        }

        private static double Map( double x , double a1 , double b1 , double a2 , double b2 )
        {
            var d1 = b1 - a1;
            var d2 = b2 - a2;
            var rap = ( x - a1 ) / d1;
            return rap * d2 + a2;
        }

        #endregion Converting base

        #region Color form HSV

        public static Color GetFromHSV( double hue , double saturation , double value )
        {
            int R, G, B;
            Convert(hue , saturation , value , out R , out G , out B);
            return new Color(R , G , B);
        }

        #endregion Color form HSV

        #region ColorArrays

        public static Color[] GetHueArray( int precision , double sat , double val )
        {
            Color[] retval = new Color[precision];
            for ( int i = 0 ; i < precision ; i++ )
            {
                var angle = Map(i , 0 , precision , 0 , 360);
                retval[i] = GetFromHSV(angle , sat , val);
            }
            return retval;
        }

        public static Color[] GetHueArray( int precision )
        {
            return GetHueArray(precision , 1 , 1);
        }

        public static Color[] GetValueArray( double hue , double saturation , int precision )
        {
            Color[] retval = new Color[precision];
            for ( int i = 0 ; i < precision ; i++ )
            {
                var val = Map(i , 0 , precision , 0 , 1);
                retval[i] = GetFromHSV(hue , saturation , val);
            }
            return retval;
        }

        public static Color[] GetSaturationArray( double hue , double value , int precision )
        {
            Color[] retval = new Color[precision];
            for ( int i = 0 ; i < precision ; i++ )
            {
                var sat = Map(i , 0 , precision , 0 , 1);
                retval[i] = GetFromHSV(hue , sat , value);
            }
            return retval;
        }

        #endregion ColorArrays

        #region Private Helpers

        private static double Angle( PVector cent , PVector v2 )
        {
            int flag;
            if ( v2.X < cent.X )
            { flag = 1; } else
            { flag = -1; }
            var vv = PVector.Sub(v2 , cent);
            var vstart = new PVector(0 , 1);
            return PVector.AngleBetween(vstart , vv) * flag;
        }

        #endregion Private Helpers

        #region DrawColors

        public static CCDrawNode HueCircleDraw( float radius , float width )
        {


            var sprite = new CCDrawNode();
            sprite.DrawCircle(new CCPoint(0 , 0) , radius , CCColor4B.White);
            sprite.DrawCircle(new CCPoint(0 , 0) , radius - width , CCColor4B.White);
            var arclength = 2 * Math.PI * radius;
            var data = GetHueArray(( int ) arclength);
            var step = 360 / arclength;
            for ( int i = 0 ; i < data.Length ; i++ )
            {
                
                var point = new PVector(0 , radius);
                
                point.RotateVector(step * i);
                var p2 = point.Copy();
                p2.SetMag(radius - width);
                var col = data[i].CocosColor();
                sprite.DrawLine(point.GetPoint() , p2.GetPoint() , col);
            }
            return sprite;

        }

        public static CCDrawNode ValueBar(float hue,float sat, float width, float height )
        {
            var sprite = new CCDrawNode();
            var data = GetValueArray(hue , sat , ( int ) width);
            for(int i = 0 ; i< data.Length ;i++ )
            {
                
                var ind = i - width / 2;
                var col = data[i].CocosColor();
                sprite.DrawLine(new CCPoint(ind , ( float ) ( height * -0.5 )) , new CCPoint(ind , ( float ) ( height * 0.5 )) , col);
            }
            return sprite;
        }

        public static CCDrawNode SaturationBar( float hue , float val , float width , float height )
        {
            var sprite = new CCDrawNode();
            var data = GetSaturationArray(hue , val , ( int ) width);
            for ( int i = 0 ; i < data.Length ; i++ )
            {
               
                var ind = i - width / 2;
                var col = data[i].CocosColor();
                sprite.DrawLine(new CCPoint(ind , ( float ) ( height * -0.5 )) , new CCPoint(ind , ( float ) ( height * 0.5 )) , col);
            }
            return sprite;
        }
        #endregion DrawColors
    }
}