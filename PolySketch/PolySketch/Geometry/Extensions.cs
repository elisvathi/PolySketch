using CocosSharp;
using PolySketch.Helpers;
using System;
using Microsoft.Xna.Framework;
using Xamarin.Forms;

namespace Poly.Geometry
{
    public static class Extensions
    {
        public static double ToRadians( this double deg )
        {
            return deg * ( Math.PI / 180 );
        }
        public static float Map( this float val , float a1 , float b1 , float a2 , float b2 ) {
           var  rap = ( val - a1 ) / ( b1 - a1 );
            return rap * ( b2 - a2 ) + a2;
        }
        public static double ToDegrees( this double rad )
        {
            return rad * ( 180 / Math.PI );
        }

        public static PVector GetVector( this CCPoint point )
        {
            return new PVector(point.X , point.Y);
        }

        public static CCColor4F CocosColor( this Microsoft.Xna.Framework.Color c )
        {
            float r = ( ( float ) c.R ).Map(0 , 255 , 0 , 1);
            float g = ( ( float ) c.G ).Map(0 , 255 , 0 , 1);
            float b = ( ( float ) c.B ).Map(0 , 255 , 0 , 1);
            float a = ( ( float ) c.A ).Map(0 , 255 , 0 , 1);
            return new CCColor4F(r,g,b,a);
        }
        public static HSVColor ToHSV(this Microsoft.Xna.Framework.Color c )
        {
            return new HSVColor(c);
        }
         
        public static PTouch GetTouch( this CCTouch touch )
        {
            return new PTouch(touch.Location.GetVector() , touch.Location.GetVector());
        }
        public static double Clamp(this double value, double start, double end )
        {
            if (value < start )
                return start;
                       if(value > end )
            { return end; }

            return value;
        }
        public static float Clamp(this float value, float start, float end )
        {
            if ( value < start )
                return start;
            if ( value > end )
            { return end; }

            return value;
        }

        public static void AddNewElementFixedSize( this RelativeLayout layout , View v , double horizontalPosition , double verticalPosition , double width , double height )
        {
            layout.Children.Add(v ,
               Constraint.RelativeToParent(( parent ) => { return ( parent.Width * horizontalPosition ) - width / 2; }) ,
               Constraint.RelativeToParent(( parent ) => { return ( parent.Height * verticalPosition ) - height / 2; }) ,
               Constraint.Constant(width) ,
               Constraint.Constant(height)
               );
        }
        public static void AddNewElementFixedPosition( this RelativeLayout layout , View v , double horizontalPosition , double verticalPosition , double width , double height )
        {
            layout.Children.Add(v ,
                Constraint.Constant(horizontalPosition) ,
          Constraint.Constant(verticalPosition) ,
          Constraint.RelativeToParent(( parent ) => { return ( parent.Width * width ); }) ,
          Constraint.RelativeToParent(( parent ) => { return ( parent.Height * height ); })

          );
        }
        public static void AddNewElementRelative( this RelativeLayout layout , View v , double horizontalPosition , double verticalPosition , double width , double height )
        {
            layout.Children.Add(v ,
           Constraint.RelativeToParent(( parent ) => { return ( parent.Width * horizontalPosition ); }) ,
          Constraint.RelativeToParent(( parent ) => { return ( parent.Height * verticalPosition ); }) ,
          Constraint.RelativeToParent(( parent ) => { return ( parent.Width * width ); }) ,
          Constraint.RelativeToParent(( parent ) => { return ( parent.Height * height ); })

          );
        }
    }
}