using CocosSharp;
using Microsoft.Xna.Framework;
using Poly.Geometry;
using System;

namespace PolySketch.Helpers
{
    public class HSVColor
    {
        #region Properties

        private double _hue;
        private double _saturation;
        private double _value;
        private double _alpha;
        public double Hue { get { return _hue; } set { _hue = Clamp(value , 0 , 360); } }
        public double Saturation { get { return _saturation; } set { _saturation = Clamp(value , 0 , 1); } }
        public double Value { get { return _value; } set { _value = Clamp(value , 0 , 1); } }
        public double Alpha { get { return _alpha; } set { _alpha = Clamp(value , 0 , 255); } }

        #endregion Properties

        #region Constructors

        public HSVColor( float h , float s , float v , float alpha )
        {
            Hue = h;
            Saturation = s;
            Value = v;
            Alpha = alpha;
        }

        public HSVColor( float h , float s , float v ) : this(h , s , v , 255)
        {
        }

        public HSVColor() : this(0 , 1 , 1 , 255)
        {
        }

        public HSVColor( Color rgb )
        {
            double delta, min;
            double h = 0, s, v;

            min = Math.Min(Math.Min(rgb.R , rgb.G) , rgb.B);
            v = Math.Max(Math.Max(rgb.R , rgb.G) , rgb.B);
            delta = v - min;

            if ( v == 0.0 )
                s = 0;
            else
                s = delta / v;

            if ( s == 0 )
                h = 0.0;
            else
            {
                if ( rgb.R == v )
                    h = ( rgb.G - rgb.B ) / delta;
                else if ( rgb.G == v )
                    h = 2 + ( rgb.B - rgb.R ) / delta;
                else if ( rgb.B == v )
                    h = 4 + ( rgb.R - rgb.G ) / delta;

                h *= 60;

                if ( h < 0.0 )
                    h = h + 360;
            }

            Hue = h;
            Saturation = s;
            Value = v / 255;
            Alpha = rgb.A;
        }

        #endregion Constructors

        #region Private Methods

        private int Clamp( int i )
        {
            if ( i < 0 )
                return 0;
            if ( i > 255 )
                return 255;
            return i;
        }

        private double Clamp(double x, double start, double end )
        {
            if ( x < start )
            { return start; }else if ( x > end )
            { return end; } else
            { return x; }
        }
        #endregion Private Methods

        #region Public Methods

        public Color GetRGB()
        {
            double H = Hue;
            int r;
            int g;
            int b;
            double S = Saturation;
            double V = Value;

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
            return new Color(r , g , b , ( int ) Alpha);
        }

        public CCColor4F GetCocosColor()
        {
            return GetRGB().CocosColor();
        }

        #endregion Public Methods
    }
}